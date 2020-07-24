using FIXCommModule.Common.Configuration;
using FIXCommModule.Common.DTOs;
using FIXCommModule.Common.Wrappers.Generic;
using FIXCommModule.Common.Wrappers.Order_Routing;
using FIXCommModule.LogicLayer.Util.Builder;
using FIXCommModule.LogicLayer.Util.Converter;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.Common.Wrappers;
using Fwk.Main.LogicLayer.Modules;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using QuickFix.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIXCommModule
{
    public class FIXCommModule : BaseModule, ILogger,IApplication
    {
        #region Protected Attributes

        protected static SessionSettings SessionSettings { get; set; }
        protected static FileStoreFactory FileStoreFactory { get; set; }
        protected static ScreenLogFactory ScreenLogFactory { get; set; }
        protected static SessionID SessionID { get; set; }
        protected static MessageFactory MessageFactory { get; set; }
        protected static SocketInitiator Initiator { get; set; }

        protected Configuration Configuration { get; set; }

        protected IFIXMessageCreator FIXMessageCreator { get; set; }

        protected Dictionary<string, ICommunicationModule> TestingModules { get; set; }

        protected Dictionary<string,string> SentOrders{ get; set; }

        protected Dictionary<int, string> KeysDict { get; set; }

        protected Dictionary<int, string> SendersDict { get; set; }

        protected object tLock = new object();

        #endregion


        #region Protected Metods

        protected void InstantiateTestingModules()
        {
            foreach (Module testingModule in Configuration.TestingModules)
            {
                try
                {
                    DoLog(string.Format("Initializing Testing Module {0}", testingModule.Name), Constants.MessageType.Information);
                    if (!string.IsNullOrEmpty(testingModule.Assembly))
                    {
                        var moduleType = Type.GetType(testingModule.Assembly);
                        if (moduleType != null)
                        {
                            ICommunicationModule module = (ICommunicationModule)Activator.CreateInstance(moduleType);
                            module.Initialize(ProcessMessage, DoLog, testingModule.Config);

                            TestingModules.Add(testingModule.Name, module);
                        }
                        else
                            throw new Exception("assembly not found: " + testingModule.Assembly);
                    }
                    else
                        DoLog(string.Format("Testing module {0} not found. It will not be initialized",testingModule.Name), Constants.MessageType.Error);
                }
                catch (Exception ex)
                {

                    DoLog(string.Format("Critical error initializing module {0}:{1}", testingModule.Name, ex.Message), Constants.MessageType.Error);
                }
            }
        }

        protected void ProcessMessageReject(object param)
        {
            try
            {
                Reject reject = (Reject)param;

                int refSeqNum = reject.GetInt(QuickFix.Fields.Tags.RefSeqNum);

                if (SendersDict.ContainsKey(refSeqNum))
                {
                    string sender = SendersDict[refSeqNum];

                    if (TestingModules.ContainsKey(sender))
                    {
                        if (KeysDict.ContainsKey(refSeqNum))
                        {
                            string key = KeysDict[refSeqNum];
                            RejectWrapper rejectWrapper = new RejectWrapper(key, reject);
                            TestingModules[sender].ProcessMessage(rejectWrapper);
                        }
                        else
                            DoLog(string.Format("Discarding message because no key was identified @{0}: Sender={1}", Configuration.Name, sender), Fwk.Main.Common.Util.Constants.MessageType.Information);

                    }
                    else
                        DoLog(string.Format("Discarding message because no sender was identified @{0}: Sender={1}", Configuration.Name, sender), Fwk.Main.Common.Util.Constants.MessageType.Information);

                }
                else
                {
                    DoLog(string.Format("Discarding message because no sender was identified @{0}: Message={1}", Configuration.Name, refSeqNum), Fwk.Main.Common.Util.Constants.MessageType.Information);
                }

            }
            catch (Exception ex)
            {

                DoLog(string.Format("Critical error processing a Reject message @{0}:{1}", Configuration.Name, ex.Message), Fwk.Main.Common.Util.Constants.MessageType.Error);
            }
        
        }

        protected void ProcesssExecutionReportMessage(object param)
        { 
           try
            {
                QuickFix.FIX44.ExecutionReport execReport = (QuickFix.FIX44.ExecutionReport)param;
                string clOrdId = execReport.GetString(QuickFix.Fields.Tags.ClOrdID);

                lock (tLock)
                {

                    if (SentOrders.ContainsKey(clOrdId))
                    {
                        string sender = SentOrders[clOrdId];

                        if (TestingModules.ContainsKey(sender))
                        {
                            FIXExecutionReportWrapper wrapper = new FIXExecutionReportWrapper(clOrdId,execReport, Configuration);
                            TestingModules[sender].ProcessMessage(wrapper);
                        }
                        else
                            DoLog(string.Format("{0}-Unknown sender @{1} ", Configuration.Name, sender), Fwk.Main.Common.Util.Constants.MessageType.Error);
                    
                    }
                    else
                        DoLog(string.Format("{0}-Ignoring unknown ClOrdId execution report @{1} ", Configuration.Name, clOrdId), Fwk.Main.Common.Util.Constants.MessageType.Information);

                }
            }
            catch (Exception ex)
            {
                DoLog(string.Format("Critical error processing execution report @{0}:{1}", Configuration.Name, ex.Message), Fwk.Main.Common.Util.Constants.MessageType.Error);
            }
        
        }

        protected void ProcessNewOrder(object param)
        {
            try
            {
                lock (tLock)
                {
                    Wrapper wrapper = (Wrapper)param;
                    Order order = OrderConverter.ConvertNewOrder(wrapper);
                    string sender = (string)wrapper.GetField(OrderFields.SENDER);

                    if (!string.IsNullOrEmpty(sender) && TestingModules.ContainsKey(sender))
                    {
                        SentOrders.Add(order.ClOrdId, sender);

                        QuickFix.Message nos = FIXMessageCreator.CreateNewOrderSingle(order.ClOrdId, order.Security.Symbol, order.Side, order.OrdType, order.SettlType,
                                                                                    order.TimeInForce, order.EffectiveTime, order.OrderQty, order.Price, order.StopPx,
                                                                                    order.Account,order.Exchange);

                        Session.SendToTarget(nos, SessionID);

                        SendersDict.Add(nos.Header.GetInt(QuickFix.Fields.Tags.MsgSeqNum), sender);
                        KeysDict.Add(nos.Header.GetInt(QuickFix.Fields.Tags.MsgSeqNum), order.ClOrdId);
                    }
                    else
                        throw new Exception("Cannot create an order for unknown sender");
                }
            }
            catch (Exception ex)
            {
                DoLog(string.Format("Critical error processing new order @{0}:{1}", Configuration.Name, ex.Message), Fwk.Main.Common.Util.Constants.MessageType.Error);
            }
        }

        #endregion

        #region ICommunicationModule

        public override CMState ProcessMessage(Fwk.Main.Common.Wrappers.Wrapper wrapper)
        {

            try
            {
                if (wrapper.GetAction() == Actions.NEW_ORDER)
                {
                    DoLog("Processing NEW_ORDER:" + wrapper.ToString(), Fwk.Main.Common.Util.Constants.MessageType.Information);
                    Thread processNewOrderThread = new Thread(ProcessNewOrder);
                    processNewOrderThread.Start(wrapper);
                    return CMState.BuildSuccess();
                }
                else
                    return CMState.BuildFail(new Exception(string.Format("Could not process action {0} for strategy {1}", wrapper.GetAction().ToString(), Configuration.Name)));
            }
            catch (Exception ex)
            {
                DoLog("Error processing market data @" + Configuration.Name + ":" + ex.Message,Fwk.Main.Common.Util.Constants.MessageType.Error);
                return CMState.BuildFail(ex);
            }
        }

        public override bool Initialize(OnMessageReceived pOnPublishMessage, OnLogMessage pOnLogMsg, string configFile)
        {
            try
            {
                this.ModuleConfigFile = configFile;
                this.DoPublishMessage += pOnPublishMessage;
                this.OnLogMsg += pOnLogMsg;

                if (LoadConfig(configFile))
                {
                    SentOrders = new Dictionary<string, string>();
                    SendersDict = new Dictionary<int, string>();
                    KeysDict = new Dictionary<int, string>();
                    TestingModules = new Dictionary<string, ICommunicationModule>();

                    FIXMessageCreator = new FIXMessageCreator();

                    string path = Configuration.InitiatorCfg;

                    SessionSettings = new SessionSettings(path);
                    FileStoreFactory = new FileStoreFactory(SessionSettings);
                    ScreenLogFactory = new ScreenLogFactory(SessionSettings);
                    MessageFactory = new QuickFix.FIX44.MessageFactory();

                    Initiator = new SocketInitiator(this, FileStoreFactory, SessionSettings, ScreenLogFactory);

                    Initiator.Start();

                    return true;
                }
                else
                {
                    DoLog("Error initializing config file " + configFile, Constants.MessageType.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                DoLog("Critic error initializing " + configFile + ":" + ex.Message, Constants.MessageType.Error);
                return false;
            }
        }

        #endregion

        #region ILogger

        public void DoLog(string msg, Fwk.Main.Common.Util.Constants.MessageType type)
        {
            base.DoLog(msg, type);
        }

        public override void DoLoadConfig(string configFile, List<string> noValFields)
        {
            List<string> noValueFields = new List<string>();
            Configuration = new Configuration().GetConfiguration<Configuration>(configFile, noValueFields);
        }

        #endregion

        #region Quickfix

        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            DoLog(string.Format("@fromAdmin:{0}", message.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
            if (message is Reject)
            {
                Reject reject = (Reject)message;
                Thread rejectPrcThread = new Thread(ProcessMessageReject);
                rejectPrcThread.Start(reject);
            }
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            DoLog(string.Format("@fromApp:{0}", message.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
            if (message is QuickFix.FIX44.ExecutionReport)
            {
                Thread processExecReport = new Thread(ProcesssExecutionReportMessage);
                processExecReport.Start((QuickFix.FIX44.ExecutionReport)message);
            }
        }

        public void OnCreate(SessionID sessionID)
        {
            DoLog(string.Format("@onCreate:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void OnLogon(SessionID sessionID)
        {
            SessionID = sessionID;
            InstantiateTestingModules();//Now we can start the tests
            DoLog(string.Format("@onLogon:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void OnLogout(SessionID sessionID)
        {
            DoLog(string.Format("@onLogout:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            DoLog(string.Format("@toAdmin:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
            if (message is Logon)
            {
                message.Header.SetField(new ResetSeqNumFlag(ResetSeqNumFlag.NO));
            }
          
        }

        public void ToApp(QuickFix.Message message, SessionID sessionId)
        {
            DoLog(string.Format("@toApp:{0}", sessionId.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);

        }

        #endregion
    }
}

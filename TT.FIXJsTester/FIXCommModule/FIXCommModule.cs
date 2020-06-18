using FIXCommModule.Common.Configuration;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.LogicLayer.Modules;
using QuickFix;
using QuickFix.FIX44;
using QuickFix.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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


        #endregion

        #region ICommunicationModule

        public override Fwk.Main.Common.DTO.CMState ProcessMessage(Fwk.Main.Common.Wrappers.Wrapper wrapper)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize(OnMessageReceived pOnMessageRcv, OnLogMessage pOnLogMsg, string configFile)
        {
            try
            {
                this.ModuleConfigFile = configFile;
                this.OnMessageRcv += pOnMessageRcv;
                this.OnLogMsg += pOnLogMsg;

                if (LoadConfig(configFile))
                {
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
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            DoLog(string.Format("@fromApp:{0}", message.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void OnCreate(SessionID sessionID)
        {
            DoLog(string.Format("@onCreate:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void OnLogon(SessionID sessionID)
        {
            SessionID = sessionID;
            DoLog(string.Format("@onLogon:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void OnLogout(SessionID sessionID)
        {
            DoLog(string.Format("@onLogout:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            DoLog(string.Format("@toAdmin:{0}", sessionID.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);
        }

        public void ToApp(QuickFix.Message message, SessionID sessionId)
        {
            DoLog(string.Format("@toApp:{0}", sessionId.ToString()), Fwk.Main.Common.Util.Constants.MessageType.Information);

        }

        #endregion
    }
}

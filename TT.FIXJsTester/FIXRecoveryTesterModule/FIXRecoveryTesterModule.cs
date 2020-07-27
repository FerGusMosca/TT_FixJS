using BaseTestingModule.Common.Wrappers.Order_Routing;
using BaseTestingModule.LogicLayer.Uitl.Builder;
using FIXRecoveryTesterModule.Common.Configuration;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIXRecoveryTesterModule
{
    public class FIXRecoveryTesterModule : BaseTestingModule.BaseTestingModule
    {

        #region Protected Attributes

        protected Configuration Configuration { get; set; }

        protected Dictionary<string, object> MessageOk { get; set; }


        protected Dictionary<string, int> ExecutionReportsReceived { get; set; }

        #endregion

        #region Protected Methods

        protected void ProcessReject(object param)
        {
        
        
        
        }

        protected void ProcessExecutionReport(object param)
        {

            try
            {
                lock (TimeoutOrders)
                {
                    Wrapper erWrapper = (Wrapper)param;

                    string key = (string)erWrapper.GetField(ExecutionReportFields.KEY);

                    string orderId = (string)erWrapper.GetField(ExecutionReportFields.OrderID);
                    string clOrderId = (string)erWrapper.GetField(ExecutionReportFields.ClOrdID);

                    if (MessageOk.ContainsKey(key))
                    {
                        ExecutionReportsReceived[key]++;

                        if (ExecutionReportsReceived[key] == Configuration.ResponsesToArrive)
                        {
                            TimeoutOrders.Remove(key);
                            DoLog(string.Format("<{0}>- Received {1} ERs with order id {2}", Configuration.Name, ExecutionReportsReceived[key], orderId), Constants.MessageType.AssertOk);
                        }
                        else if (ExecutionReportsReceived[key] > Configuration.ResponsesToArrive)
                            DoLog(string.Format("<{0}>- Received {1} ERs for ClOrdId {2}. This is more than expected (max {3})!!!!",
                                                Configuration.Name, ExecutionReportsReceived[key], clOrderId, Configuration.ResponsesToArrive),
                                                Constants.MessageType.AssertOk);
                    }
                    //This must be an order we don't manage or that we have already rejected so the counter is considered to be not working
                }
            }
            catch (Exception ex)
            {
                DoLog(string.Format("<{0}>-Critical error processing a execution report:{1}", Configuration.Name, ex.Message), Constants.MessageType.AssertFailed);
            }

        }

        protected void ProcessTimeouts(object param)
        {

            try
            {
                while (true)
                {
                    lock (TimeoutOrders)
                    {
                        List<string> toRemove = new List<string>();
                        foreach (string key in TimeoutOrders.Keys)
                        {
                            TimeSpan elapsed = DateTime.Now - TimeoutOrders[key];

                            if (elapsed.TotalSeconds > Configuration.TimeoutInSeconds)
                            {

                                DoLog(string.Format("<{0}>- TIMEOUT waiting for ERs for ClOrdId {1}. Received {2} - Expected {3}",
                                                        Configuration.Name, key, ExecutionReportsReceived[key],
                                                        Configuration.ResponsesToArrive),
                                                        Constants.MessageType.AssertFailed);

                                toRemove.Add(key);
                            }
                        }

                        toRemove.ForEach(x => TimeoutOrders.Remove(x));
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                DoLog(string.Format("<{0}>- Critical error cleaning timedout Execution Reports:{1}",
                                                    Configuration.Name, ex.Message),
                                                    Constants.MessageType.AssertFailed);
            }
        }

        public void RunTestsThread()
        {
            try
            {
                while (true)
                {
                    lock (TimeoutOrders)
                    {
                        //1- Ok order
                        DoLog(string.Format("Sending full regular order @{0}", Configuration.Name), Constants.MessageType.Information);
                        Order fullOrder = OrderBuilder.BuildFullOrder();
                        NewOrderWrapper fullOrderWrapper = new NewOrderWrapper(fullOrder, Configuration);
                        MessageOk.Add(fullOrder.ClOrdId, fullOrder);
                        TimeoutOrders.Add(fullOrder.ClOrdId, DateTime.Now);
                        ExecutionReportsReceived.Add(fullOrder.ClOrdId, 0);
                        DoPublishMessage(fullOrderWrapper);
                    }
                    Thread.Sleep(2000);
                }

            }
            catch (Exception ex)
            {
                DoLog(string.Format("{0}-Critical error running tests:{1}", Configuration.Name, ex.Message), Constants.MessageType.Error);
            }
        }


        #endregion

        #region ICommunicationModule

        public override CMState ProcessMessage(Wrapper wrapper)
        {
            try
            {
                if (wrapper.GetAction() == Actions.REJECT)
                {
                    DoLog("Processing REJECT @:" + Configuration.Name, Fwk.Main.Common.Util.Constants.MessageType.Information);

                    Thread processNewOrderThread = new Thread(ProcessReject);
                    processNewOrderThread.Start(wrapper);
                    return CMState.BuildSuccess();
                }
                else if (wrapper.GetAction() == Actions.EXECUTION_REPORT)
                {
                    DoLog("Processing EXECUTION_REPORT @:" + Configuration.Name, Fwk.Main.Common.Util.Constants.MessageType.Information);

                    Thread processExecReportThread = new Thread(ProcessExecutionReport);
                    processExecReportThread.Start(wrapper);
                    return CMState.BuildSuccess();
                }
                else
                    return CMState.BuildFail(new Exception(string.Format("Could not process action {0} for strategy {1}", wrapper.GetAction().ToString(), Configuration.Name)));
            }
            catch (Exception ex)
            {
                DoLog("Error processing message @" + Configuration.Name + ":" + ex.Message, Fwk.Main.Common.Util.Constants.MessageType.Error);
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
                    MessageOk = new Dictionary<string, object>();
                    TimeoutOrders = new Dictionary<string, DateTime>();

                    ExecutionReportsReceived = new Dictionary<string, int>();

                    Thread runTestsThread = new Thread(RunTestsThread);
                    runTestsThread.Start();

                 
                    Thread processTimeouts = new Thread(ProcessTimeouts);
                    processTimeouts.Start(Configuration.Name);

                    

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

        public override void DoLoadConfig(string configFile, List<string> noValueFields)
        {
            List<string> noValFlds = new List<string>();
            Configuration = new Configuration().GetConfiguration<Configuration>(configFile, noValFlds);
        }

        #endregion
    }
}

using BaseTestingModule.Common.Wrappers.Order_Routing;
using BaseTestingModule.LogicLayer.Uitl.Builder;
using FIXExtendedCounterTesterModule.Common.Configuration;
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

namespace FIXExtendedCounterTesterModule
{
    public class FIXExtendedCounterTesterModule : BaseTestingModule.BaseTestingModule
    {
        #region Protected Attributes

        protected Configuration Configuration { get; set; }

        protected Dictionary<string, Order> OrdersSent { get; set; }

        protected Dictionary<string,int> LastProcessedCounter { get; set; }

        #endregion

        #region Protected Methods

        private void ClearOrderTracking(string clOrderId)
        {
            TimeoutOrders.Remove(clOrderId);
            LastProcessedCounter.Remove(clOrderId);
            OrdersSent.Remove(clOrderId);
        
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

                    if (OrdersSent.ContainsKey(key) && LastProcessedCounter.ContainsKey(key))
                    {

                        DoLog(string.Format("<{0}>- Received order id {1} - cl order id {2}", Configuration.Name, orderId,clOrderId), Constants.MessageType.Information);

                        try
                        {
                           
                            int counter = Convert.ToInt32(erWrapper.GetField(ExecutionReportFields.Account));
                            int lastCounter = LastProcessedCounter[clOrderId];

                            //We validate that it's lower. No that it has a specific number
                            if (counter == Configuration.ExecutionReportsPerOrder && counter == (lastCounter + 1))
                            {
                                DoLog(string.Format("<{0}>- Recevied counter #{1} for cl. order id {2} which is the last counter and all the previous counter were received ok!. The count is ok!",
                                                     Configuration.Name, counter, clOrderId), Constants.MessageType.AssertOk);
                                ClearOrderTracking(clOrderId);
                            }
                            else
                            {
                                if (counter == (lastCounter + 1))
                                    LastProcessedCounter[clOrderId] = counter;
                                else
                                {
                                    DoLog(string.Format("<{0}>- Recevied counter #{1} and expected #{2} for cl. order id {3} . Wrong counter number!",
                                                     Configuration.Name, counter,lastCounter+1, clOrderId), Constants.MessageType.AssertFailed);
                                    ClearOrderTracking(clOrderId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            DoLog(string.Format("<{0}>- Received an order Id with a counter that doesn't seem to be an int value: {1}", Configuration.Name, erWrapper.GetField(ExecutionReportFields.Account)), Constants.MessageType.AssertFailed);
                            OrdersSent.Remove(clOrderId);
                        }
                      
                    }
                    //This must be an order we don't manage or that we have already rejected so the counter is considered to be not working
                }

            }
            catch (Exception ex)
            {
                DoLog(string.Format("<{0}>-Critical error processing a execution report:{1}", Configuration.Name, ex.Message), Constants.MessageType.AssertFailed);
            }
        }

        //Here we will process the rejected messages and see if that's ok or not
        protected void ProcessReject(object param)
        {
            try
            {

                lock (TimeoutOrders)
                {
                    Wrapper rejectWrapper = (Wrapper)param;

                    string key = (string)rejectWrapper.GetField(RejectFields.KEY);
                    string text = (string)rejectWrapper.GetField(RejectFields.Text);

                    if (TimeoutOrders.ContainsKey(key))
                        TimeoutOrders.Remove(key);

                    //Implement rejects
                    if (OrdersSent.ContainsKey(key))
                    {
                        DoLog(string.Format("<{0}>-Received reject message for Cl. Order Id {1}:{2}", Configuration.Name, key, text), Constants.MessageType.AssertFailed);
                        OrdersSent.Remove(key);
                    }
                }

            }
            catch (Exception ex)
            {
                DoLog(string.Format("{0}-Critical error processing a reject:{1}", Configuration.Name, ex.Message), Constants.MessageType.Error);
            }
        }

        public void RunTestsThread()
        {
            try
            {
                while (true)
                {
                    //1- Send orders and get ready to validate counters
                    DoLog(string.Format("Sending full regular order @{0}", Configuration.Name), Constants.MessageType.Information);
                    Order fullOrder1 = OrderBuilder.BuildFullOrder();
                    NewOrderWrapper fullOrderWrapper = new NewOrderWrapper(fullOrder1, Configuration);
                    OrdersSent.Add(fullOrder1.ClOrdId, fullOrder1);
                    TimeoutOrders.Add(fullOrder1.ClOrdId, DateTime.Now);
                    LastProcessedCounter.Add(fullOrder1.ClOrdId, 0);
                    DoPublishMessage(fullOrderWrapper);

                    Thread.Sleep(1000*Configuration.PauseBetweenOrdersInSeconds);//1 second
                }

            }
            catch (Exception ex)
            {
                DoLog(string.Format("{0}-Critical error running tests:{1}", Configuration.Name, ex.Message), Constants.MessageType.Error);
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
                                string received = "(none)";
                                if (LastProcessedCounter.ContainsKey(key))
                                    received = LastProcessedCounter[key].ToString();

                                DoLog(string.Format("<{0}>- TIMEOUT waiting for ERs for ClOrdId {1}. Received {2} - Expected {3}",
                                                        Configuration.Name, key, received,Configuration.ExecutionReportsPerOrder),
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
                    OrdersSent = new Dictionary<string, Order>();
                    TimeoutOrders = new Dictionary<string, DateTime>();
                    LastProcessedCounter = new Dictionary<string, int>();

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

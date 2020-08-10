using BaseTestingModule.Common.Wrappers.Order_Routing;
using BaseTestingModule.LogicLayer.Uitl.Builder;
using FIXCounterTesterModule.Common.Configuration;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.Common.Wrappers;
using Fwk.Main.LogicLayer.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FIXCounterTesterModule
{
    class FIXCounterTesterModule : BaseTestingModule.BaseTestingModule
    {
        #region Protected Attributes

        protected Configuration Configuration { get; set; }

        protected Dictionary<string, Order> OrdersSent { get; set; }

        protected int? LastProcessedCounter { get; set; } 

        #endregion

        #region Protected Methods

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


                    if (TimeoutOrders.ContainsKey(key))
                        TimeoutOrders.Remove(key);

                    if (OrdersSent.ContainsKey(key))
                    {

                        if (orderId.Contains("_"))
                        {
                            DoLog(string.Format("<{0}>- Received order id {1}", Configuration.Name, orderId), Constants.MessageType.AssertOk);

                            string strCount = orderId.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];


                            try
                            {
                                int count = Convert.ToInt32(strCount);

                                //We validate that it's lower. No that it has a specific number
                                if (LastProcessedCounter.HasValue && LastProcessedCounter.Value < count)
                                {
                                    DoLog(string.Format("<{0}>- Recevied an orderId counter {1} which is bigger than the number we had in memory. The count seems ok",
                                        Configuration.Name, count, LastProcessedCounter.HasValue ? LastProcessedCounter.Value.ToString() : "-"), Constants.MessageType.AssertOk);
                                    LastProcessedCounter = count;
                                }
                                else
                                {
                                    if (LastProcessedCounter.HasValue)
                                    {
                                        DoLog(string.Format("<{0}>- Recevied an orderId counter {1} which is LOWER than the number we had in memory. What happened with the counter?",
                                             Configuration.Name, count, LastProcessedCounter.HasValue ? LastProcessedCounter.Value.ToString() : "-"), Constants.MessageType.AssertFailed);

                                        OrdersSent.Remove(key);
                                    }
                                    else
                                        LastProcessedCounter = count;
                                }

                            }
                            catch (Exception ex)
                            {
                                DoLog(string.Format("<{0}>- Received an order Id with a counter that doesn't seem to be an int value: {1}", Configuration.Name, strCount), Constants.MessageType.AssertFailed);
                                OrdersSent.Remove(key);
                            }
                        }
                        else
                        {
                            DoLog(string.Format("<{0}>- Received an order Id whose format doesn't match the exepected one: {1}", Configuration.Name, orderId), Constants.MessageType.AssertFailed);
                            OrdersSent.Remove(key);
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
                        //Validar que el error es por Extra Tags
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

                //1- Send orders and get ready to validate counters
                DoLog(string.Format("Sending full regular order @{0}", Configuration.Name), Constants.MessageType.Information);
                Order fullOrder1 = OrderBuilder.BuildFullOrder();
                NewOrderWrapper fullOrderWrapper = new NewOrderWrapper(fullOrder1, Configuration);
                OrdersSent.Add(fullOrder1.ClOrdId, fullOrder1);
                TimeoutOrders.Add(fullOrder1.ClOrdId, DateTime.Now);
                DoPublishMessage(fullOrderWrapper);

                Order fullOrder2 = OrderBuilder.BuildFullOrder();
                NewOrderWrapper fullOrderWrapper2 = new NewOrderWrapper(fullOrder2, Configuration);
                OrdersSent.Add(fullOrder2.ClOrdId, fullOrder1);
                TimeoutOrders.Add(fullOrder2.ClOrdId, DateTime.Now);
                DoPublishMessage(fullOrderWrapper2);

                Order fullOrder3 = OrderBuilder.BuildFullOrder();
                NewOrderWrapper fullOrderWrapper3 = new NewOrderWrapper(fullOrder3, Configuration);
                OrdersSent.Add(fullOrder3.ClOrdId, fullOrder3);
                TimeoutOrders.Add(fullOrder3.ClOrdId, DateTime.Now);
                DoPublishMessage(fullOrderWrapper3);

            


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
                    OrdersSent = new Dictionary<string, Order>();
                    TimeoutOrders = new Dictionary<string, DateTime>();

                    Thread runTestsThread = new Thread(RunTestsThread);
                    runTestsThread.Start();

                    Thread processTimeouts = new Thread(ProcessTimeouts);
                    processTimeouts.Start(new object[] { Configuration.Name, Configuration.TimeoutInSeconds });

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

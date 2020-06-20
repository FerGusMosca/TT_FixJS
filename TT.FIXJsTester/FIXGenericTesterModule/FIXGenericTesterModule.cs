using FIXGenericTesterModule.Common.Configuration;
using FIXGenericTesterModule.Common.Uitl.Builder;
using FIXGenericTesterModule.Common.Wrappers.Order_Routing;
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

namespace FIXGenericTesterModule
{
    public class FIXGenericTesterModule : BaseModule
    {

        #region Protected Attributes

       protected Configuration Configuration { get; set; }

       protected Dictionary<string, DateTime> TimeoutOrders { get; set; }

       protected Dictionary<string, string> MissingTags { get; set; }

       protected Dictionary<string, object> MessageOk { get; set; }

       protected Dictionary<string, string> ExtraTags { get; set; }

        #endregion


        #region Protected Methods

       protected void ProcessTimeouts()
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
                           DateTime start = TimeoutOrders[key];

                           TimeSpan elapsed = DateTime.Now - start;

                           if (elapsed.TotalSeconds > 3)
                           {
                               DoLog(string.Format("{0}-TIMEOUT waiting for response for for Order ClOrdId = {1}. ", Configuration.Name,key),Constants.MessageType.AssertFailed);
                               toRemove.Add(key);
                           }

                       }

                       toRemove.ForEach(x => TimeoutOrders.Remove(x));
                   }

                   Thread.Sleep(5 * 1000);
               }
           }
           catch (Exception ex)
           {
               DoLog(string.Format("{0}-Critical error processing timeouts}:{1}", Configuration.Name, ex.Message), Constants.MessageType.Error);
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

                   if (MessageOk.ContainsKey(key))
                   {
                       string refTag = (string)rejectWrapper.GetField(RejectFields.RefTagID);
                       //We expected the message to be ok but it was ejected
                       DoLog(string.Format("{0}-Test case FAILED for Order ClOrdId = {1}. We expected the order to be ok but it was rejected. Reason={2} Tag={3}", Configuration.Name, key, text, refTag != null ? refTag : "??"),
                             Constants.MessageType.AssertFailed);
                   }

                   if (MissingTags.ContainsKey(key))
                   {
                       string refTag = (string)rejectWrapper.GetField(RejectFields.RefTagID);
                       if (MissingTags[key] == refTag)
                           DoLog(string.Format("{0}-Test case OK for ClOrdId ={1}. Extected to have a message rejected for  missing tag ={2} and it was rejected for that tag", Configuration.Name, key, refTag), Constants.MessageType.AssertOk);
                       else
                           DoLog(string.Format("{0}-Test case FAILED for ClOrdId ={1}. Extected to have a message rejected for  missing tag ={2} but it was rejected for tag {3}", Configuration.Name, key, MissingTags[key], refTag), Constants.MessageType.AssertFailed);
                   }

                   if (ExtraTags.ContainsKey(key))
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

                //1- Ok order
                DoLog(string.Format("Sending full regular order @{0}", Configuration.Name), Constants.MessageType.Information);
                Order fullOrder = OrderBuilder.BuildFullOrder();
                NewOrderWrapper fullOrderWrapper = new NewOrderWrapper(fullOrder, Configuration);
                MessageOk.Add(fullOrder.ClOrdId, fullOrder);
                TimeoutOrders.Add(fullOrder.ClOrdId, DateTime.Now);
                DoPublishMessage(fullOrderWrapper);

                //2- Missing Mandatory tag test
                DoLog(string.Format("Sending missing tag order @{0}", Configuration.Name), Constants.MessageType.Information);
                Order missingTagOrder = OrderBuilder.BuildOrderMissingAccount();
                NewOrderWrapper missingTagOrderWrapper = new NewOrderWrapper(missingTagOrder, Configuration);
                MissingTags.Add(missingTagOrder.ClOrdId, "1");//ClOrdId
                TimeoutOrders.Add(missingTagOrder.ClOrdId, DateTime.Now);
                DoPublishMessage(missingTagOrderWrapper);
            }
            catch (Exception ex)
            {
                DoLog(string.Format("{0}-Critical error running tests:{1}",Configuration.Name,ex.Message), Constants.MessageType.Error);
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
                    MissingTags = new Dictionary<string, string>();
                    MessageOk = new Dictionary<string, object>();
                    ExtraTags = new Dictionary<string, string>();
                    TimeoutOrders = new Dictionary<string, DateTime>();

                    Thread runTestsThread = new Thread(RunTestsThread);
                    runTestsThread.Start();

                    Thread processTimeouts = new Thread(ProcessTimeouts);
                    processTimeouts.Start();

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

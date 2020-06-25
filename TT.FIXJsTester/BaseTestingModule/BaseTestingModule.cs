using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Util;
using Fwk.Main.LogicLayer.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseTestingModule
{
    public abstract class BaseTestingModule : BaseModule
    {
        #region Protected Attributes

        protected Dictionary<string, DateTime> TimeoutOrders { get; set; }

        #endregion


        #region Protected Methods

        protected void ProcessTimeouts(object param)
        {
            string moduleName = (string)param;
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
                                DoLog(string.Format("{0}-TIMEOUT waiting for response for for Order ClOrdId = {1}. ", moduleName, key), Constants.MessageType.AssertFailed);
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
                DoLog(string.Format("{0}-Critical error processing timeouts}:{1}", moduleName, ex.Message), Constants.MessageType.Error);
            }
        }

        #endregion
    }
}

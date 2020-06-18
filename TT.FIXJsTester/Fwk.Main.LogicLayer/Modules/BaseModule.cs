using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.LogicLayer.Modules
{
    public abstract class BaseModule : ICommunicationModule
    {
        #region ICommunicationModule 

        public abstract Common.DTO.CMState ProcessMessage(Common.Wrappers.Wrapper wrapper);
        public abstract bool Initialize(OnMessageReceived pOnMessageRcv, OnLogMessage pOnLogMsg, string configFile);

        public abstract void DoLoadConfig(string configFile, List<string> noValueFields);

        #endregion

        #region Protected Attributes

        public string ModuleConfigFile { get; set; }

        protected OnLogMessage OnLogMsg { get; set; }

        protected OnMessageReceived OnMessageRcv { get; set; }

        protected IConfiguration Config { get; set; }

        #endregion

        #region Protected Methods

        public void DoLog(string msg, Constants.MessageType type)
        {
            if (OnLogMsg != null)
                OnLogMsg(msg, type);
        }

        public bool LoadConfig(string configFile)
        {
            DoLog(DateTime.Now.ToString() + "StrategyBase.LoadConfig", Constants.MessageType.Information);

            DoLog("Loading config:" + configFile, Constants.MessageType.Information);
            if (!File.Exists(configFile))
            {
                DoLog(configFile + " does not exists", Constants.MessageType.Error);
                return false;
            }

            List<string> noValueFields = new List<string>();
            DoLog("Processing config:" + configFile, Constants.MessageType.Information);
            try
            {
                DoLoadConfig(configFile, noValueFields);
                DoLog("Ending GetConfiguracion " + configFile, Constants.MessageType.Information);
            }
            catch (Exception e)
            {
                DoLog("Error recovering config " + configFile + ": " + e.Message, Constants.MessageType.Error);
                return false;
            }

            if (noValueFields.Count > 0)
                noValueFields.ForEach(s => DoLog(string.Format(Constants.FieldMissing, s), Constants.MessageType.Error));

            return true;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;

namespace Fwk.Main.Common.Abstract
{
    public abstract class BaseCommunicationModule : ICommunicationModule
    {
        #region Public Attributes

        protected object tLock = new object();

        protected OnMessageReceived OnMessageRcv { get; set; }

        protected OnLogMessage OnLogMsg { get; set; }

        public BaseConfiguration Config { get; set; }

        #endregion


        #region Public Abstract Mehtods

        protected abstract void DoLoadConfig(string configFile, List<string> listaCamposSinValor);

        public abstract DTO.CMState ProcessMessage(Wrappers.Wrapper wrapper);

        public abstract bool Initialize(OnMessageReceived pOnMessageRcv, OnLogMessage pOnLogMsg, string configFile);

        #endregion

        #region Public Methods

        protected void DoLog(string msg, Constants.MessageType type)
        {
            if (OnLogMsg != null)
                OnLogMsg(msg, type);
        }

        protected bool LoadConfig(string configFile)
        {
            DoLog(DateTime.Now.ToString() + string.Format("BaseCommunicationModule.LoadConfig"), Constants.MessageType.Information);

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

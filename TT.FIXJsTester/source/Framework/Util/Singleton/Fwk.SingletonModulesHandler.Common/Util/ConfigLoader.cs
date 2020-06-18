using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Util;
using Fwk.SingletonModulesHandler.Common.Interfaces;

namespace Fwk.SingletonModulesHandler.Common.Util
{
    public class ConfigLoader
    {
        public static bool DoLoadConfig(ISingletonModule module, string configFile)
        {
            module.DoLog(DateTime.Now.ToString() + string.Format("ConfigLoader.LoadConfig"), Constants.MessageType.Information);

            module.DoLog("Loading config:" + configFile, Constants.MessageType.Information);
            if (!File.Exists(configFile))
            {
                module.DoLog(configFile + " does not exists", Constants.MessageType.Error);
                return false;
            }

            List<string> noValueFields = new List<string>();
            module.DoLog("Processing config:" + configFile, Constants.MessageType.Information);
            try
            {
                module.DoLoadConfig(configFile, noValueFields);
                module.DoLog("Ending GetConfiguracion " + configFile, Constants.MessageType.Information);
            }
            catch (Exception e)
            {
                module.DoLog("Error recovering config " + configFile + ": " + e.Message, Constants.MessageType.Error);
                return false;
            }

            if (noValueFields.Count > 0)
                noValueFields.ForEach(s => module.DoLog(string.Format(Constants.FieldMissing, s), Constants.MessageType.Error));

            return true;
        }
    }
}

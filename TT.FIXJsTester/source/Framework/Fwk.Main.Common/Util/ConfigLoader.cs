using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Interfaces;

namespace Fwk.Main.Common.Util
{
    public class ConfigLoader
    {
        public static bool LoadConfig(ILogger logger, string configFile)
        {
            logger.DoLog(DateTime.Now.ToString() + "Fwk.Main.Common.Util.ConfigLoader.LoadConfig", Constants.MessageType.Information);

            logger.DoLog("Loading config:" + configFile, Constants.MessageType.Information);
            if (!File.Exists(configFile))
            {
                logger.DoLog(configFile + " does not exists", Constants.MessageType.Error);
                return false;
            }

            List<string> noValueFields = new List<string>();
            logger.DoLog("Processing config:" + configFile, Constants.MessageType.Information);
            try
            {
                logger.DoLoadConfig(configFile, noValueFields);
                logger.DoLog("Ending GetConfiguracion " + configFile, Constants.MessageType.Information);
            }
            catch (Exception e)
            {
                logger.DoLog("Error recovering config " + configFile + ": " + e.Message, Constants.MessageType.Error);
                return false;
            }

            if (noValueFields.Count > 0)
                noValueFields.ForEach(s => logger.DoLog(string.Format(Constants.FieldMissing, s), Constants.MessageType.Error));

            return true;
        }
    }
}

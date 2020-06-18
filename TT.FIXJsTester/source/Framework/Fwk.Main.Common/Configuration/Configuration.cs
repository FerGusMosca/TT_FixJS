using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;

namespace Fwk.Main.Common.Configuration
{
    [Serializable]
    public class Configuration : BaseConfiguration,IConfiguration
    {
        #region Private Static Consts

        public static string _LOG_LEVEL_INFO = "INFO";
        public static string _LOG_LEVEL_ERROR = "ERROR";
        public static string _LOG_LEVEL_DEBUG = "DEBUG";

        //INFO: Info
        //ERROR:  Info + ERROR
        //DEBUG: All

        #endregion

        #region Public Attributes

        public string Name { get; set; }

        public string OutgoingConfigPath { get; set; }

        public string IncomingConfigPath { get; set; }

        public string IncomingModule { get; set; }

        public string OutgoingModule { get; set; }

        public bool LogToConsole { get; set; }

        public string LogLevel {get;set;}

        #endregion

        #region Private Methods

        private void LogEvent(Type type,string mssg)
        {
            if (Name != null)
                EventLog.WriteEntry(Name, mssg, System.Diagnostics.EventLogEntryType.Information);
            else
                EventLog.WriteEntry(type.ToString(), mssg, System.Diagnostics.EventLogEntryType.Information);
        }

        public override bool CheckDefaults(List<string> result)
        {
            bool resultado = true;

            if (string.IsNullOrEmpty(Name))
            {
                result.Add("Name");
                resultado = false;
            }

            if (string.IsNullOrEmpty(LogLevel))
            {
                result.Add("LogLevel");
                resultado = false;
            }

            return resultado;
        }

        #endregion
    }
}

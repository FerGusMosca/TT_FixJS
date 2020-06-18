using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;

namespace Fwk.Main.Common.Abstract
{
    public abstract class BaseConfiguration:IConfiguration
    {
        #region Public Attributes

        public string Name { get; set; }

        #endregion

        #region Private Method

        private void LogEvent(Type type, string mssg)
        {
            //if (Name != null)
            //    EventLog.WriteEntry(Name, mssg, System.Diagnostics.EventLogEntryType.Information);
            //else
            //    EventLog.WriteEntry(type.ToString(), mssg, System.Diagnostics.EventLogEntryType.Information);
        }

        #endregion

        #region Abstract Methods

        public abstract bool CheckDefaults(List<string> result);

        #endregion


        #region Public Methods

        public T GetConfiguration<T>(string configFile, List<string> listaErrs)
        {

            LogEvent(this.GetType(), "Creating Serializer");
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            FileStream myFileStream = null;
            T config;
            try
            {
                try
                {
                    LogEvent(this.GetType(), "Creating filestream");
                    myFileStream = new FileStream(configFile, FileMode.Open);
                    LogEvent(this.GetType(), "Deserializing Config");
                    config = (T)mySerializer.Deserialize(myFileStream);

                    if (!((IConfiguration)config).CheckDefaults(listaErrs))
                        throw new InvalidOperationException(string.Format(Constants.MissingConfigParam, listaErrs.FirstOrDefault()));
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(String.Format(Constants.XmlInvalid, e.Message));
                }
            }
            finally
            {
                if (myFileStream != null)
                    myFileStream.Close();
            }

            return config;
        }

        #endregion
    }
}

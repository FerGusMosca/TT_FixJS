using FIXCommModule.Common.DTOs;
using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIXCommModule.Common.Configuration
{
    public class Configuration : BaseConfiguration, IConfiguration
    {

        #region Public Methods

        public string InitiatorCfg { get; set; }

        [XmlArray(ElementName = "TestingModules")]
        [XmlArrayItem(ElementName = "Module")]
        public List<Module> TestingModules { get; set; }

        #endregion

        #region Public Methods

        public override bool CheckDefaults(List<string> result)
        {
            bool resp = true;

            if (string.IsNullOrEmpty(InitiatorCfg))
            {
                result.Add("InitiatorCfg");
                resp = false;
            }


            return resp;
        }

        #endregion

    }
}

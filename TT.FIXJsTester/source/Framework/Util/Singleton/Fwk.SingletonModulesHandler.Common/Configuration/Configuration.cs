using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Interfaces;

namespace Fwk.SingletonModulesHandler.Common.Configuration
{
    public class Configuration : BaseConfiguration, IConfiguration
    {
        #region Public Attributes

        public string SingletonAssembly { get; set; }

        public string SingletonConfigFile { get; set; }

        public string ModuleDirection { get; set; }

        #endregion

        #region Public Methods

        public override bool CheckDefaults(List<string> result)
        {
            bool resultado = true;

            if (string.IsNullOrEmpty(SingletonAssembly))
            {
                result.Add("SingletonAssembly");
                resultado = false;
            }

            if (string.IsNullOrEmpty(SingletonConfigFile))
            {
                result.Add("SingletonConfigFile");
                resultado = false;
            }

            if (string.IsNullOrEmpty(ModuleDirection))
            {
                result.Add("ModuleDirection");
                resultado = false;
            }

            return resultado;
        }

        #endregion
    }
}

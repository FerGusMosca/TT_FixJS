using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXGenericTesterModule.Common.Configuration
{
    public class Configuration : BaseConfiguration, IConfiguration
    {
        #region Public Methods
       

        #endregion

        #region Public Methods

        public override bool CheckDefaults(List<string> result)
        {
            bool resp = true;


            return resp;
        }

        #endregion
    }
}

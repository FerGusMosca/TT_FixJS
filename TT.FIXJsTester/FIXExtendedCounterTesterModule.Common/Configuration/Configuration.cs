using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXExtendedCounterTesterModule.Common.Configuration
{
    public class Configuration : BaseConfiguration, IConfiguration
    {
        #region Public Methods


        public int PauseBetweenOrdersInSeconds { get; set; }

        public int ExecutionReportsPerOrder { get; set; }

        public int TimeoutInSeconds { get; set; }


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

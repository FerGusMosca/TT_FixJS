using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class TradingSessionRule
    {
        #region Public Attributes

        public List<OrdTypeRule> OrdTypeRules { get; set; }

        public List<TimeInForceRule> TimeInForceRules { get; set; }

        public List<ExecInstRule> ExecInstRules { get; set; }

        #endregion
    }
}

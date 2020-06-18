using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class SecurityTradingRule
    {
        #region Public Attributes

        public BaseTradingRule BaseTradingRule { get; set; }

        public List<TradingSessionRuleGrp> TradingSessionRulesGrp { get; set; }

        public TradingSessionRule TradingSessionRules { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class BaseTradingRule
    {
        #region Public Attributes

        public double? MaxTradeVol { get; set; }

        public double? MinTradeVol { get; set; }

        public List<LotTypeRule> LotTypeRules { get; set; }

        public PriceLimit PriceLimit { get; set; }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class TradeListFields:Fields
    {
        public static readonly TradeListFields Trades = new TradeListFields(2);
        public static readonly TradeListFields PositionId = new TradeListFields(3);

        protected TradeListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

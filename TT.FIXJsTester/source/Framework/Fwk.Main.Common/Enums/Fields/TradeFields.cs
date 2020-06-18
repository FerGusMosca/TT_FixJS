using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class TradeFields : Fields
    {
        public static readonly TradeFields Trade = new TradeFields(2);
        public static readonly TradeFields PositionId = new TradeFields(3);

        protected TradeFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

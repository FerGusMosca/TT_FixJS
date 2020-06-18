using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class OrderBookFields : Fields
    {
        public static readonly OrderBookFields NoMDEntries = new OrderBookFields(2);


        public static readonly OrderBookFields Symbol = new OrderBookFields(3);

        public static readonly OrderBookFields Bids = new OrderBookFields(4);

        public static readonly OrderBookFields Asks = new OrderBookFields(5);

        public static readonly OrderBookFields MDMkt = new OrderBookFields(6);

        protected OrderBookFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

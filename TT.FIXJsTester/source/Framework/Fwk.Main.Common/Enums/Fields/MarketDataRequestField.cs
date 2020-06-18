using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class MarketDataRequestField : Fields
    {
        public static readonly MarketDataRequestField Symbol = new MarketDataRequestField(2);
        public static readonly MarketDataRequestField Exchange = new MarketDataRequestField(3);
        public static readonly MarketDataRequestField SecurityType = new MarketDataRequestField(4);
        public static readonly MarketDataRequestField SubscriptionRequestType = new MarketDataRequestField(5);
        public static readonly MarketDataRequestField Currency = new MarketDataRequestField(6);
        public static readonly MarketDataRequestField MDReqId = new MarketDataRequestField(7);



        protected MarketDataRequestField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

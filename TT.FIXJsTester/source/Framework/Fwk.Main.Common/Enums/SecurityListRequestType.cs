using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum SecurityListRequestType
    {
        Symbol=0,
        SecurityType = 1,//SecurityType and/or CFICode
        Product=2,
        TradingSessionID=3,
        AllSecurities=4,
        MarketID = 5,//MarketID or MarketID + MarketSegmentID
    }
}

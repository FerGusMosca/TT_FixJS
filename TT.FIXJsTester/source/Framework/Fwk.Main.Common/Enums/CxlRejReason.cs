using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum CxlRejReason
    {
        TooLateToCancel = 0,
        UnknownOrder = 1,
        BrokerExchangeOption = 2,
        OrderAlreadyPendingCancelOrPendingReplace = 3,
        UnableProcessOrderMassCancelRequest = 4,
        OrigOrdModTimeDidNotMatchLastTransactTime = 5,
        DuplicateCLOrdId = 6,
        Other = 99,
        InvalidPriceIncrement = 18,
        PriceExceedsCurrentPrice = 7,
        PriceExceedsCurrentPriceBand = 8
    }
}

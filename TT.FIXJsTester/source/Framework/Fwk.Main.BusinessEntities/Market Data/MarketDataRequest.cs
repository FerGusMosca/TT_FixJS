using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.BusinessEntities.Market_Data
{
    public class MarketDataRequest
    {
        #region Public Attributes

        public int ReqId { get; set; }

        public Security Security { get; set; }

        public SubscriptionRequestType SubscriptionRequestType { get; set; }


        #endregion
    }
}

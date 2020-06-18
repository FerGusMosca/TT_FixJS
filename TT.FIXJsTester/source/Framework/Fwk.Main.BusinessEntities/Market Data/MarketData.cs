using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.BusinessEntities.Market_Data
{
    public class MarketData
    {
        #region Public Attributes

        #region Prices
        public double? OpeningPrice { get; set; }
        public double? ClosingPrice { get; set; }
        public double? SettlementPrice { get; set; }
        public double? TradingSessionHighPrice { get; set; }
        public double? TradingSessionLowPrice { get; set; }
        public double? TradingSessionVWAPPrice { get; set; }
        public double? Imbalance { get; set; }
        public double? TradeVolume { get; set; }
        public double? OpenInterest { get; set; }
        public double? CompositeUnderlyingPrice { get; set; }
        public double? MarginRate { get; set; }
        public double? MidPrice { get; set; }
        public double? SettleHighPrice { get; set; }
        public double? SettlPriorPrice { get; set; }
        public double? SessionHighBid { get; set; }
        public double? SessionLowOffer { get; set; }
        public double? EarlyPrices { get; set; }
        public double? AuctionClearingPrice { get; set; }
        public double? Trade { get; set; }

        public double? BestBidPrice { get; set; }
        public long? BestBidSize { get; set; }
        public string BestBidExch { get; set; }
        public double? BestAskPrice { get; set; }
        public long? BestAskSize { get; set; }
        public string BestAskExch { get; set; }

        public decimal? BestBidCashSize { get; set; }
        public decimal? BestAskCashSize { get; set; }

        public double? CashVolume { get; set; }
        public double? NominalVolume { get; set; }

        #endregion

        public UpdateAction MDUpdateAction { get; set; }
        public string Currency { get; set; }
        public DateTime? MDEntryDate { get; set; }
        public DateTime? MDLocalEntryDate { get; set; }
        public TickDirection TickDirection { get; set; }
        public double? MDTradeSize { get; set; }

        public SettlType SettlType { get; set; }
        public DateTime? SettlDate { get; set; }

        public DateTime? LastTradeDateTime { get; set; }

        public Security Security { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class MarketDataFields : Fields
    {
        public static readonly Fields NoMDEntries = new MarketDataFields(2);

        #region Prices
        public static readonly MarketDataFields OpeningPrice = new MarketDataFields(3);
        public static readonly MarketDataFields ClosingPrice = new MarketDataFields(4);
        public static readonly MarketDataFields SettlementPrice = new MarketDataFields(5);
        public static readonly MarketDataFields TradingSessionHighPrice = new MarketDataFields(6);
        public static readonly MarketDataFields TradingSessionLowPrice = new MarketDataFields(7);
        public static readonly MarketDataFields TradingSessionVWAPPrice = new MarketDataFields(8);
        public static readonly MarketDataFields Imbalance = new MarketDataFields(9);
        public static readonly MarketDataFields TradeVolume = new MarketDataFields(10);
        public static readonly MarketDataFields OpenInterest = new MarketDataFields(11);
        public static readonly MarketDataFields CompositeUnderlyingPrice = new MarketDataFields(12);
        public static readonly MarketDataFields MarginRate = new MarketDataFields(13);
        public static readonly MarketDataFields MidPrice = new MarketDataFields(14);
        public static readonly MarketDataFields SettleHighPrice = new MarketDataFields(15);
        public static readonly MarketDataFields SettlPriorPrice = new MarketDataFields(16);
        public static readonly MarketDataFields SessionHighBid = new MarketDataFields(17);
        public static readonly MarketDataFields SessionLowOffer = new MarketDataFields(18);
        public static readonly MarketDataFields EarlyPrices = new MarketDataFields(19);
        public static readonly MarketDataFields AuctionClearingPrice = new MarketDataFields(20);
        public static readonly MarketDataFields Trade = new MarketDataFields(20);
        #endregion

        public static readonly MarketDataFields MDUpdateAction = new MarketDataFields(22);
        public static readonly MarketDataFields Currency = new MarketDataFields(23);
        public static readonly MarketDataFields MDEntryDate = new MarketDataFields(24);
        public static readonly MarketDataFields MDEntryTime = new MarketDataFields(25);
        public static readonly MarketDataFields TickDirection = new MarketDataFields(26);

        public static readonly MarketDataFields SettlType = new MarketDataFields(27);
        public static readonly MarketDataFields SettlDate = new MarketDataFields(28);

        public static readonly MarketDataFields Symbol = new MarketDataFields(29);
        public static readonly MarketDataFields SecurityType = new MarketDataFields(30);

        public static readonly MarketDataFields MDMkt = new MarketDataFields(31);

        public static readonly MarketDataFields BestBidPrice = new MarketDataFields(32);
        public static readonly MarketDataFields BestAskPrice = new MarketDataFields(33);
        public static readonly MarketDataFields BestAskSize = new MarketDataFields(34);
        public static readonly MarketDataFields BestBidSize = new MarketDataFields(35);
        public static readonly MarketDataFields BestAskExch = new MarketDataFields(35);
        public static readonly MarketDataFields BestBidExch = new MarketDataFields(36);
        public static readonly MarketDataFields MDTradeSize = new MarketDataFields(37);

        public static readonly MarketDataFields MDLocalEntryDate = new MarketDataFields(38);

        public static readonly MarketDataFields ReverseMarketData = new MarketDataFields(39);

        public static readonly MarketDataFields BestBidCashSize = new MarketDataFields(40);
        public static readonly MarketDataFields BestAskCashSize = new MarketDataFields(41);

        public static readonly MarketDataFields LastTradeDateTime = new MarketDataFields(42);
        
        protected MarketDataFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Market_Data;
using Fwk.Main.Common.Enums;
using Newtonsoft.Json;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class Security
    {
        #region Private Static Consts

        private static string _OTH="OTH";
        private static string _CASH="CASH";
        private static string _CS="CS";
        private static string _FUT="FUT";
        private static string _IND="IND";
        private static string _OPT="OPT";
        private static string _TB="TB";
        private static string _TBOND="TBOND";
        private static string _CB="CB";
        private static string _="";


        #endregion

        #region Public Methods

        public string Symbol { get; set; }

        public string AltIntSymbol { get; set; }

        public string SecurityDesc { get; set; }

        public SecurityType SecType { get; set; }

        public string Currency { get; set; }

        public string Exchange { get; set; }


        [JsonIgnore]
        public MarketData MarketData { get; set; }

        public Halted? Halted { get; set; }

        public bool Active { get; set; }

        #region Option Attributes

        public double? StrikePrice { get; set; }

        public DateTime? MaturityDate { get; set; }

        public string MaturityMonthYear { get; set; }

        public string SymbolSfx { get; set; }

        public string StrikeCurrency { get; set; }

        public string PutOrCall { get; set; }

        [JsonIgnore]
        public int StrikeMultiplier { get; set; }

        #endregion

        #region Contract Attributes

        public string UnderlyingSymbol { get; set; }

        public double? Factor { get; set; }

        public string CFICode { get; set; }

        public double? ContractMultiplier { get; set; }

        public double? MinPriceIncrement { get; set; }

        public double? TickSize { get; set; }

        public int? InstrumentPricePrecision { get; set; }

        public int? InstrumentSizePrecision { get; set; }

        public FinancingDetail FinancingDetails { get; set; }

        public SecurityTradingRule SecurityTradingRule { get; set; }

        public long? ContractPositionNumber { get; set; }

        public double? MarginRatio { get; set; }

        public decimal? ContractSize { get; set; }

        #endregion

        #region CryptoCurrency Attributes

        [JsonIgnore]
        public bool ReverseMarketData { get; set; }

        #endregion

        #endregion

        #region Constructors

        public Security() 
        {
            MarketData = new MarketData();

            Active = true;
        }

        #endregion

        #region Public Methods

        public Security Clone(string newSymbol)
        {
            Security cloned = new Security();

            cloned.Symbol = newSymbol;
            cloned.SecType = SecType;
            cloned.Exchange = Exchange;

            return cloned;
        }

        #endregion

        #region Public Static Methods

        public  string GetStrSecurityType()
        {
            if (SecType == SecurityType.OTH)
                return _OTH;
            else if (SecType == SecurityType.CASH)
                return _CASH;
            else if (SecType == SecurityType.CS)
                return _CS;
            else if (SecType == SecurityType.FUT)
                return _FUT;
            else if (SecType == SecurityType.IND)
                return _IND;
            else if (SecType == SecurityType.OPT)
                return _OPT;
            else if (SecType == SecurityType.TB)
                return _TB;
            else if (SecType == SecurityType.TBOND)
                return _TBOND;
            else if (SecType == SecurityType.CB)
                return _CB;
            else
                throw new Exception(string.Format("Unknown security type str translation {0}", SecType));
        }

        public static SecurityType GetSecurityType(string secType)
        {
            if(string.IsNullOrEmpty(secType))
                return SecurityType.OTH;

            if (secType.ToUpper() == _CASH)
                return SecurityType.CASH;
            else if (secType.ToUpper() == _CS)
                return SecurityType.CS;
            else if (secType.ToUpper() == _FUT)
                return SecurityType.FUT;
            else if (secType.ToUpper() == _IND)
                return SecurityType.IND;
            else if (secType.ToUpper() == _OPT)
                return SecurityType.OPT;
            else if (secType.ToUpper() == _TB)
                return SecurityType.TB;
            else if (secType.ToUpper() == _TBOND)
                return SecurityType.TBOND;
            else if (secType.ToUpper() == _OTH)
                return SecurityType.OTH;
            else if (secType.ToUpper() == _CB)
                return SecurityType.CB;
            else
                return SecurityType.OTH;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class SecurityFields : Fields
    {
        public static readonly SecurityFields Symbol = new SecurityFields(2);
        public static readonly SecurityFields SecurityType = new SecurityFields(3);
        public static readonly SecurityFields Currency = new SecurityFields(4);
        public static readonly SecurityFields Exchange = new SecurityFields(5);
        public static readonly SecurityFields MarketData = new SecurityFields(6);
        public static readonly SecurityFields Halted = new SecurityFields(7);
        public static readonly SecurityFields SecurityDesc = new SecurityFields(8);

        #region OptionFields

        public static readonly SecurityFields StrikePrice = new SecurityFields(9);
        public static readonly SecurityFields MaturityDate = new SecurityFields(10);
        public static readonly SecurityFields MaturityMonthYear = new SecurityFields(11);
        public static readonly SecurityFields SymbolSfx = new SecurityFields(12);
        public static readonly SecurityFields StrikeCurrency = new SecurityFields(13);
        public static readonly SecurityFields UnderlyingSymbol = new SecurityFields(14);

        #endregion


        #region Contract Fields

        public static readonly SecurityFields Factor = new SecurityFields(14);
        public static readonly SecurityFields CFICode = new SecurityFields(15);
        public static readonly SecurityFields ContractMultiplier = new SecurityFields(16);
        public static readonly SecurityFields MinPriceIncrement = new SecurityFields(17);
        public static readonly SecurityFields TickSize = new SecurityFields(18);
        public static readonly SecurityFields InstrumentPricePrecision = new SecurityFields(19);
        public static readonly SecurityFields InstrumentSizePrecision = new SecurityFields(20);
        public static readonly SecurityFields FinancingDetails = new SecurityFields(21);
        public static readonly SecurityFields SecurityTradingRule = new SecurityFields(22);
        public static readonly SecurityFields ContractPositionNumber = new SecurityFields(23);
        public static readonly SecurityFields MarginRatio = new SecurityFields(24);
        public static readonly SecurityFields ContractSize = new SecurityFields(25);
       
        #endregion

        protected SecurityFields(int pInternalValue)
            : base(pInternalValue)
        {

        }

    }
}

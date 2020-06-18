using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class PositionFields : Fields
    {
        public static readonly PositionFields Symbol = new PositionFields(2);

        public static readonly PositionFields Exchange = new PositionFields(3);
        public static readonly PositionFields QuantityType = new PositionFields(4);
        public static readonly PositionFields PriceType = new PositionFields(5);
        public static readonly PositionFields Qty = new PositionFields(6);
        public static readonly PositionFields CashQty = new PositionFields(7);
        public static readonly PositionFields Percent = new PositionFields(8);
        public static readonly PositionFields ExecutionReports = new PositionFields(9);
        public static readonly PositionFields Orders = new PositionFields(10);
        public static readonly PositionFields Side = new PositionFields(11);
        public static readonly PositionFields PosId = new PositionFields(12);
        public static readonly PositionFields PositionRejectReason = new PositionFields(13);
        public static readonly PositionFields PositionRejectText = new PositionFields(14);
        public static readonly PositionFields PosStatus = new PositionFields(15);
        public static readonly PositionFields Security = new PositionFields(16);
        public static readonly PositionFields Currency = new PositionFields(17);
        public static readonly PositionFields SecurityType = new PositionFields(18);
        public static readonly PositionFields Account = new PositionFields(19);
        public static readonly PositionFields Suspended = new PositionFields(20);
        public static readonly PositionFields TargetPrice = new PositionFields(21);
        public static readonly PositionFields TargetBigger = new PositionFields(22);
        public static readonly PositionFields BandThreshold1 = new PositionFields(23);
        public static readonly PositionFields LastTradeRefPrice = new PositionFields(24);
        public static readonly PositionFields Broker = new PositionFields(25);
        public static readonly PositionFields Strategy = new PositionFields(26);
        public static readonly PositionFields Instruction = new PositionFields(27);
        public static readonly PositionFields AggressionLevel = new PositionFields(28);
        public static readonly PositionFields CFD = new PositionFields(29);
        public static readonly PositionFields TimeInForce = new PositionFields(30);

        protected PositionFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
        
    }
}

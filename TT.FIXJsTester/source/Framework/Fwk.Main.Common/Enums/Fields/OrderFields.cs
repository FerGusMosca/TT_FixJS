using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class OrderFields : Fields
    {
        public static readonly OrderFields SettlType = new OrderFields(2);
        public static readonly OrderFields SettlDate = new OrderFields(3);
        public static readonly OrderFields Symbol = new OrderFields(4);
        public static readonly OrderFields SecurityType = new OrderFields(5);
        public static readonly OrderFields Currency = new OrderFields(6);
        public static readonly OrderFields Exchange = new OrderFields(7);
        public static readonly OrderFields ClOrdID = new OrderFields(8);
        public static readonly OrderFields OrdType = new OrderFields(9);
        public static readonly OrderFields PriceType = new OrderFields(10);
        public static readonly OrderFields Price = new OrderFields(11);
        public static readonly OrderFields StopPx = new OrderFields(12);
        public static readonly OrderFields ExpireDate = new OrderFields(13);
        public static readonly OrderFields ExpireTime = new OrderFields(14);
        public static readonly OrderFields Side = new OrderFields(15);
        public static readonly OrderFields OrderQty = new OrderFields(16);
        public static readonly OrderFields CashOrderQty = new OrderFields(17);
        public static readonly OrderFields OrderPercent = new OrderFields(18);
        public static readonly OrderFields TimeInForce = new OrderFields(19);
        public static readonly OrderFields MinQty = new OrderFields(20);
        public static readonly OrderFields OrigClOrdID = new OrderFields(21);
        public static readonly OrderFields Account = new OrderFields(22);
        public static readonly OrderFields DecimalPrecission = new OrderFields(23);
        public static readonly OrderFields OrderId = new OrderFields(24);
        public static readonly OrderFields Broker = new OrderFields(25);
        public static readonly OrderFields Strategy = new OrderFields(26);
        public static readonly OrderFields AggressionLevel = new OrderFields(27);
        public static readonly OrderFields CFD = new OrderFields(28);

        protected OrderFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

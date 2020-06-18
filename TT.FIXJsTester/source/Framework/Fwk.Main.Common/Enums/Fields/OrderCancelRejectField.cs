using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class OrderCancelRejectField : Fields
    {
        public static readonly OrderCancelRejectField ClOrdID = new OrderCancelRejectField(2);
        public static readonly OrderCancelRejectField OrigClOrdID = new OrderCancelRejectField(3);
        public static readonly OrderCancelRejectField OrderID = new OrderCancelRejectField(4);
        public static readonly OrderCancelRejectField OrdStatus = new OrderCancelRejectField(5);
        public static readonly OrderCancelRejectField CxlRejResponseTo = new OrderCancelRejectField(6);
        public static readonly OrderCancelRejectField CxlRejReason = new OrderCancelRejectField(7);
        public static readonly OrderCancelRejectField Text = new OrderCancelRejectField(8);

        protected OrderCancelRejectField(int pInternalValue)
            : base(pInternalValue)
        {

        }

    }
}

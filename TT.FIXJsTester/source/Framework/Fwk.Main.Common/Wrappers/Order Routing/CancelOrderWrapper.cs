using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class CancelOrderWrapper:Wrapper
    {
        #region Constructors

        public CancelOrderWrapper(string clOrderId,string orderId)
        {
            ClOrdId = clOrderId;

            OrderId = orderId;
        }

        #endregion

        #region Attributes


        protected string ClOrdId { get; set; }

        protected string OrderId { get; set; }


        #endregion

        #region Public Methods

        public override string ToString()
        {
            return "";
        }

        public override Actions GetAction()
        {
            return Actions.CANCEL_ORDER;
        }


        public override object GetField(Fields field)
        {
            OrderFields xrField = (OrderFields)field;


            if (xrField == OrderFields.ClOrdID)
                return ClOrdId;
            else if (xrField == OrderFields.OrderId)
                return OrderId;
            else
                return OrderFields.NULL;
        }

        #endregion
    }
}

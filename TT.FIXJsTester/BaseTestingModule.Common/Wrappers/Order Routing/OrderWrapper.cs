using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTestingModule.Common.Wrappers.Order_Routing
{
    public abstract class OrderWrapper : Wrapper
    {
        #region Protected Attributes

        protected Order Order { get; set; }

        protected IConfiguration Config { get; set; }

        #endregion

        #region Public Methods

        public override object GetField(Fields field)
        {
            if (field is OrderFields)
            {

                OrderFields oField = (OrderFields)field;

                if (Order == null)
                    return OrderFields.NULL;

                if (oField == OrderFields.ClOrdID)
                    return Order.ClOrdId;
                if (oField == OrderFields.OrigClOrdID)
                    return Order.OrigClOrdId;
                else if (oField == OrderFields.SettlType)
                    return Order.SettlType;
                else if (oField == OrderFields.SettlDate)
                    return Order.SettlDate;
                else if (oField == OrderFields.Symbol)
                    return Order.Security.Symbol;
                else if (oField == OrderFields.SecurityType)
                    return Order.Security.SecType;
                else if (oField == OrderFields.Currency)
                    return Order.Currency;
                else if (oField == OrderFields.Exchange)
                    return Order.Exchange;
                else if (oField == OrderFields.OrdType)
                    return Order.OrdType;
                else if (oField == OrderFields.PriceType)
                    return Order.PriceType;
                else if (oField == OrderFields.Price)
                    return Order.Price;
                else if (oField == OrderFields.StopPx)
                    return Order.StopPx;
                else if (oField == OrderFields.ExpireDate)
                    return Order.ExpireTime.HasValue ? (DateTime?)new DateTime(
                                                                                Order.ExpireTime.Value.Year,
                                                                                Order.ExpireTime.Value.Month,
                                                                                Order.ExpireTime.Value.Day) : null;
                else if (oField == OrderFields.ExpireTime)
                    return Order.ExpireTime.HasValue ? (DateTime?)new DateTime(
                                                                                Order.ExpireTime.Value.Year,
                                                                                Order.ExpireTime.Value.Month,
                                                                                Order.ExpireTime.Value.Day,
                                                                                Order.ExpireTime.Value.Hour,
                                                                                Order.ExpireTime.Value.Minute,
                                                                                Order.ExpireTime.Value.Second) : null;
                else if (oField == OrderFields.Side)
                    return Order.Side;
                else if (oField == OrderFields.OrderQty)
                    return Order.OrderQty;
                else if (oField == OrderFields.CashOrderQty)
                    return Order.CashOrderQty;
                else if (oField == OrderFields.OrderPercent)
                    return Order.OrderPercent;
                else if (oField == OrderFields.TimeInForce)
                    return Order.TimeInForce;
                else if (oField == OrderFields.MinQty)
                    return Order.MinQty;
                else if (oField == OrderFields.Account)
                    return Order.Account;
                else if (oField == OrderFields.DecimalPrecission)
                    return Order.DecimalPrecission;
                else
                    return base.GetField(field);
            }
            else
                return base.GetField(field);
        }

        #endregion

    }
}

using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXCommModule.LogicLayer.Util.Converter
{
    public class OrderConverter
    {
        #region Public Static Methods

        public static Order ConvertNewOrder(Wrapper wrapper)
        {
            OrdType? ordType = (OrdType?)wrapper.GetField(OrderFields.OrdType);
            double? price = (double?)wrapper.GetField(OrderFields.Price);

            double? stopPx = (double?)wrapper.GetField(OrderFields.StopPx);

          
            Side side = (Side)wrapper.GetField(OrderFields.Side);
            TimeInForce? tif = (TimeInForce?)wrapper.GetField(OrderFields.TimeInForce);
            SettlType? settlType = (SettlType?)wrapper.GetField(OrderFields.SettlType);
            double? ordQty = (double)wrapper.GetField(OrderFields.OrderQty);
            string account = (string)wrapper.GetField(OrderFields.Account);
            string symbol = (string)wrapper.GetField(OrderFields.Symbol);
            SecurityType? secType = (SecurityType?)wrapper.GetField(OrderFields.SecurityType);
            string clOrdId = (string)wrapper.GetField(OrderFields.ClOrdID);
            string origClOrdId = (string)wrapper.GetField(OrderFields.OrigClOrdID);
            string exchange = (string)wrapper.GetField(OrderFields.Exchange); ;


            Order order = new Order();
            order.ClOrdId = clOrdId;
            order.OrigClOrdId = origClOrdId;
            order.OrdType = ordType;
            order.Price = price;
            order.StopPx = stopPx;
            order.Side = side;
            order.TimeInForce = tif;
            order.SettlType = settlType;
            order.OrderQty = ordQty;
            order.Account = account;
            order.Symbol = symbol;
            order.Security = new Security() { Symbol = symbol, SecType = secType };
            order.Exchange = exchange;
            return order;
        }

        #endregion
    }
}

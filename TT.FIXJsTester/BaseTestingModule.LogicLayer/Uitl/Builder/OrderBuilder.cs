using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTestingModule.LogicLayer.Uitl.Builder
{
    public class OrderBuilder
    {
        #region Public Static Methods


        public static Order BuildFullOrder()
        {
            Order order = new Order()
            {
                Security = new Security() { Symbol = "AAPL" },
                ClOrdId = Guid.NewGuid().ToString(),
                Side = Side.Buy,
                OrdType = OrdType.Market,
                Exchange = "testExch",
                TimeInForce = TimeInForce.Day,
                Currency = Currency.USD.ToString(),
                QuantityType = QuantityType.SHARES,
                PriceType = PriceType.FixedAmount,
                OrderQty = 100,
                Account = "testAcc",
                Index = 0
            };
            order.OrigClOrdId = order.ClOrdId;
            return order;
        }

        public static Order BuildOrderMissingSymbol()
        {
            Order order = BuildFullOrder();
            order.Security.Symbol = null;

            return order;
        }


        public static Order BuildOrderMissingAccount()
        {
            Order order = BuildFullOrder();
            order.Account = null;

            return order;
        }

        #endregion
    }
}

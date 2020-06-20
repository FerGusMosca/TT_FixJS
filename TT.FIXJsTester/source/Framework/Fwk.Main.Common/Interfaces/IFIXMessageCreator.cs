using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Interfaces
{
    public interface IFIXMessageCreator
    {
        QuickFix.Message RequestMarketData(int id, string symbol, Fwk.Main.Common.Enums.SubscriptionRequestType pSubscriptionRequestType);

        QuickFix.Message RequestSecurityList(int secType, string security);

        QuickFix.Message CreateNewOrderSingle(string clOrderId, string symbol,
                                             Fwk.Main.Common.Enums.Side? side,
                                             Fwk.Main.Common.Enums.OrdType? ordType,
                                             Fwk.Main.Common.Enums.SettlType? settlType,
                                             Fwk.Main.Common.Enums.TimeInForce? timeInForce,
                                             DateTime? effectiveTime,
                                             double? ordQty, double? price, double? stopPx, string account,
                                             string exchange);

        QuickFix.Message CreateOrderCancelReplaceRequest(string clOrderId, string orderId, string origClOrdId,
                                                             string symbol,
                                                             Fwk.Main.Common.Enums.Side side,
                                                             Fwk.Main.Common.Enums.OrdType ordType,
                                                             Fwk.Main.Common.Enums.SettlType? settlType,
                                                             Fwk.Main.Common.Enums.TimeInForce? timeInForce,
                                                             DateTime effectiveTime,
                                                             double? ordQty, double? price, double? stopPx, string account);

        QuickFix.Message CreateOrderCancelRequest(string clOrderId, string origClOrderId, string orderId, string symbol,
                                                  Fwk.Main.Common.Enums.Side side, DateTime effectiveTime,
                                                  double? ordQty, string account, string mainExchange);


        void ProcessMarketData(QuickFix.Message snapshot, object security, OnLogMessage pOnLogMsg);

        QuickFix.Message CreateOrderMassCancelRequest();


    }
}

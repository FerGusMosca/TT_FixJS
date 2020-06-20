using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Interfaces;
using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FIXCommModule.LogicLayer.Util.Builder
{
    public class FIXMessageCreator : IFIXMessageCreator
    {
        #region IFIXMessageCreator

        public QuickFix.Message RequestMarketData(int id, string symbol, Fwk.Main.Common.Enums.SubscriptionRequestType pSubscriptionRequestType)
        {
            throw new NotImplementedException();
        }

        public QuickFix.Message RequestSecurityList(int secType, string security)
        {
            throw new NotImplementedException();
        }

        public QuickFix.Message CreateNewOrderSingle(string clOrderId, string symbol, Fwk.Main.Common.Enums.Side? side, Fwk.Main.Common.Enums.OrdType? ordType, 
                                                    Fwk.Main.Common.Enums.SettlType? settlType, Fwk.Main.Common.Enums.TimeInForce? timeInForce, DateTime? effectiveTime, 
                                                    double? ordQty, double? price, double? stopPx, string account,string exchange)
        {

            QuickFix.FIX44.NewOrderSingle nos = new QuickFix.FIX44.NewOrderSingle();


            if(!string.IsNullOrEmpty(account))
                nos.SetField(new Account(account));

            if (!string.IsNullOrEmpty(clOrderId))
                nos.SetField(new ClOrdID(clOrderId) );

            if (!string.IsNullOrEmpty(exchange))
                nos.SetField(new SecurityExchange(exchange));

            if (effectiveTime!=null)
                nos.SetField(new TransactTime(effectiveTime.Value));

            if (!string.IsNullOrEmpty(symbol))
                nos.SetField(new Symbol(symbol));

            if (ordQty!=null)
                nos.SetField(new OrderQty(Convert.ToDecimal(ordQty.Value)));

            if (ordType != null)
                nos.SetField(new QuickFix.Fields.OrdType(Convert.ToChar(ordType.Value)));

            if (price != null)
                nos.SetField(new Price(Convert.ToDecimal(price.Value)));

            if (stopPx != null)
                nos.SetField(new StopPx(Convert.ToDecimal(stopPx.Value))) ;

            if (side != null)
                nos.SetField(new QuickFix.Fields.Side(Convert.ToChar(side.Value)));

            if (timeInForce.HasValue)
                nos.SetField(new QuickFix.Fields.TimeInForce(Convert.ToChar(timeInForce.Value)));

            return nos;

            
        }

        public QuickFix.Message CreateOrderCancelReplaceRequest(string clOrderId, string orderId, string origClOrdId, string symbol, Fwk.Main.Common.Enums.Side side, Fwk.Main.Common.Enums.OrdType ordType, Fwk.Main.Common.Enums.SettlType? settlType, Fwk.Main.Common.Enums.TimeInForce? timeInForce, DateTime effectiveTime, double? ordQty, double? price, double? stopPx, string account)
        {
            throw new NotImplementedException();
        }

        public QuickFix.Message CreateOrderCancelRequest(string clOrderId, string origClOrderId, string orderId, string symbol, Fwk.Main.Common.Enums.Side side, DateTime effectiveTime, double? ordQty, string account, string mainExchange)
        {
            throw new NotImplementedException();
        }

        public void ProcessMarketData(QuickFix.Message snapshot, object security, OnLogMessage pOnLogMsg)
        {
            throw new NotImplementedException();
        }

        public QuickFix.Message CreateOrderMassCancelRequest()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

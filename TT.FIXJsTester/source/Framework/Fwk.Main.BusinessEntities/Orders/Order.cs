using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Market_Data;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.BusinessEntities.Orders
{
    public class Order
    {
        #region Public Attributes

        public long Id { get; set; }

        public string OrigClOrdId { get; set; }

        public string ClOrdId { get; set; }

        public string OrderId { get; set; }

        public Security Security { get; set; }

        public SettlType? SettlType { get; set; }

        public DateTime? SettlDate { get; set; }

        public Side Side { get; set; }

        public string Exchange { get; set; }

        public OrdType? OrdType { get; set; }

        public QuantityType QuantityType { get; set; }

        public double? OrderQty { get; set; }

        public double? CashOrderQty { get; set; }

        public double? OrderPercent { get; set; }

        public PriceType PriceType { get; set; }

        public double? Price { get; set; }

        public double? StopPx { get; set; }

        public string Currency { get; set; }

        public TimeInForce? TimeInForce { get; set; }

        public DateTime? ExpireTime { get; set; }//Date and Time

        public DateTime? EffectiveTime { get; set; }

        public double? MinQty { get; set; }

        public int Index { get; set; }

        public string Account { get; set; }

        public string Broker { get; set; }

        public string Strategy { get; set; }

        public OrdStatus OrdStatus { get; set; }

        public string Instruction { get; set; }

        public string Symbol
        {
            get 
            {
                if (Security != null)
                    return Security.Symbol;
                else
                    return null;
            }
            set
            {
                if (Security == null)
                    Security = new Security();

                Security.Symbol = value;
            }
        
        }

        public string RejReason { get; set; }

        public int DecimalPrecission { get; set; }

        #region Strategy Attributes

        public string AggressionLevel { get; set; }

        public bool? CFD { get; set; }

        #endregion

        #endregion

        #region Public Metods

        public Order Clone()
        {
            Order newOrder = new Order();

            newOrder.OrigClOrdId = ClOrdId;
            newOrder.Security = Security;
            newOrder.SettlType = SettlType;
            newOrder.SettlDate = SettlDate;
            newOrder.Side = Side;
            newOrder.Exchange = Exchange;
            newOrder.OrdType = OrdType;
            newOrder.QuantityType = QuantityType;
            newOrder.OrderQty = OrderQty;
            newOrder.CashOrderQty = CashOrderQty;
            newOrder.OrderPercent = OrderPercent;
            newOrder.PriceType = PriceType;
            newOrder.Price = Price;
            newOrder.StopPx = StopPx;
            newOrder.Currency = Currency;
            newOrder.TimeInForce = TimeInForce;
            newOrder.ExpireTime = ExpireTime;
            newOrder.EffectiveTime = EffectiveTime;
            newOrder.MinQty = MinQty;
            newOrder.Index = Index;
            newOrder.Account = Account;
            newOrder.OrdStatus = OrdStatus;

            return newOrder;
        
        }

        public bool IsTradedOrder()
        {
            return OrdStatus == OrdStatus.PartiallyFilled || OrdStatus == OrdStatus.Filled;
        
        }

        public bool IsOpenOrder()
        {
            return OrdStatus == OrdStatus.AcceptedForBidding || OrdStatus == OrdStatus.Calculated
                    || OrdStatus == OrdStatus.PartiallyFilled || OrdStatus == OrdStatus.New
                    || OrdStatus == OrdStatus.PendingCancel || OrdStatus == OrdStatus.PendingNew
                    || OrdStatus == OrdStatus.PendingReplace || OrdStatus == OrdStatus.Replaced
                    || OrdStatus == OrdStatus.Suspended;
        
        }

        #endregion

        #region Public Static Methods

        public static bool FinalStatus(OrdStatus status)
        { 
            return (status==OrdStatus.Filled || status==OrdStatus.Canceled || status==OrdStatus.DoneForDay
                    || status == OrdStatus.Rejected || status==OrdStatus.Stopped || status==OrdStatus.Suspended);
        
        }

        public static bool ActiveStatus(OrdStatus status)
        {
            return (status == OrdStatus.New || status == OrdStatus.PartiallyFilled || status == OrdStatus.PendingNew
                    || status == OrdStatus.PendingCancel || status == OrdStatus.Calculated || status == OrdStatus.AcceptedForBidding
                    || status == OrdStatus.PendingReplace || status == OrdStatus.Replaced);

        }

        #endregion
    }
}

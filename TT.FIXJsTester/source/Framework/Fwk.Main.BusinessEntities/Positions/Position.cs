using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Market_Data;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.BusinessEntities.Positions
{
    public class Position
    {
        #region Constructors

        public Position()
        {
            Security = new Security();
            ExecutionReports = new List<ExecutionReport>();
            Orders = new List<Order>();
            PosStatus = PositionStatus.PendingNew;
        }

        #endregion

        #region Public Attributes

        public int Id { get; set; }

        private string _PosId;
        public string PosId { get { return _PosId; } }

        public Security Security { get; set; }

        public PositionStatus PosStatus { get; set; }

        public Side Side { get; set; }

        public string Exchange { get; set; }

        public QuantityType QuantityType { get; set; }

        public PriceType PriceType { get; set; }

        public double? Qty { get; set; }

        public double? CashQty { get; set; }

        public double? Percent { get; set; }

        public List<ExecutionReport> ExecutionReports { get; set; }

        public List<Order> Orders { get; set; }

        public double CumQty { get; set; }

        public double? LeavesQty { get; set; }

        public double? AvgPx { get; set; }

        public double? LastQty { get; set; }

        public double? LastPx { get; set; }

        public string LastMkt { get; set; }

        public string Symbol
        {
            set
            {

                if (Security == null)
                    Security = new Security();

                Security.Symbol = value;


            }

            get
            {
                if (Security != null)
                    return Security.Symbol;
                else
                    return null;
            }


        }

        public string AccountId { get; set; }

        public string Broker { get; set; }

        public string Strategy { get; set; }

        public string Instruction { get; set; }

        public OrdType OrderType { get; set; }

        public double? OrderPrice { get; set; }

        public PositionRejectReason? PositionRejectReason { get; set; }

        public TimeInForce? TimeInForce { get; set; }

        #region Trading Atts

        public string AggressionLevel { get; set; }

        public bool? CFD { get; set; }

        public double StopLossPct { get; set; }

        #endregion


        #endregion

        #region Public Methods

        public void ValidateNewPosition()
        {
            if (OrderType == OrdType.Limit && OrderPrice == null)
                throw new Exception(string.Format("New position for symbol {0} is a limit order position and doesn't have a price specified", Symbol));
        }

        public bool IsNonMonetaryQuantity()
        {
            return QuantityType == QuantityType.BONDS || QuantityType == QuantityType.CONTRACTS ||
                   QuantityType == QuantityType.SHARES || QuantityType==QuantityType.CRYPTOCURRENCY
                   || QuantityType == QuantityType.OTHER;

        }

        public bool IsMonetaryQuantity()
        {
            return QuantityType == QuantityType.CURRENCY;
        }

        public bool IsSinlgeUnitSecurity()
        {

            return Security.SecType == SecurityType.CS || Security.SecType == SecurityType.FUT || Security.SecType == SecurityType.OPT
                    || Security.SecType == SecurityType.TB || Security.SecType == SecurityType.TBOND || Security.SecType == SecurityType.REPO;
        
        }


        public string GetNextClOrdId(int index)
        {
            return PosId + GetCurrOrdPrefix(true) + index.ToString("00000");
        }

        public void LoadPosId(int id)
        {
            _PosId = id.ToString("000");
        }

        public void LoadPosId(string posId)
        {
            _PosId = posId;
        }

        private string GetCurrOrdPrefix(bool addOne)
        {
            //3 digitos con el número de ordenes en un escenario de armado de una posición de a pasos
            //Me might want to add one when GetCurrOrdPrefix is called for an order which was not created yet
            if (addOne)
                return (Orders.Count + 1).ToString("000");
            else
                return (Orders.Count).ToString("000");
        }


        public ExecutionReport GetFirstExecutionReport()
        {
            if (ExecutionReports != null)
            {
                return ExecutionReports.OrderBy(x => x.TransactTime).FirstOrDefault();
            }
            else
                return null;
        
        
        }

        public ExecutionReport GetLastExecutionReport()
        {
            if (ExecutionReports != null)
            {
                return ExecutionReports.OrderByDescending(x => x.TransactTime).FirstOrDefault();
            }
            else
                return null;


        }

        public Order GetCurrentOrder()
        {
            string orderIdPlusOrdPrefix = PosId + GetCurrOrdPrefix(false);
            if (Orders != null && Orders.Any(x => x.ClOrdId.StartsWith(orderIdPlusOrdPrefix)))
            {


                //There will be only one order with this prefix, which could be updated many times (index)
                return Orders.Where(x => x.ClOrdId.StartsWith(orderIdPlusOrdPrefix)).FirstOrDefault();
            }
            else
            {
                return Orders.OrderByDescending(x => x.OrderId).FirstOrDefault();
                
            }
        }

      

        public void SetPositionStatusFromExecution(ExecType execType)
        {
            if (execType == ExecType.New)
                PosStatus = PositionStatus.New;
            else if (execType == ExecType.DoneForDay)
                PosStatus = PositionStatus.DoneForDay;
            else if (execType == ExecType.Canceled)
                PosStatus = PositionStatus.Canceled;
            else if (execType == ExecType.Replaced)
                PosStatus = PositionStatus.Replaced;
            else if (execType == ExecType.PendingCancel)
                PosStatus = PositionStatus.PendingCancel;
            else if (execType == ExecType.Stopped)
                PosStatus = PositionStatus.Stopped;
            else if (execType == ExecType.Rejected)
                PosStatus = PositionStatus.Rejected;
            else if (execType == ExecType.Suspended)
                PosStatus = PositionStatus.Suspended;
            else if (execType == ExecType.PendingNew)
                PosStatus = PositionStatus.PendingNew;
            else if (execType == ExecType.Calculated)
                PosStatus = PositionStatus.Calculated;
            else if (execType == ExecType.Expired)
                PosStatus = PositionStatus.Expired;
            else if (execType == ExecType.PendingReplace)
                PosStatus = PositionStatus.PendingReplace;
            else if (execType == ExecType.Trade)
                PosStatus = LeavesQty == 0 ? PositionStatus.Filled : PositionStatus.PartiallyFilled;
            else if (execType == ExecType.Unknown)
                PosStatus = PositionStatus.Unknown;
            else
                PosStatus = PositionStatus.Unknown;


        }

        public bool PositionNoLongerActive()
        {
            return PosStatus == PositionStatus.Filled || PosStatus == PositionStatus.Canceled ||
                   PosStatus == PositionStatus.DoneForDay || PosStatus == PositionStatus.Expired ||
                   PosStatus == PositionStatus.Rejected || PosStatus == PositionStatus.Stopped ||
                   PosStatus == PositionStatus.Suspended || PosStatus == PositionStatus.Unknown;
        
        }

        public bool PositionOpen()
        {

            return PosStatus == PositionStatus.New ||
                   PosStatus == PositionStatus.Replaced ||
                   PosStatus == PositionStatus.PendingCancel ||
                   PosStatus == PositionStatus.PendingNew ||
                   PosStatus == PositionStatus.Calculated ||
                   PosStatus == PositionStatus.PendingReplace ||
                   PosStatus == PositionStatus.AcceptedForBidding ||
                   PosStatus == PositionStatus.PartiallyFilled;
        
        }

        public bool PositionTraded()
        {
            return PosStatus == PositionStatus.PartiallyFilled ||
                      PosStatus == PositionStatus.Filled;
        
        }

        public void SetPositionStatusFromExecutionStatus(OrdStatus ordStatus)
        {
            if (ordStatus == OrdStatus.New)
                PosStatus = PositionStatus.New;
            else if (ordStatus == OrdStatus.DoneForDay)
                PosStatus = PositionStatus.DoneForDay;
            else if (ordStatus == OrdStatus.Canceled)
                PosStatus = PositionStatus.Canceled;
            else if (ordStatus == OrdStatus.Replaced)
                PosStatus = PositionStatus.Replaced;
            else if (ordStatus == OrdStatus.PendingCancel)
                PosStatus = PositionStatus.PendingCancel;
            else if (ordStatus == OrdStatus.Stopped)
                PosStatus = PositionStatus.Stopped;
            else if (ordStatus == OrdStatus.Rejected)
                PosStatus = PositionStatus.Rejected;
            else if (ordStatus == OrdStatus.Suspended)
                PosStatus = PositionStatus.Suspended;
            else if (ordStatus == OrdStatus.PendingNew)
                PosStatus = PositionStatus.PendingNew;
            else if (ordStatus == OrdStatus.Calculated)
                PosStatus = PositionStatus.Calculated;
            else if (ordStatus == OrdStatus.Expired)
                PosStatus = PositionStatus.Expired;
            else if (ordStatus == OrdStatus.PendingReplace)
                PosStatus = PositionStatus.PendingReplace;
            else if (ordStatus == OrdStatus.PartiallyFilled)
                PosStatus = PositionStatus.PartiallyFilled;
            else if (ordStatus == OrdStatus.Filled)
                PosStatus = PositionStatus.Filled;
            else 
                PosStatus = PositionStatus.Unknown;


        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.BusinessEntities.Positions
{
    public class ExecutionSummary
    {

        #region Constructors

        public ExecutionSummary(Position pos)
        {
            Position = pos;
            Date = DateTime.Now;
            Symbol = pos.Security.Symbol;
            AvgPx = pos.AvgPx;
            CumQty = pos.CumQty;
            LeavesQty = pos.LeavesQty;
            Text = pos.GetCurrentOrder() != null ? pos.GetCurrentOrder().RejReason : "";
            AccountNumber = pos.AccountId;
            LastUpdateTime=DateTime.Now;
        }


        #endregion

        #region Public Attributes

        public long Id { get; set; }

        public DateTime Date { get; set; }

        public Position Position { get; set; }

        public string Symbol { get; set; }

        public double? AvgPx { get; set; }

        public double CumQty { get; set; }

        public double? LeavesQty { get; set; }

        public double? Commission { get; set; }

        public string Text { get; set; }

        public string Console { get; set; }

        public string AccountNumber{ get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime? LastTradeTime { get; set; }

        #endregion

        #region Public Methods

        public double GetCashExecution()
        {
            double acum = 0;

            if (Position.PositionTraded())
            {
                if (AvgPx.HasValue)
                {
                    acum = CumQty * AvgPx.Value;

                    if (Commission != null)
                        acum += Commission.Value;
                    
                    return acum;
                }
                else
                    throw new Exception(string.Format("Could not process avg px for a closed position for symbol {0}", Position.Symbol));
            }
            else
                return 0;
        }

        public bool IsFilledPosition()
        {
            return Position.PositionTraded();
        }

        public bool IsCancelledPosition()
        {
            return (Position != null && Position.PosStatus == PositionStatus.Canceled);
        }

        public bool IsRejectedPosition()
        {
            return (Position != null && Position.PosStatus == PositionStatus.Rejected);
        }


        public void UpdateStatus(ExecutionReport execReport)
        {
            CumQty = execReport.CumQty;
            LeavesQty = execReport.LeavesQty;
            AvgPx = execReport.AvgPx;
            Commission = execReport.Commission;
            Text = execReport.Text;
            Position.LeavesQty = execReport.LeavesQty;
            Position.SetPositionStatusFromExecution(execReport.ExecType);
            Position.ExecutionReports.Add(execReport);
            LastUpdateTime = LastUpdateTime;

            if (Position.PositionTraded())
                LastTradeTime = execReport.TransactTime;

            if (execReport.Order != null)
                Position.Orders.Add(execReport.Order);
        
        }

        #endregion
    }
}

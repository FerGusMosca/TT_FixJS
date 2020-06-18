using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Positions;
using Fwk.Main.DataAccess;
using Fwk.StrategyHandler.DataAccessLayer;
using Fwk.Main.DataAccessLayer.Helpers;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Enums;

namespace Fwk.Main.DataAccessLayer.Managers
{
    public class ExecutionSummaryManager : MappingEnabledAbstract
    {
        #region Constructors
        public ExecutionSummaryManager(string connectionString): base(connectionString)
        {

        }
        public ExecutionSummaryManager(StrategyReportsEntities context) : base(context) { }
        #endregion

        #region Protected Methods

        protected void Map(ExecutionSummary summary, execution_summaries summaryDB)
        { 
            summaryDB.date = summary.Date;
            summaryDB.symbol = summary.Symbol;
            summaryDB.avg_px = summary.AvgPx;
            summaryDB.cum_qty = summary.CumQty;
            summaryDB.leaves_qty = summary.LeavesQty;
            summaryDB.commission = summary.Commission;
            summaryDB.text = summary.Text;
            summaryDB.console = summary.Console;
            summaryDB.account_number = summary.AccountNumber;
        }

        protected void Map(Position position, positions positionsDB)
        { 
            positionsDB.pos_id = position.PosId;
            positionsDB.symbol = position.Symbol;
            positionsDB.side = Convert.ToChar(position.Side).ToString();
            positionsDB.pos_status = Convert.ToChar(position.PosStatus).ToString();
            positionsDB.exchange = position.Exchange;
            positionsDB.quantity_type = Convert.ToInt32(position.QuantityType);
            positionsDB.price_type = Convert.ToInt32(position.PriceType);
            positionsDB.qty = position.Qty;
            positionsDB.cash_qty = position.CashQty;
            positionsDB.percent = position.Percent;
            positionsDB.cum_qty = position.CumQty;
            positionsDB.leaves_qty = position.LeavesQty;
            positionsDB.avg_px = position.AvgPx;
            positionsDB.last_qty = position.LastQty;
            positionsDB.last_mkt = position.LastMkt;
        }

        protected void Map(Order order, orders orderBD)
        {
            //orderBD.id = order.Id;
            orderBD.order_id = order.OrderId;
            orderBD.symbol = order.Symbol;
            orderBD.settl_type = Convert.ToChar(order.SettlType).ToString();
            orderBD.settl_date = order.SettlDate;
            orderBD.side = Convert.ToChar(order.Side).ToString();
            orderBD.exchange = order.Exchange;
            orderBD.ord_type =  Convert.ToChar(order.OrdType).ToString();
            orderBD.quantity_type = Convert.ToInt32(order.QuantityType);
            orderBD.order_qty = order.OrderQty;
            orderBD.cash_order_qty = order.CashOrderQty;
            orderBD.order_percent = order.OrderPercent;
            orderBD.price_type = Convert.ToInt32(order.PriceType);
            orderBD.price = order.Price;
            orderBD.stop_px = order.StopPx;
            orderBD.currency = order.Currency;
            orderBD.time_inforce = order.TimeInForce != null ? (int?)Convert.ToInt32(order.TimeInForce.Value) : null;
            orderBD.expire_time = order.ExpireTime;
            orderBD.effective_time = order.EffectiveTime;
            orderBD.min_qty = order.MinQty;
            orderBD.index = order.Index;
        }

        protected void Map(ExecutionReport report, execution_reports reportsDB)
        {
            reportsDB.exec_id = report.ExecID;
            reportsDB.transact_time = report.TransactTime;
            reportsDB.exec_type = Convert.ToChar(report.ExecType).ToString();
            reportsDB.ord_status = Convert.ToChar(report.OrdStatus).ToString();
            reportsDB.ord_rej_reason = report.OrdRejReason != null ? (int?)Convert.ToInt32(report.OrdRejReason) : null;
            reportsDB.last_qty = report.LastQty;
            reportsDB.last_px = report.LastPx;
            reportsDB.last_mkt = report.LastMkt;
            reportsDB.leaves_qty = report.LeavesQty;
            reportsDB.cum_qty = report.CumQty;
            reportsDB.avg_px = report.AvgPx;
            reportsDB.commission = report.Commission;
            reportsDB.text = report.Text;
        }


        #endregion

        #region Public Methods

        protected execution_summaries GetById(long id)
        {
            var executionSummaryDB = ctx.execution_summaries.ToList().Where(x => x.id==id) .FirstOrDefault();
            return executionSummaryDB;
        }

        protected execution_summaries GetBySymbolAndDate(string symbol, DateTime date)
        {
            var executionSummaryDB = ctx.execution_summaries.ToList().Where(    x => x.symbol == symbol 
                                                                             && DateTime.Compare(x.date.Date,date.Date)==0)
                                                                      .OrderByDescending(x=>x.date)
                                                                      .FirstOrDefault();

            return executionSummaryDB;
        }

        public void Insert(ExecutionSummary summary)
        {
            execution_summaries execSummaryDB = GetById(summary.Id);

            if (execSummaryDB == null)
            {
                execution_summaries summaries = new execution_summaries();
                Map(summary, summaries);

                summaries.positions = new positions();
                Map(summary.Position, summaries.positions);

                foreach (Order order in summary.Position.Orders)
                {
                    orders orderBD = new orders();
                    Map(order, orderBD);
                    summaries.positions.orders.Add(orderBD);
                }

                foreach (ExecutionReport report in summary.Position.ExecutionReports)
                {
                    execution_reports reportsDB = new execution_reports();
                    Map(report, reportsDB);
                    summaries.positions.execution_reports.Add(reportsDB);
                }
                ctx.execution_summaries.AddObject(summaries);
                ctx.SaveChanges();

                summary.Id = summaries.id;

                int i = 0;
                foreach (orders orderDB in summaries.positions.orders)
                {
                    summary.Position.Orders[i].Id = orderDB.id;
                    i++;
                }

            }
            else
            {
                Map(summary, execSummaryDB);

                Map(summary.Position, execSummaryDB.positions);

                foreach (Order order in summary.Position.Orders)
                {
                    if (!execSummaryDB.positions.orders.Any(x => x.id == order.Id))
                    {
                        orders orderBD = new orders();
                        Map(order, orderBD);
                        execSummaryDB.positions.orders.Add(orderBD);
                    }

                }

                foreach (ExecutionReport report in summary.Position.ExecutionReports)
                {
                    if (report.TransactTime.HasValue && !execSummaryDB.positions.execution_reports.Any(x => x.transact_time.HasValue && DateTime.Compare(x.transact_time.Value, report.TransactTime.Value) == 0))
                    {
                        execution_reports reportsDB = new execution_reports();
                        Map(report, reportsDB);
                        execSummaryDB.positions.execution_reports.Add(reportsDB);
                    }
                }
                ctx.SaveChanges();
            }
            
        }

        #endregion
    }
}

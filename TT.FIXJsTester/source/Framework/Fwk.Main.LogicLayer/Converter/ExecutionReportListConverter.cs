using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Positions;
using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Converter;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.LogicLayer.Converter
{
    public class ExecutionReportListConverter : ConverterBase
    {

        #region Private Methods

        protected void ValidateExecutionReportList(Wrapper wrapper)
        {
            if (!ValidateField(wrapper, ExecutionReportListFields.ExecutionReports))
                throw new Exception("Missing parameter ExecutionReports for execution report list");
        }


        #endregion

        #region Public Methods

        public List<Position> GetPositionsFromExecutionReportList(Wrapper wrapper,ExecutionReportConverter execReportConv,
                                                                  ILogger logger, IConfiguration config)
        {

            bool success = (bool) wrapper.GetField(ExecutionReportListFields.Status);

            if (success)
            {

                ValidateExecutionReportList(wrapper);

                Wrapper[] execReportWrappers = (Wrapper[])wrapper.GetField(ExecutionReportListFields.ExecutionReports);

                List<Position> positions = new List<Position>();
                foreach (Wrapper execReportWrapper in execReportWrappers)
                {
                    ExecutionReport execReport = null;
                    try
                    {
                        execReport = execReportConv.GetExecutionReport(execReportWrapper, config);

                        Position pos = new Position()
                        {
                            Security = new Security()
                            {
                                Symbol = execReport.Order.Security.Symbol,
                                Currency = execReport.Order.Security.Currency,
                                Exchange = execReport.Order.Exchange
                            },
                            Side = execReport.Order.Side,
                            Exchange = execReport.Order.Exchange,
                            PriceType = PriceType.FixedAmount,
                            Qty = execReport.Order.OrderQty,
                            CashQty = null,
                            Percent = null,
                            QuantityType = QuantityType.SHARES,
                            LeavesQty = execReport.LeavesQty,
                            CumQty = execReport.CumQty,
                            AvgPx = execReport.AvgPx,
                            LastQty = execReport.LastQty,
                            LastPx = execReport.LastPx,
                            LastMkt = execReport.LastMkt,
                            AccountId = execReport.Order.Account,
                            Broker = execReport.Order.Broker,
                            Strategy = execReport.Order.Strategy,
                            OrderType = execReport.Order.OrdType,
                            OrderPrice = execReport.Order.Price,
                            PositionRejectReason = null,
                            CFD = execReport.CFD
                        };

                        pos.LoadPosId(Guid.NewGuid().ToString());
                        pos.SetPositionStatusFromExecution(execReport.ExecType);
                        pos.ExecutionReports.Add(execReport);
                        pos.Orders.Add(execReport.Order);

                        positions.Add(pos);
                    }
                    catch (Exception ex)
                    {
                        logger.DoLog(string.Format("Error loading previously exsiting execution report for order {0}:{1}",
                                    (execReport != null && execReport.Order != null) ? execReport.Order.OrderId : "unknown order", ex.Message),
                                    Common.Util.Constants.MessageType.Error);
                    }
                }

                return positions;
            }
            else
            {
                string error = (string) wrapper.GetField(ExecutionReportListFields.Error);
                throw new Exception(error);
            }
        }

        #endregion
    }
}

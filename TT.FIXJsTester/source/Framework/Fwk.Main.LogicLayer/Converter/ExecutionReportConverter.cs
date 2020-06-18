using Fwk.Main.BusinessEntities.Orders;
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
    public class ExecutionReportConverter : ConverterBase
    {
        #region Private Methods
        private void RunMainValidations(Wrapper wrapper)
        {
            if (wrapper.GetAction() != Actions.EXECUTION_REPORT)
                throw new Exception("Invalid action building execution report");

        }

        protected void ValidateExecutionReport(Wrapper wrapper)
        {
            if (!ValidateField(wrapper, ExecutionReportFields.ExecType))
                throw new Exception("Missing execution type");

            if (!ValidateField(wrapper, ExecutionReportFields.OrdStatus))
                throw new Exception("Missing order status");

            if (!ValidateField(wrapper, ExecutionReportFields.LeavesQty))
                throw new Exception("Missing leaves qty");

            if (!ValidateField(wrapper, ExecutionReportFields.CumQty))
                throw new Exception("Missing cum qty");

            //if (!ValidateField(wrapper, ExecutionReportFields.AvgPx))
            //    throw new Exception("Missing average price");

            //if (!ValidateField(wrapper, ExecutionReportFields.LastPx))
            //    throw new Exception("Missing last price");

            //if (!ValidateField(wrapper, ExecutionReportFields.OrderID))
            //    throw new Exception("Missing Order Id");


        }

        private Order BuildOrder(Wrapper wrapper)
        {
            Order order = new Order();

            order.OrderId = (ValidateField(wrapper, ExecutionReportFields.OrderID) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.OrderID)) : null);
            order.ClOrdId = (ValidateField(wrapper, ExecutionReportFields.ClOrdID) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.ClOrdID)) : null);
            order.OrderQty = (ValidateField(wrapper, ExecutionReportFields.OrderQty) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.OrderQty)) : null);
            order.CashOrderQty = (ValidateField(wrapper, ExecutionReportFields.CashOrderQty) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.CashOrderQty)) : null);
            order.OrdType = (OrdType)(ValidateField(wrapper, ExecutionReportFields.OrdType) ? wrapper.GetField(ExecutionReportFields.OrdType) : OrdType.Limit);
            order.Price = (ValidateField(wrapper, ExecutionReportFields.Price) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.Price)) : null);
            order.StopPx = (ValidateField(wrapper, ExecutionReportFields.StopPx) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.StopPx)) : null);
            order.Currency = (ValidateField(wrapper, ExecutionReportFields.Currency) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.Currency)) : null);
            order.ExpireTime = (ValidateField(wrapper, ExecutionReportFields.ExpireDate) ? (DateTime?)Convert.ToDateTime(wrapper.GetField(ExecutionReportFields.ExpireDate)) : null);
            order.MinQty = (ValidateField(wrapper, ExecutionReportFields.MinQty) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.MinQty)) : null);
            order.Side = (Side)(ValidateField(wrapper, ExecutionReportFields.Side) ? wrapper.GetField(ExecutionReportFields.Side) : Side.Unknown);
            order.QuantityType = (QuantityType)(ValidateField(wrapper, ExecutionReportFields.QuantityType) ? wrapper.GetField(ExecutionReportFields.QuantityType) : QuantityType.OTHER);
            order.PriceType = (PriceType)(ValidateField(wrapper, ExecutionReportFields.PriceType) ? wrapper.GetField(ExecutionReportFields.PriceType) : PriceType.FixedAmount);
            order.TimeInForce = (TimeInForce?)(ValidateField(wrapper, ExecutionReportFields.TimeInForce) ? wrapper.GetField(ExecutionReportFields.TimeInForce) : null);
            order.Account = Convert.ToString(wrapper.GetField(ExecutionReportFields.Account));

            order.Security = new Security();
            order.Security.Symbol = (ValidateField(wrapper, ExecutionReportFields.Symbol) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.Symbol)) : null);

            return order;
        }

        #endregion

        #region Public Methods

        public ExecutionReport GetExecutionReport(Wrapper wrapper, IConfiguration Config)
        {
            ExecutionReport er = new ExecutionReport();

            ValidateExecutionReport(wrapper);

            er.ExecType = (ExecType)(ValidateField(wrapper, ExecutionReportFields.ExecType) ? wrapper.GetField(ExecutionReportFields.ExecType) : null);
            er.OrdStatus = (OrdStatus)(ValidateField(wrapper, ExecutionReportFields.OrdStatus) ? wrapper.GetField(ExecutionReportFields.OrdStatus) : null);
            er.OrdRejReason = (OrdRejReason?)(ValidateField(wrapper, ExecutionReportFields.OrdRejReason) ? wrapper.GetField(ExecutionReportFields.OrdRejReason) : null);
            er.LeavesQty = (ValidateField(wrapper, ExecutionReportFields.LeavesQty) ? Convert.ToDouble(wrapper.GetField(ExecutionReportFields.LeavesQty)) : 0);
            er.CumQty = (ValidateField(wrapper, ExecutionReportFields.CumQty) ? Convert.ToDouble(wrapper.GetField(ExecutionReportFields.CumQty)) : 0);
            er.AvgPx = (ValidateField(wrapper, ExecutionReportFields.AvgPx) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.AvgPx)) : null);
            er.Commission = (ValidateField(wrapper, ExecutionReportFields.Commission) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.Commission)) : null);
            er.Text = (ValidateField(wrapper, ExecutionReportFields.Text) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.Text)) : null);
            er.TransactTime = (ValidateField(wrapper, ExecutionReportFields.TransactTime) ? (DateTime?)Convert.ToDateTime(wrapper.GetField(ExecutionReportFields.TransactTime)) : null);
            er.LastQty = (ValidateField(wrapper, ExecutionReportFields.LastQty) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.LastQty)) : null);
            er.LastPx = (ValidateField(wrapper, ExecutionReportFields.LastPx) ? (double?)Convert.ToDouble(wrapper.GetField(ExecutionReportFields.LastPx)) : null);
            er.LastMkt = (ValidateField(wrapper, ExecutionReportFields.LastMkt) ? Convert.ToString(wrapper.GetField(ExecutionReportFields.LastMkt)) : null);
            er.LastFillTime = (ValidateField(wrapper, ExecutionReportFields.LastFillTime) ? (DateTime?)Convert.ToDateTime(wrapper.GetField(ExecutionReportFields.LastFillTime)) : null);
            er.CFD = (ValidateField(wrapper, ExecutionReportFields.CFD) ? (bool?)Convert.ToBoolean(wrapper.GetField(ExecutionReportFields.CFD)) : false);


            er.Order = BuildOrder(wrapper);

            return er;

        }


        #endregion
    }
}

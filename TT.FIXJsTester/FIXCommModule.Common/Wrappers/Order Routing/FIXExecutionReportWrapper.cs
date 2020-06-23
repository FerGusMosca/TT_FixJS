using FIXCommModule.Common.Util;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Wrappers;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FIXCommModule.Common.Wrappers.Order_Routing
{
    public class FIXExecutionReportWrapper : Wrapper
    {
        #region Private Attributes

        protected string Key { get; set; }

        protected Message ExecutionReport { get; set; }

        protected IConfiguration Config { get; set; }

        #endregion

        #region Constructors

        public FIXExecutionReportWrapper(string pKey, Message pExecReport, IConfiguration pConfig)
        {
            Key = pKey;

            ExecutionReport = pExecReport;

            Config = pConfig;
        }

        #endregion

        #region Private Methods

        protected string GetOrdId(Message msg, int field)
        {
            string OrdId = null;
            if (field == QuickFix.Fields.Tags.ClOrdID)
                OrdId = FixHelper.GetNullFieldIfSet(msg,QuickFix.Fields.Tags.ClOrdID);
            else if (field == QuickFix.Fields.Tags.SecondaryClOrdID)
                OrdId = FixHelper.GetNullFieldIfSet(msg, QuickFix.Fields.Tags.SecondaryClOrdID);
            else if (field == QuickFix.Fields.Tags.OrderID)
                OrdId = FixHelper.GetNullFieldIfSet(msg, QuickFix.Fields.Tags.OrderID);
            else if (field == QuickFix.Fields.Tags.OrigClOrdID)
                OrdId = FixHelper.GetNullFieldIfSet(msg, QuickFix.Fields.Tags.OrigClOrdID);

            return OrdId;
        }

        protected ExecType GetExecType(char state)
        {

            if (state == QuickFix.Fields.ExecType.PENDING_NEW)
                return ExecType.PendingNew;
            else if (state == QuickFix.Fields.ExecType.NEW)
                return ExecType.New;
            else if (state == QuickFix.Fields.ExecType.REJECTED)
                return ExecType.Rejected;
            else if (state == QuickFix.Fields.ExecType.CANCELED)
                return ExecType.Canceled;
            else if (state == QuickFix.Fields.ExecType.EXPIRED)
                return ExecType.Expired;
            else if (state == QuickFix.Fields.ExecType.PENDING_REPLACE)
                return ExecType.PendingReplace;
            else if (state == QuickFix.Fields.ExecType.REPLACED)
                return ExecType.Replaced;
            else if (state == QuickFix.Fields.ExecType.TRADE)
                return ExecType.Trade;
            else if (state == QuickFix.Fields.ExecType.PENDING_CANCEL)
                return ExecType.PendingCancel;
            else
                throw new Exception("Tipo de ejecución no soportado: " + state.ToString());
        }

        protected OrdStatus GetOrdStatus(char estado)
        {
            if (estado == QuickFix.Fields.OrdStatus.PENDING_NEW)
                return OrdStatus.PendingNew;
            else if (estado == QuickFix.Fields.OrdStatus.NEW)
                return OrdStatus.New;
            else if (estado == QuickFix.Fields.OrdStatus.REJECTED)
                return OrdStatus.Rejected;
            else if (estado == QuickFix.Fields.OrdStatus.CANCELED)
                return OrdStatus.Canceled;
            else if (estado == QuickFix.Fields.OrdStatus.EXPIRED)
                return OrdStatus.Expired;
            else if (estado == QuickFix.Fields.OrdStatus.PENDING_REPLACE)
                return OrdStatus.PendingReplace;
            else if (estado == QuickFix.Fields.OrdStatus.REPLACED)
                return OrdStatus.Replaced;
            else if (estado == QuickFix.Fields.OrdStatus.FILLED)
                return OrdStatus.Filled;
            else if (estado == QuickFix.Fields.OrdStatus.PARTIALLY_FILLED)
                return OrdStatus.PartiallyFilled;
            else
                throw new Exception("OrdStatus not supported: " + estado.ToString());
        }

        protected OrdType? GetOrdType(char estado)
        {
            if (estado == QuickFix.Fields.OrdType.LIMIT)
                return OrdType.Limit;
            else if (estado == QuickFix.Fields.OrdType.MARKET)
                return OrdType.Market;
            else if (estado == QuickFix.Fields.OrdType.STOP)
                return OrdType.Stop;
            else if (estado == QuickFix.Fields.OrdType.STOP_LIMIT)
                return OrdType.StopLimit;
            else if (estado == QuickFix.Fields.OrdType.MARKET_ON_CLOSE)
                return OrdType.MarketOnClose;
            else if (estado == QuickFix.Fields.OrdType.LIMIT_ON_CLOSE)
                return OrdType.LimitOnClose;
            else
                return null;

        }

        protected Side GetSide(char estado)
        {
            if (estado == QuickFix.Fields.Side.BUY)
                return Side.Buy;
            else if (estado == QuickFix.Fields.Side.SELL)
                return Side.Sell;

            else
                return Side.Unknown;
        }

        #endregion


        #region Public Overriden Methods

        public override Actions GetAction()
        {
            return Actions.EXECUTION_REPORT;
        }

        public override object GetField(Fields field)
        {

            if (ExecutionReport == null)
                return ExecutionReportFields.NULL;

            if (field == Fields.KEY)
                return Key;
            else
            {
                ExecutionReportFields xrField = (ExecutionReportFields)field;

                if (xrField == ExecutionReportFields.OrderID)
                    return GetOrdId(ExecutionReport, QuickFix.Fields.Tags.OrderID);
                else if (xrField == ExecutionReportFields.ClOrdID)
                    return GetOrdId(ExecutionReport, QuickFix.Fields.Tags.ClOrdID);
                else if (xrField == ExecutionReportFields.OrigClOrdID)
                    return GetOrdId(ExecutionReport, QuickFix.Fields.Tags.OrigClOrdID);
                else if (xrField == ExecutionReportFields.ExecType)
                    return GetExecType(ExecutionReport.GetChar(QuickFix.Fields.Tags.ExecType));
                else if (xrField == ExecutionReportFields.ExecID)
                    return ExecutionReport.GetString(QuickFix.Fields.Tags.ExecID);
                else if (xrField == ExecutionReportFields.OrdStatus)
                    return GetOrdStatus(ExecutionReport.GetChar(QuickFix.Fields.Tags.OrdStatus));
                else if (xrField == ExecutionReportFields.OrdRejReason)
                    return FixHelper.GetNullIntFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.OrdRejReason);
                else if (xrField == ExecutionReportFields.LeavesQty)
                    return ExecutionReport.GetInt(QuickFix.Fields.Tags.LeavesQty);
                else if (xrField == ExecutionReportFields.CumQty)
                    return ExecutionReport.GetInt(QuickFix.Fields.Tags.CumQty);
                else if (xrField == ExecutionReportFields.AvgPx)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.AvgPx);
                else if (xrField == ExecutionReportFields.Commission)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.Commission);
                else if (xrField == ExecutionReportFields.Text)
                    return ExecutionReport.GetField(QuickFix.Fields.Tags.Text);
                else if (xrField == ExecutionReportFields.TransactTime)
                    return DateTime.Now;
                else if (xrField == ExecutionReportFields.LastQty)
                    return FixHelper.GetNullIntFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.LastQty);
                else if (xrField == ExecutionReportFields.LastPx)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.LastPx);
                else if (xrField == ExecutionReportFields.LastMkt)
                    return FixHelper.GetNullFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.LastMkt);


                else if (xrField == ExecutionReportFields.Symbol)
                {
                    return FixHelper.GetNullFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.Symbol);
                }
                else if (xrField == ExecutionReportFields.OrderQty)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.OrderQty);
                else if (xrField == ExecutionReportFields.CashOrderQty)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.CashOrderQty);
                else if (xrField == ExecutionReportFields.OrdType)
                    return GetOrdType(ExecutionReport.GetChar(QuickFix.Fields.Tags.OrdType));
                else if (xrField == ExecutionReportFields.Price)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.Price);
                else if (xrField == ExecutionReportFields.StopPx)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.StopPx);
                else if (xrField == ExecutionReportFields.Currency)
                    return FixHelper.GetNullFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.Currency);
                else if (xrField == ExecutionReportFields.ExpireDate)
                    return FixHelper.GetDateTimeFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.ExpireDate);
                else if (xrField == ExecutionReportFields.MinQty)
                    return FixHelper.GetNullDoubleFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.MinQty);
                else if (xrField == ExecutionReportFields.Side)
                    GetSide(ExecutionReport.GetChar(QuickFix.Fields.Tags.Side));
                else if (xrField == ExecutionReportFields.QuantityType)
                    return QuantityType.SHARES;
                else if (xrField == ExecutionReportFields.PriceType)
                    return PriceType.FixedAmount;
                else if (xrField == ExecutionReportFields.Account)
                    return ExecutionReport.GetField(QuickFix.Fields.Tags.Account);
                else if (xrField == ExecutionReportFields.ExecInst)
                    return ExecutionReport.GetField(QuickFix.Fields.Tags.ExecInst);
            }

            return ExecutionReportFields.NULL;
        }

        public override string ToString()
        {
            OrdStatus ordStatus = GetOrdStatus(ExecutionReport.GetChar(QuickFix.Fields.Tags.OrdStatus));
            ExecType? execType = GetExecType(ExecutionReport.GetChar( QuickFix.Fields.Tags.ExecType));
            string symbol = FixHelper.GetNullFieldIfSet(ExecutionReport, QuickFix.Fields.Tags.Symbol);
            string exchange = "";
            

            return string.Format("Execution Report for symbol {2}: Order Status={0} - Exec Type={1}",
                                               ordStatus.ToString(), execType.ToString(), symbol);
        }

        #endregion
    }
}

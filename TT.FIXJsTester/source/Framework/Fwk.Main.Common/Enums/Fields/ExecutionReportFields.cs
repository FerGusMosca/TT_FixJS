using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class ExecutionReportFields : Fields
    {
        public static readonly ExecutionReportFields OrderID = new ExecutionReportFields(2);
        public static readonly ExecutionReportFields ClOrdID = new ExecutionReportFields(3);
        public static readonly ExecutionReportFields ExecType = new ExecutionReportFields(4);
        public static readonly ExecutionReportFields OrdStatus = new ExecutionReportFields(5);
        public static readonly ExecutionReportFields OrdRejReason = new ExecutionReportFields(6);
        public static readonly ExecutionReportFields Symbol = new ExecutionReportFields(7);

        public static readonly ExecutionReportFields OrderQty = new ExecutionReportFields(8);
        public static readonly ExecutionReportFields CashOrderQty = new ExecutionReportFields(9);
        public static readonly ExecutionReportFields OrdType = new ExecutionReportFields(10);
        public static readonly ExecutionReportFields Price = new ExecutionReportFields(11);
        public static readonly ExecutionReportFields StopPx = new ExecutionReportFields(12);
        public static readonly ExecutionReportFields Currency = new ExecutionReportFields(13);
        public static readonly ExecutionReportFields ExpireDate = new ExecutionReportFields(14);
        public static readonly ExecutionReportFields LeavesQty = new ExecutionReportFields(15);
        public static readonly ExecutionReportFields CumQty = new ExecutionReportFields(16);
        public static readonly ExecutionReportFields AvgPx = new ExecutionReportFields(17);
        public static readonly ExecutionReportFields Commission = new ExecutionReportFields(18);
        public static readonly ExecutionReportFields MinQty = new ExecutionReportFields(19);
        public static readonly ExecutionReportFields Text = new ExecutionReportFields(20);
        public static readonly ExecutionReportFields TransactTime = new ExecutionReportFields(21);
        public static readonly ExecutionReportFields LastQty = new ExecutionReportFields(22);
        public static readonly ExecutionReportFields LastPx = new ExecutionReportFields(23);
        public static readonly ExecutionReportFields LastMkt = new ExecutionReportFields(24);
        public static readonly ExecutionReportFields ExecID = new ExecutionReportFields(25);
        public static readonly ExecutionReportFields Side = new ExecutionReportFields(26);
        public static readonly ExecutionReportFields QuantityType = new ExecutionReportFields(27);
        public static readonly ExecutionReportFields PriceType = new ExecutionReportFields(28);
        public static readonly ExecutionReportFields OrigClOrdID = new ExecutionReportFields(29);
        public static readonly ExecutionReportFields Account = new ExecutionReportFields(30);
        public static readonly ExecutionReportFields ExecInst = new ExecutionReportFields(31);
        public static readonly ExecutionReportFields TimeInForce = new ExecutionReportFields(32);
        public static readonly ExecutionReportFields PosId = new ExecutionReportFields(33);
        public static readonly ExecutionReportFields Instruction = new ExecutionReportFields(34);
        public static readonly ExecutionReportFields LastFillTime = new ExecutionReportFields(35);
        public static readonly ExecutionReportFields CFD = new ExecutionReportFields(36);

        protected ExecutionReportFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

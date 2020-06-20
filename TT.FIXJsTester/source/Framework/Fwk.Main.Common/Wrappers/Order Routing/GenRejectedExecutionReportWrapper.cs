using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class GenRejectedExecutionReportWrapper:Wrapper
    {

        #region Constructors

        public GenRejectedExecutionReportWrapper(string error)
        {
            Error = error;
        }


        #endregion

        #region Protected Attributes

        public string Error { get; set; }

        #endregion

        #region Wrapper

        public override object GetField(Enums.Fields.Fields field)
        {

            ExecutionReportFields xrField = (ExecutionReportFields)field;

            if (xrField == ExecutionReportFields.ExecType)
                return ExecType.Rejected;

            else if (xrField == ExecutionReportFields.OrdStatus)
                return OrdStatus.Rejected;
            else if (xrField == ExecutionReportFields.OrdRejReason)
                return ExecutionReportFields.NULL;
            else if (xrField == ExecutionReportFields.LeavesQty)
                return 0;
            else if (xrField == ExecutionReportFields.CumQty)
                return 0;
            else if (xrField == ExecutionReportFields.Text)
                return Error;
            else if (xrField == ExecutionReportFields.TransactTime)
                return DateTime.Now;
            else
                return ExecutionReportFields.NULL;
        }

        public override Enums.Actions GetAction()
        {
            return Actions.EXECUTION_REPORT;
        }

        #endregion
    }
}

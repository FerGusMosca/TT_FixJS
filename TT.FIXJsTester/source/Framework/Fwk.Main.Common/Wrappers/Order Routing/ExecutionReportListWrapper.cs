using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class ExecutionReportListWrapper:Wrapper
    {
        #region Constructors

        public ExecutionReportListWrapper(Wrapper[] executionReports)
        {
            Success = true;
            ExecutionReports = executionReports;
        }

        public ExecutionReportListWrapper(Exception ex)
        {
            Success = false;
            Exception = ex;
        }

        #endregion

        #region Protected Attributes

        protected bool Success { get; set; }

        protected Exception Exception { get; set; }

        protected Wrapper[] ExecutionReports { get; set; }

        #endregion

        #region Wrapper Methods

        public override object GetField(Fields field)
        {
            ExecutionReportListFields xrField = (ExecutionReportListFields)field;

            if (xrField == ExecutionReportListFields.ExecutionReports)
                return ExecutionReports;
            else if (xrField == ExecutionReportListFields.Status)
                return Success;
            else if (xrField == ExecutionReportListFields.Error)
                return Exception != null ? Exception.Message : null;

            else
                return ExecutionReportListFields.NULL;
        }

        public override Enums.Actions GetAction()
        {
            return Actions.EXECUTION_REPORT_LIST;
        }

        #endregion
    }
}

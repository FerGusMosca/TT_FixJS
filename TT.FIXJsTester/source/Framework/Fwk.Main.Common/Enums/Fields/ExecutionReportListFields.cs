using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class ExecutionReportListFields : Fields
    {
        public static readonly ExecutionReportListFields ExecutionReports = new ExecutionReportListFields(2);

        public static readonly ExecutionReportListFields Status = new ExecutionReportListFields(3);

        public static readonly ExecutionReportListFields Error = new ExecutionReportListFields(4);

        protected ExecutionReportListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

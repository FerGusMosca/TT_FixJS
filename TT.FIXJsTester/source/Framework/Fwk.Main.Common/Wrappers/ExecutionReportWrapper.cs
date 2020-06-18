using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class ExecutionReportWrapper:Wrapper
    {
        #region Protected Attributes

        public string PosId { get; set; }

        public Wrapper ExecReportWrapper { get; set; }

        #endregion


        #region Constructors

        public ExecutionReportWrapper(string posId, Wrapper execReportWrapper)
        {
            PosId = posId;

            ExecReportWrapper = execReportWrapper;
        
        }

        #endregion

        #region Protected Attributes

        public override object GetField(Fields field)
        {
            ExecutionReportFields xrField = (ExecutionReportFields)field;


            if (xrField == ExecutionReportFields.PosId)
                return PosId;
            else
                return ExecReportWrapper.GetField(field);
        }

        public override Enums.Actions GetAction()
        {
            return Actions.EXECUTION_REPORT;
        }

        #endregion
    }
}

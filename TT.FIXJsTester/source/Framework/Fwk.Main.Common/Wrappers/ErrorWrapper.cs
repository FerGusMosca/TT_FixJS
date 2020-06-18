using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class ErrorWrapper : Wrapper
    {
        #region Constructors

        public ErrorWrapper(Exception ex)
        {
            Exception = ex;

        }

        #endregion

        #region Protected Attributes

        protected Exception Exception { get; set; }

        #endregion

        #region Wrapper

        public override object GetField(Fwk.Main.Common.Enums.Fields.Fields field)
        {
            ErrorFields xrField = (ErrorFields)field;

            if (xrField == ErrorFields.Exception)
                return Exception;
            else if (xrField == ErrorFields.ErrorMessage)
                return Exception != null ? Exception.Message : "unknown error message";

            else
                return ErrorFields.NULL;
        }

        public override Fwk.Main.Common.Enums.Actions GetAction()
        {
            return Actions.ERROR;
        }

        #endregion
    }
}

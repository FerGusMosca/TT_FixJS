using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.DTO
{
    public class CMState
    {
        #region Public Attributes

        public bool Success { get; set; }
        public Exception Exception { get; set; }

        #endregion

        #region Public Static Methods

        public static CMState BuildSuccess()
        {
            return new CMState() { Success = true, Exception = null };
        }

        public static CMState BuildSuccess(bool success, string errorMsg)
        {
            if (success)
                return new CMState() { Success = success, Exception = null };
            else
                return new CMState() { Success = success, Exception = errorMsg != null ? new Exception(errorMsg) : new Exception("Unknown Error") };
        }

        public static CMState BuildFail(Exception ex)
        {
            return new CMState() { Success = false, Exception = ex };
        }

        #endregion
    }
}

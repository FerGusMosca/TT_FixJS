using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;

namespace Fwk.Main.Common.Wrappers
{
    public abstract class Wrapper
    {
        #region Public Abstract Methods

        public abstract object GetField(Fields field);

        public abstract Actions GetAction();

        #endregion

        #region Public Methods

        public virtual string ToString()
        {
            return "Wrapper";
        }
        #endregion
    }
}

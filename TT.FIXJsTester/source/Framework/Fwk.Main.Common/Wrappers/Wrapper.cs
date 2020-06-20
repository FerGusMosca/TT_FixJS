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
        #region Protected Attributes

        public string Sender { get; set; }


        #endregion


        #region Public Abstract Methods

        public virtual object GetField(Fields field)
        {
            if (field == Fields.SENDER)
                return Sender;
            else
                return Fields.NULL;
        }

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

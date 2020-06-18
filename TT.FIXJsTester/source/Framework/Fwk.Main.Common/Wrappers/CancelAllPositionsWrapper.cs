using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class CancelAllPositionsWrapper:Wrapper
    {
        #region Constructors

        public CancelAllPositionsWrapper()
        {
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return "";
        }

        public override Actions GetAction()
        {
            return Actions.CANCEL_ALL_POSITIONS;
        }


        public override object GetField(Fields field)
        {
            return PositionFields.NULL;
        }

        #endregion
    }
}

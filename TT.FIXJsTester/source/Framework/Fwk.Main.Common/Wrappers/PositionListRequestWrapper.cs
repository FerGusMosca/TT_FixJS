using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class PositionListRequestWrapper : Wrapper
    {
        public override object GetField(Fields field)
        {
            return Fields.NULL;
        }

        public override Enums.Actions GetAction()
        {
            return Actions.POSITION_LIST_REQUEST;
        }
    }
}

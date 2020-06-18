using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class DivSplitListField:Fields
    {
        public static readonly DivSplitListField DivSplitList = new DivSplitListField(2);

        protected DivSplitListField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

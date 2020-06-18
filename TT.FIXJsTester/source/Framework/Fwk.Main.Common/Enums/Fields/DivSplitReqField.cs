using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class DivSplitReqField : Fields
    {
        public static readonly DivSplitReqField Security = new DivSplitReqField(2);

        public static readonly DivSplitReqField FromDate = new DivSplitReqField(3);


        protected DivSplitReqField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

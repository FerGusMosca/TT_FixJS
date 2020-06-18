using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class PositionListFields:Fields
    {
        public static readonly PositionListFields Positions = new PositionListFields(2);

        public static readonly PositionListFields Status = new PositionListFields(3);

        public static readonly PositionListFields Error = new PositionListFields(4);

        protected PositionListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

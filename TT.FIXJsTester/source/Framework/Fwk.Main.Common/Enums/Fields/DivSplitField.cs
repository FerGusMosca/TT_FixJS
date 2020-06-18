using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class DivSplitField : Fields
    {
        public static readonly DivSplitField Security = new DivSplitField(2);

        public static readonly DivSplitField DeclaredDate = new DivSplitField(3);

        public static readonly DivSplitField ExDate = new DivSplitField(4);

        public static readonly DivSplitField RecordDate = new DivSplitField(5);

        public static readonly DivSplitField PayableDate = new DivSplitField(6);

        public static readonly DivSplitField DividendAmmount = new DivSplitField(7);

        public static readonly DivSplitField DividendFrequency = new DivSplitField(8);

        public static readonly DivSplitField DividendSplitType = new DivSplitField(9);

        public static readonly DivSplitField DividendSplitTypeDesc = new DivSplitField(10);

        protected DivSplitField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

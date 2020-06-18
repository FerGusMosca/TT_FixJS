using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class ReSyncPositionsField : Fields
    {
        public static readonly ReSyncPositionsField From = new ReSyncPositionsField(2);

        public static readonly ReSyncPositionsField Symbol = new ReSyncPositionsField(3);

        protected ReSyncPositionsField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

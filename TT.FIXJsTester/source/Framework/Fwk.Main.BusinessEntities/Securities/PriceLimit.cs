using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class PriceLimit
    {
        #region Public Attributes

        public double? LowLimitPrice { get; set; }

        public double? HighLimitPrice { get; set; }

        #endregion
    }
}

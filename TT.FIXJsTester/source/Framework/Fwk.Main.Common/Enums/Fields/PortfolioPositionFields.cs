using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class PortfolioPositionFields : Fields
    {
        public static readonly PortfolioPositionFields PortfolioPosition = new PortfolioPositionFields(2);


        protected PortfolioPositionFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

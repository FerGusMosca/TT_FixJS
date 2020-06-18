using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class PortfolioPositionListFields:Fields
    {
        public static readonly PortfolioPositionListFields PortfolioPositions = new PortfolioPositionListFields(2);

        public static readonly PortfolioPositionListFields Status = new PortfolioPositionListFields(3);

        public static readonly PortfolioPositionListFields Error = new PortfolioPositionListFields(4);

        protected PortfolioPositionListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

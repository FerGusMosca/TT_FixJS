using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class PortfolioPositionTradeListRequestFields : Fields
    {
        public static readonly PortfolioPositionTradeListRequestFields PositionId = new PortfolioPositionTradeListRequestFields(2);



        protected PortfolioPositionTradeListRequestFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

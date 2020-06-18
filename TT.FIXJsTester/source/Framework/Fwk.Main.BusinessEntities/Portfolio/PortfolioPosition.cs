using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Portfolio
{
    public class PortfolioPosition
    {
        #region Public Attributes

        public Security Security { get; set; }

        public List<PortfolioPosition> LinkedPositons { get; set; }

        public List<Trade> Trades { get; set; }

        public int Quantity { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }


        #endregion
    }
}

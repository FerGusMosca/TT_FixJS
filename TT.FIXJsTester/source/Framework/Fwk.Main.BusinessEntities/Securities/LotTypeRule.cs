using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Securities
{
    public class LotTypeRule
    {
        #region Public Attributes

        public char? LotType { get; set; }

        public int? MinLotSize { get; set; }

        public int? MaxLotSize { get; set; }

        #endregion
    }
}

using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Market_Data
{
    public class DividendSplitEvent
    {
        #region Public Attributes

        public Security Security { get; set; }

        public DateTime? DeclaredDate { get; set; }

        public DateTime? ExDate { get; set; }

        public DateTime? RecordDate { get; set; }

        public DateTime? PayableDate { get; set; }

        public double? DividendAmmount{ get; set; }

        public string DividendFrequency { get; set; }

        public DividendSplitType DividendSplitType { get; set; }

        public string DividendSplitTypeDesc { get; set; }

        #endregion
    }
}

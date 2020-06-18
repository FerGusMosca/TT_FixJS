using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class SecurityListFields : Fields
    {
        public static readonly SecurityListFields SecurityRequestResult = new SecurityListFields(2);

        public static readonly SecurityListFields SecurityListRequestType = new SecurityListFields(3);
        public static readonly SecurityListFields MarketID = new SecurityListFields(4);
        public static readonly SecurityListFields MarketSegmentID = new SecurityListFields(5);
        public static readonly SecurityListFields TotNoRelatedSym = new SecurityListFields(6);
        public static readonly SecurityListFields LastFragment = new SecurityListFields(7);
        public static readonly SecurityListFields NoRelatedSym = new SecurityListFields(8);
        public static readonly SecurityListFields Securities = new SecurityListFields(9);
        public static readonly SecurityListFields ContractPositionNumber = new SecurityListFields(10);

        protected SecurityListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }

    }
}

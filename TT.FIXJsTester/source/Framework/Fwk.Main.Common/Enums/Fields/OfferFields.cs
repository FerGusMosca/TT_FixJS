using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class OfferFields : Fields
    {
        public static readonly OfferFields Symbol = new OfferFields(2);

        public static readonly OfferFields MDEntryPx = new OfferFields(3);
        public static readonly OfferFields MDEntrySize = new OfferFields(4);
        public static readonly OfferFields MDEntryDate = new OfferFields(5);
        public static readonly OfferFields MDEntryTime = new OfferFields(6);
        public static readonly OfferFields TickDirection = new OfferFields(7);
        public static readonly OfferFields MDMkt = new OfferFields(8);
        public static readonly OfferFields QuoteCondition = new OfferFields(9);

        public static readonly OfferFields MDEntryID = new OfferFields(10);

        
        protected OfferFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

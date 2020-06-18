using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class BusinessMessageRejectField:  Fields
    {
        public static readonly BusinessMessageRejectField RefMsgType  = new BusinessMessageRejectField(2);
        public static readonly BusinessMessageRejectField BusinessRejectRefID = new BusinessMessageRejectField(3);
        public static readonly BusinessMessageRejectField BusinessRejectReason = new BusinessMessageRejectField(4);
        public static readonly BusinessMessageRejectField Text = new BusinessMessageRejectField(5);

        protected BusinessMessageRejectField(int pInternalValue)
            : base(pInternalValue)
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class RejectFields : Fields
    {
        public static readonly RejectFields RefSeqNum = new RejectFields(2);
        public static readonly RejectFields RefTagID = new RejectFields(3);
        public static readonly RejectFields RefMsgType = new RejectFields(4);
        public static readonly RejectFields SessionRejectReason = new RejectFields(5);
        public static readonly RejectFields Text = new RejectFields(6);



        protected RejectFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

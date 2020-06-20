using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class Fields
    {
        public static readonly Fields NULL = null;
        public static readonly Fields TEST = new Fields(1);
        public static readonly Fields KEY = new Fields(998);
        public static readonly Fields SENDER = new Fields(999);

        public int InternalValue { get; protected set; }

        protected Fields(int pInternalValue)
        {
            InternalValue = pInternalValue;
        }
    }

    
}

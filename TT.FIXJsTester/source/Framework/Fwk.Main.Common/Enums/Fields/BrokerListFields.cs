using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class BrokerListFields : Fields
    {
        public static readonly BrokerListFields Brokers = new BrokerListFields(2);

        public static readonly BrokerListFields Status = new BrokerListFields(3);

        public static readonly BrokerListFields Error = new BrokerListFields(4);

        protected BrokerListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

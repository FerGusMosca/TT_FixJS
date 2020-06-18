using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class SecurityListRequestField: Fields
    {
        public static readonly SecurityListRequestField Symbol = new SecurityListRequestField(2);
        public static readonly SecurityListRequestField SecurityListRequestType = new SecurityListRequestField(3);



        protected SecurityListRequestField(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class ErrorFields:Fields
    {
         public static readonly ErrorFields Exception = new ErrorFields(100);
         public static readonly ErrorFields ErrorMessage = new ErrorFields(101);

        protected ErrorFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

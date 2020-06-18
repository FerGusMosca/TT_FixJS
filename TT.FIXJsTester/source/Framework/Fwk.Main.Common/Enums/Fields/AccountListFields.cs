using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class AccountListFields : Fields
    {
        public static readonly AccountListFields Accounts = new AccountListFields(2);

        public static readonly AccountListFields Status = new AccountListFields(3);

        public static readonly AccountListFields Error = new AccountListFields(4);

        protected AccountListFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

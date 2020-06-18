using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums.Fields
{
    public class CryptoCurrencyFields : SecurityFields
    {
        public static readonly CryptoCurrencyFields MinConfirmation = new CryptoCurrencyFields(100);
        public static readonly CryptoCurrencyFields TxFee = new CryptoCurrencyFields(101);
        public static readonly CryptoCurrencyFields CoinType = new CryptoCurrencyFields(102);
        public static readonly CryptoCurrencyFields BaseAddress = new CryptoCurrencyFields(103);
        public static readonly CryptoCurrencyFields Notice = new CryptoCurrencyFields(104);

        protected CryptoCurrencyFields(int pInternalValue)
            : base(pInternalValue)
        {

        }
    }
}

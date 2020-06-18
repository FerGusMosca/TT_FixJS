using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum SettlType
    {
        Regular = '0',
        Cash = '1',
        NextDay = '2',
        Tplus2 = '3',
        Tplus3 = '4',
        Tplus4 = '5',
        Future = '6',
        Tplus5 = '9'
    }
}

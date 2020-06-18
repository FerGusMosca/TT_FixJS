using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum Halted
    {
        StatusNotAvailable = -1,
        NotHalted = 0,
        GeneralHalt = 1,
        VolatilityHalt = 2
    }
}

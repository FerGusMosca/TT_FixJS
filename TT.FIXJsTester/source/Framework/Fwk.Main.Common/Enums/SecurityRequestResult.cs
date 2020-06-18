using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum SecurityRequestResult
    {
        ValidRequest = 0,
        InvalidOrUnsupportedRequest = 1,
        NoInstrumentsFoundForSelectionCriteria = 2,
        NotAuthorizedToRetrieve = 3,
        InstrumentDataTemprarilyUnavailable = 4,
        RequestPerInstrument = 5,
        Unknown=99
    }
}

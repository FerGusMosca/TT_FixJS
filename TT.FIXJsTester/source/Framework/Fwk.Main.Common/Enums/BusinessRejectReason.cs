using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum BusinessRejectReason
    {
        Other = 0,
        UnknownID = 1,
        UnkwownSecurity = 2,
        UnsupportedMessageType = 3,
        ApplicationNotAvailable = 4,
        ConditionallyRequiredFieldMissing = 5
    }
}

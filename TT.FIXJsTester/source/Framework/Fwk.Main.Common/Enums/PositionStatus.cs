using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum PositionStatus
    {
        New = '0',
        PartiallyFilled = '1',
        Filled = '2',
        DoneForDay = '3',
        Canceled = '4',
        Replaced = '5',
        PendingCancel = '6',
        Stopped = '7',
        Rejected = '8',
        Suspended = '9',
        PendingNew = 'A',
        Calculated = 'B',
        Expired = 'C',
        AcceptedForBidding = 'D',
        PendingReplace = 'E',
        Unknown = 'x'
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.BusinessEntities.Positions;
using Fwk.Main.DataAccess;

namespace Fwk.Main.DataAccessLayer.Helpers
{
    public static class PositionHelper
    {
        public static Position Map(this positions source)
        {
            return Mapper.Map<positions, Position>(source);
        }

        public static positions Map(this Position source)
        {
            return Mapper.Map<Position, positions>(source);
        }
    }
}

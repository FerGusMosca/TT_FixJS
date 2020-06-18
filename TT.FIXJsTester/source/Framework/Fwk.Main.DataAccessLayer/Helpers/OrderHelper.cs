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
    public static class OrderHelper
    {
        public static Order Map(this orders source)
        {
            return Mapper.Map<orders, Order>(source);
        }

        public static orders Map(this Order source)
        {
            return Mapper.Map<Order, orders>(source);
        }
    }
}

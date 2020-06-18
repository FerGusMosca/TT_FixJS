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
    public static class ExecutionReportHelper
    {
        public static ExecutionReport Map(this execution_reports source)
        {
            return Mapper.Map<execution_reports, ExecutionReport>(source);
        }

        public static execution_reports Map(this ExecutionReport source)
        {
            return Mapper.Map<ExecutionReport, execution_reports>(source);
        }
    }
}

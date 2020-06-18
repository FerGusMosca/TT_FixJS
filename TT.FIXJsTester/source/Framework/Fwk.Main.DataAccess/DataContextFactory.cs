using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zHFT.Main.DataAccess;

namespace zHFT.StrategyHandler.StrategyHandler.DataAccess
{
    public class DataContextFactory
    {
        public static StrategyReportsEntities GetDataContext(string connString)
        {
            return new StrategyReportsEntities(connString);
        }
    }
}

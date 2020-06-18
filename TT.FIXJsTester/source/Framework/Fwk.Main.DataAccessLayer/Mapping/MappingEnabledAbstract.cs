using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.DataAccess;
using Fwk.StrategyHandler.StrategyHandler.DataAccess;

namespace Fwk.StrategyHandler.DataAccessLayer
{
    public class MappingEnabledAbstract
    {
        #region Protected Methods
        protected readonly StrategyReportsEntities ctx;
        protected AutoMapperConfiguration AutoMapperConfiguration { get; set; }
        #endregion

        #region Constructors
        public MappingEnabledAbstract(StrategyReportsEntities context)
        {
            ctx = context;
            AutoMapperConfiguration = new AutoMapperConfiguration();
            AutoMapperConfiguration.Configure();
        }

        public MappingEnabledAbstract(string connectionString)
        {
            ctx = DataContextFactory.GetDataContext(connectionString);
            AutoMapperConfiguration = new AutoMapperConfiguration();
            AutoMapperConfiguration.Configure();
        }


        #endregion
    }
}

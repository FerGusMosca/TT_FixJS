using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class PortfolioPositionTradeListRequestWrapper:Wrapper
    {
        #region Constructor

        public PortfolioPositionTradeListRequestWrapper(string portfPostId)
        {
            PortfPostId = portfPostId;
        }

        #endregion

        #region Protected Attributes

        protected string PortfPostId { get; set; }

        #endregion

        #region Wrapper Methods

        public override object GetField(Enums.Fields.Fields field)
        {
            PortfolioPositionTradeListRequestFields xrField = (PortfolioPositionTradeListRequestFields)field;

            if (xrField == PortfolioPositionTradeListRequestFields.PositionId)
                return PortfPostId;
            else
                return ExecutionReportFields.NULL;
        }

        public override Enums.Actions GetAction()
        {
            return Actions.PORTFOLIO_POSITION_TRADE_LIST_REQUEST;
        }

        #endregion
    }
}

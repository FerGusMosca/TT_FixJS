using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Wrappers.Order_Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXGenericTesterModule.Common.Wrappers.Order_Routing
{
    public class NewOrderWrapper : OrderWrapper
    {
        #region Constructors

        public NewOrderWrapper(Order pOrder, FIXGenericTesterModule.Common.Configuration.Configuration pConfig)
        {
            Order = pOrder;

            Config = pConfig;

            Sender = pConfig.Name;
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            if (Order != null)
            {
                return "";//TO DO : Desarrollar el método to string
            }
            else
                return "";
        }

        public override Actions GetAction()
        {
            return Actions.NEW_ORDER;
        }

        #endregion

    }
}

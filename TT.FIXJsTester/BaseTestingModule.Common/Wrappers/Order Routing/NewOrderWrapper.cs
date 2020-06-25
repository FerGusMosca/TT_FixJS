using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTestingModule.Common.Wrappers.Order_Routing
{
    public class NewOrderWrapper : OrderWrapper
    {
        #region Constructors

        public NewOrderWrapper(Order pOrder, BaseConfiguration pConfig)
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

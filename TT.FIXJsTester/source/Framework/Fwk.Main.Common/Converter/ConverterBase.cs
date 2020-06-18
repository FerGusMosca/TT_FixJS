using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Wrappers;
using Fwk.Main.Common.Enums.Fields;

namespace Fwk.Main.Common.Converter
{
    public class ConverterBase
    {

        #region Protected Attributes

        protected IConfiguration Config { get; set; }

        #endregion

        #region Protected Methods
        protected bool ValidateField(Wrapper wrapper, Fields field)
        {
            return  wrapper.GetField(field) != Fields.NULL;
        }


        protected void ValidatePosition(Wrapper wrapper)
        {
            if (!ValidateField(wrapper, PositionFields.PosId))
                throw new Exception("Missing position id");

            if (!ValidateField(wrapper, PositionFields.Symbol))
                throw new Exception("Missing position symbol");

            if (!ValidateField(wrapper, PositionFields.QuantityType))
                throw new Exception("Missing position quantity type");

            if (!ValidateField(wrapper, PositionFields.Side))
                throw new Exception("Missing position side");

            QuantityType qt = (QuantityType)wrapper.GetField(PositionFields.QuantityType);

            if (qt == QuantityType.SHARES || qt==QuantityType.BONDS || qt == QuantityType.CONTRACTS)
            {
                if (!ValidateField(wrapper, PositionFields.Qty))
                    throw new Exception("Missing position quantity");
            }

            if (qt == QuantityType.CURRENCY)
            {
                if (!ValidateField(wrapper, PositionFields.CashQty))
                    throw new Exception("Missing position cash quantity");

            }

        }
        #endregion
    }
}

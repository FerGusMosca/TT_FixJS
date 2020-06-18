using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Wrappers
{
    public class CancelPositionWrapper : Wrapper
    {
         #region Constructors

        public CancelPositionWrapper(string posId)
        {
            PosId = posId;
        }

        #endregion

        #region Attributes


        protected string PosId { get; set; }


        #endregion

        #region Public Methods

        public override string ToString()
        {
            return "";
        }

        public override Actions GetAction()
        {
            return Actions.CANCEL_POSITION;
        }


        public override object GetField(Fields field)
        {
            PositionFields xrField = (PositionFields)field;


            if (xrField == PositionFields.PosId)
                return PosId;
            else
                return PositionFields.NULL;
        }

        #endregion
    }
}

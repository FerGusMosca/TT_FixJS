using Fwk.Main.Common.Enums;
using Fwk.Main.Common.Enums.Fields;
using Fwk.Main.Common.Wrappers;
using QuickFix.FIX44;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXCommModule.Common.Wrappers.Generic
{
    public class RejectWrapper:Wrapper
    {
        #region Constructor

        public RejectWrapper(string pKey,Reject pReject)
        {
            Key = pKey;
            Reject = pReject;
        
        }


        #endregion

        #region Attributes

        protected string Key { get; set; }

        protected Reject Reject { get; set; }

        #endregion


        #region Wrapper Methods

        public override object GetField(Fields field)
        {
            if (field is RejectFields)
            {
                RejectFields rejField = (RejectFields)field;

                if (rejField == RejectFields.RefSeqNum)
                    return Reject.GetInt(QuickFix.Fields.Tags.RefSeqNum);
                else if (rejField == RejectFields.RefTagID)
                    return Reject.GetField(QuickFix.Fields.Tags.RefTagID);
                else if (rejField == RejectFields.RefMsgType)
                    return Reject.GetField(QuickFix.Fields.Tags.RefMsgType);
                else if (rejField == RejectFields.SessionRejectReason)
                    return Reject.GetField(QuickFix.Fields.Tags.SessionRejectReason);
                else if (rejField == RejectFields.Text)
                    return Reject.GetField(QuickFix.Fields.Tags.Text);
                else
                    return Fields.NULL;
            }
            else if (field == Fields.KEY)
                return Key;
            else
                return base.GetField(field);
        }

        public override Actions GetAction()
        {
            return Actions.REJECT;
        }

        #endregion
    }
}

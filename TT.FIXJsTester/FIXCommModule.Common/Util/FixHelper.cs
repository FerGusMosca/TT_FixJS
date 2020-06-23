using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXCommModule.Common.Util
{
    public class FixHelper
    {
        public static double? GetNullDoubleFieldIfSet(QuickFix.Message message,int field)
        {
            if (message.IsSetField(field))
                return Convert.ToDouble(message.GetDecimal(field));
            else
                return null;
        
        }

        public static DateTime? GetDateTimeFieldIfSet(QuickFix.Message message, int field)
        {
            if (message.IsSetField(field))
                return message.GetDateTime(field);
            else
                return null;
        }

        public static string GetNullFieldIfSet(QuickFix.Message message, int field)
        {
            if (message.IsSetField(field))
                return message.GetString(field);
            else
                return null;
        
        }

        public static int? GetNullIntFieldIfSet(QuickFix.Message message, int field)
        {

            if (message.IsSetField(field))
                return message.GetInt(field);
            else
                return null;
        
        }

        public static char? GetCharFieldIfSet(QuickFix.Message message, int field)
        {

            if (message.IsSetField(field))
                return message.GetChar(field);
            else
                return null;

        }
    }
}

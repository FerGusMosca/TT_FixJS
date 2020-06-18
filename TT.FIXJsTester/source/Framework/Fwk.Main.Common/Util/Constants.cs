using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Util
{
    public class Constants
    {
        public const string XmlInvalid = "Invalid XML configuration file  {0}";

        public const string MissingConfigParam = "Missing config parameter  {0}";

        public const string FieldMissing = "Configuration field: {0} is missing. using default value.";

        public const string InvalidKey = "Key not valid {0}: {1}";

        public const string InvalidField = "Field not valid {0}: {1}";

        public enum MessageType { Information, Debug, Error, Exception, EndLog };
    }
}

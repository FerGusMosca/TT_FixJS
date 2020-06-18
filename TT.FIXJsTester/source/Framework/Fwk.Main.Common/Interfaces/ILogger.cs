using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.Util;

namespace Fwk.Main.Common.Interfaces
{
    public interface ILogger
    {
        void DoLog(string msg, Constants.MessageType type);

        void DoLoadConfig(string configFile, List<string> listaCamposSinValor);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.Common.Wrappers;

namespace Fwk.SingletonModulesHandler.Common.Interfaces
{
    public interface ISingletonModule
    {
        void DoLoadConfig(string configFile, List<string> listaCamposSinValor);

        void DoLog(string msg, Constants.MessageType type);

        void SetOutgoingEvent(OnMessageReceived OnMessageRcv);

        void SetIncomingEvent(OnMessageReceived OnMessageRcv);

        CMState ProcessMessage(Wrapper wrapper);
    }
}

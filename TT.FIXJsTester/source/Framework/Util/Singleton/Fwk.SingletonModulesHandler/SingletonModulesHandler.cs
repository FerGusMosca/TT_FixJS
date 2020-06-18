using Fwk.Main.Common.Abstract;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Interfaces;
using Fwk.SingletonModulesHandler.Common.Enums;
using Fwk.SingletonModulesHandler.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.SingletonModulesHandler
{
    public class SingletonModulesHandler : BaseCommunicationModule
    {
        #region Protected Attributes

        ISingletonModule SingletonHandler { get; set; }

        public IConfiguration Config { get; set; }

        protected Common.Configuration.Configuration SingletonModuleConfiguration
        {
            get { return (Common.Configuration.Configuration)Config; }
            set { Config = value; }
        }

        #endregion

        #region Protected Methods

        protected override void DoLoadConfig(string configFile, List<string> listaCamposSinValor)
        {
            List<string> noValueFields = new List<string>();
            Config = new Common.Configuration.Configuration().GetConfiguration<Common.Configuration.Configuration>(configFile, noValueFields);

        }

        #endregion

        #region Public Methods

        public override Main.Common.DTO.CMState ProcessMessage(Main.Common.Wrappers.Wrapper wrapper)
        {
            try
            {
                CMState state = SingletonHandler.ProcessMessage(wrapper);

                if (state.Success)
                    DoLog(string.Format("@{0}:Publishing Wrapper for action {1}: {2} ", SingletonModuleConfiguration.Name, wrapper.GetAction(),
                                        wrapper.ToString()), Main.Common.Util.Constants.MessageType.Debug);
                else
                    DoLog(string.Format("@{0}:Error Publishing Wrapper. Error={1} ",
                                        state.Exception != null ? state.Exception.Message : "",
                                        SingletonModuleConfiguration.Name),
                                        Main.Common.Util.Constants.MessageType.Error);

                return state;

            }
            catch (Exception ex)
            {
                DoLog(string.Format("@{0}:Error Publishing Wrapper. Error={1} ",
                                            ex != null ? ex.Message : "",
                                            SingletonModuleConfiguration.Name),
                                            Main.Common.Util.Constants.MessageType.Error);
                throw;
            }
        }

        public override bool Initialize(OnMessageReceived pOnMessageRcv, OnLogMessage pOnLogMsg, string configFile)
        {
            try
            {
                this.OnMessageRcv += pOnMessageRcv;
                this.OnLogMsg += pOnLogMsg;

                if (LoadConfig(configFile))
                {
                    var singletonHandlerClass = Type.GetType(SingletonModuleConfiguration.SingletonAssembly);
                    if (singletonHandlerClass != null)
                    {
                        SingletonHandler = (ISingletonModule)singletonHandlerClass.GetMethod("GetInstance").Invoke(null, new object[] { pOnLogMsg, SingletonModuleConfiguration.SingletonConfigFile });

                        if (SingletonModuleConfiguration.ModuleDirection == ModuleDirection.Incoming)
                            SingletonHandler.SetIncomingEvent(pOnMessageRcv);
                        else if (SingletonModuleConfiguration.ModuleDirection == ModuleDirection.Outgoing)
                            SingletonHandler.SetOutgoingEvent(pOnMessageRcv);
                        else
                            throw new Exception(string.Format("Unkown module direction for {0}", SingletonModuleConfiguration.ModuleDirection));

                    }
                    else
                    {
                        DoLog("assembly not found: " + SingletonModuleConfiguration.SingletonAssembly, Main.Common.Util.Constants.MessageType.Error);
                        return false;
                    }

                    return true;
                }
                else
                {
                    DoLog("Error initializing config file " + configFile, Main.Common.Util.Constants.MessageType.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                DoLog("Critic error initializing " + configFile + ":" + ex.Message, Main.Common.Util.Constants.MessageType.Error);
                return false;
            }
        }

        #endregion
    }
}

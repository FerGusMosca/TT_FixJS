using Fwk.Main.Common.Apps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsShared.Logging;
using Fwk.Main.Common.Apps;
using Fwk.Main.Common.Configuration;
using Fwk.Main.Common.DTO;
using Fwk.Main.Common.Interfaces;
using Fwk.Main.Common.Util;
using Fwk.Main.Common.Wrappers;

namespace Fwk.Main
{
    public class MainApp : MainAppBase
    {
        #region Private Attributes

        protected ICommunicationModule OutgoingModule { get; set; }

        protected ICommunicationModule IncomingModule { get; set; }

        public Configuration Config { get; set; }

        protected OnLogMessage OnLogMsg { get; set; }

        #endregion

        #region Constructors

        public MainApp(ILogSource p_LogIncommingHost, ILogSource p_LogOutgoingHost, ILogSource p_LogSource, string configFile,
                       OnLogMessage onLog)
        {
            try
            {
                this.MessageIncomingLogger = p_LogIncommingHost;
                this.MessageOutgoingLogger = p_LogOutgoingHost;
                this.appLogger = p_LogSource;
                this.OnLogMsg += onLog;

                if (!String.IsNullOrWhiteSpace(configFile))
                    ConfigFile = configFile;

                State = Bussiness.ApplicationBase.ApplicationState.Created;

                bool result = LoadConfig(ConfigFile);

                if (!string.IsNullOrEmpty(Config.OutgoingModule))
                {
                    var typeOutgoingModule = Type.GetType(Config.OutgoingModule);
                    if (typeOutgoingModule != null)
                    {
                        OutgoingModule = (ICommunicationModule)Activator.CreateInstance(typeOutgoingModule);
                    }
                    else
                        Log("assembly not found: " + Config.OutgoingModule);
                }
                else
                    Log("Outgoing Module not found. It will not be initialized");

                if (!string.IsNullOrEmpty(Config.IncomingModule))
                {
                    var typeIncomingModule = Type.GetType(Config.IncomingModule);
                    if (typeIncomingModule != null)
                    {
                        IncomingModule = (ICommunicationModule)Activator.CreateInstance(typeIncomingModule);
                    }
                    else
                        Log("Assembly not found: " + Config.IncomingModule);
                }
                else
                    Log("Incoming Module not found. It will not be initialized");
            }
            catch (Exception e)
            {
                Log("Error initializing service: " + e.Message);
                EventLog.WriteEntry(Config.Name, e.Message + e.StackTrace.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }
        
        #endregion

        #region Private and Protected Methods

        protected bool LoadConfig(string configFile)
        {
            Log(DateTime.Now.ToString() + "MainApp.LoadConfig", Constants.MessageType.Information);

            Log("Loading config:" + configFile, Constants.MessageType.Information);
            if (!File.Exists(configFile))
            {
                Log("ConfigFile inexistente");
                return false;
            }

            List<string> noValueFields = new List<string>();
            Log("Proccessing config:" + configFile, Constants.MessageType.Information);
            try
            {
                Config = new Configuration().GetConfiguration <Configuration>(configFile, noValueFields);
                Log("Fin GetConfiguracion", Constants.MessageType.Information);
            }
            catch (Exception e)
            {
                Log("Error recovering config: " + e.Message, Constants.MessageType.Error);
                return false;
            }

            if (noValueFields.Count > 0)
                noValueFields.ForEach(s => Log(string.Format(Constants.MissingConfigParam, s), Constants.MessageType.Error));

            return true;
        }


        protected override CMState ProcessOutgoing(Wrapper wrapper)
        {
            //StrongLogOutgoing("ProcessOutgoing: " + wrapper.ToString(), Constants.MessageType.Information);
            Console.WriteLine("ProcessOutgoing: " + wrapper.ToString());
            try
            {
                if (IncomingModule != null)
                    return IncomingModule.ProcessMessage(wrapper);
                else
                    return CMState.BuildSuccess(false, "Incoming module not set...");
            }
            catch (Exception ex)
            {
                LogOutgoing("Error processing incoming message: " + ex.Message, Constants.MessageType.Error);
                return CMState.BuildFail(ex);
            }
        }

        public bool EvalLogging(Fwk.Main.Common.Util.Constants.MessageType type)
        {

            if (Config.LogLevel == Fwk.Main.Common.Configuration.Configuration._LOG_LEVEL_INFO && type != Fwk.Main.Common.Util.Constants.MessageType.Information)
                return false;//We only need info messages

            if (Config.LogLevel == Fwk.Main.Common.Configuration.Configuration._LOG_LEVEL_ERROR &&
                (type != Fwk.Main.Common.Util.Constants.MessageType.Information
                && type != Fwk.Main.Common.Util.Constants.MessageType.Error
                && type != Fwk.Main.Common.Util.Constants.MessageType.Exception))
                return false;//We only need info + error messages

            return true;
        }

        protected override CMState ProcessIncoming(Wrapper wrapper)
        {
            //StrongLogIncoming("ProcessIncoming..." + wrapper.ToString(), Constants.MessageType.Information);
            
            try
            {
                if (OutgoingModule != null)
                    return OutgoingModule.ProcessMessage(wrapper);
                else
                    return CMState.BuildSuccess(false, "Outgoing module not set...");
            }
            catch (Exception ex)
            {
                LogIncoming("Error processing outgoing message: " + ex.Message, Constants.MessageType.Error);
                return CMState.BuildFail(ex); ;
            }
        }

        public void LogIncoming(string message, Constants.MessageType type)
        {
            if(EvalLogging(type))
                MessageIncomingLogger.Debug(message, type);

            if (OnLogMsg != null)
                OnLogMsg(message, type);
        }

        public void StrongLogIncoming(string message, Constants.MessageType type)
        {
            if(EvalLogging(type))
                MessageIncomingLogger.Debug(message, type);

            if (OnLogMsg != null)
                OnLogMsg((Config.LogToConsole ? "toConsole->" : "") + message, type);
        }



        public void LogOutgoing(string message, Constants.MessageType type)
        {
            if(EvalLogging(type))
                MessageOutgoingLogger.Debug(message, type);

            if (OnLogMsg != null)
                OnLogMsg(message, type);
        }

        public void StrongLogOutgoing(string message, Constants.MessageType type)
        {
            if(EvalLogging(type))
                MessageOutgoingLogger.Debug(message, type);

            if (OnLogMsg != null)
                OnLogMsg((Config.LogToConsole ? "toConsole->" : "") + message, type);
        }

        #endregion

        #region Public Methods

        public override void CloseSession()
        {
            //TO DO: Implementar el cierre de sesión contra los modulos si corresponde
        }

        public override void Run()
        {
            try
            {
                if (Config != null)
                {

                    Log("Initializing Log");
                    State = Bussiness.ApplicationBase.ApplicationState.Logging;

                    Log("Initializing Incoming Module");
                    if (IncomingModule != null)
                    {
                        if (IncomingModule.Initialize(ProcessIncoming, LogIncoming, Config.IncomingConfigPath))
                            Log("Incoming Module successfully initialized");
                        else
                            Log("Error initializing Incoming Module");
                    }
                    else
                        Log("Incoming module could not be initialized because it was not instantiated");

                    Log("Initializing Outgoing Module");
                    if (OutgoingModule != null)
                    {
                        if (OutgoingModule.Initialize(ProcessOutgoing, LogOutgoing, Config.OutgoingConfigPath))
                            Log("Outgoing Module successfully initialized");
                        else
                            Log("Error initializing Outgoing Module");
                    }
                    else
                        Log("Outgoing module could not be initialized because it was not instantiated");

                    State = Bussiness.ApplicationBase.ApplicationState.Logged;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CMState ProcessMessageToIncoming(Wrapper wrapper)
        {
            try
            {
                Log("ProcessMessageToIncoming");
                return IncomingModule.ProcessMessage(wrapper);
            }
            catch (Exception ex)
            {
                Log(string.Format("ProcessMessageToIncoming Error: {0}", ex.Message));
                return CMState.BuildFail(ex);
            }
        }

        public CMState ProcessMessageToOutgoing(Wrapper wrapper)
        {
            try
            {
                Log("ProcessMessageToOutgoing");
                return OutgoingModule.ProcessMessage(wrapper);
            }
            catch (Exception ex)
            {
                Log(string.Format("ProcessMessageToOutgoing Error: {0}", ex.Message));
                return CMState.BuildFail(ex);
            }
        }

        #endregion
    }
}

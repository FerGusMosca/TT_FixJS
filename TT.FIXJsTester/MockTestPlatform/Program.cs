using Bussiness.Auxiliares;
using Fwk.Main;
using Fwk.Main.Common.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsShared.Logging;

namespace MockTestPlatform
{
    class Program
    {
        private static bool ToConsole { get; set; }

        private static MainApp App { get; set; }

        private static ILogSource AppLogger { get; set; }


        public static void DoLog(string msg, Constants.MessageType type)
        {
            lock (App)
            {
                if (type == Fwk.Main.Common.Util.Constants.MessageType.Error ||
                    type == Fwk.Main.Common.Util.Constants.MessageType.Exception)
                {
                    AppLogger.Debug(msg, Constants.MessageType.Error);
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (type == Fwk.Main.Common.Util.Constants.MessageType.Debug)
                {
                    AppLogger.Debug(msg, Constants.MessageType.Debug);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (type == Fwk.Main.Common.Util.Constants.MessageType.AssertFailed)
                {
                    AppLogger.Debug(msg, Constants.MessageType.Error);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }

                if (type == Fwk.Main.Common.Util.Constants.MessageType.AssertOk)
                {
                    AppLogger.Debug(msg, Constants.MessageType.Information);
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                if (type == Fwk.Main.Common.Util.Constants.MessageType.Information)
                {
                    AppLogger.Debug(msg, Constants.MessageType.Information);
                }

                if (ToConsole)
                {
                    Console.WriteLine(msg);
                }
                else if (msg.StartsWith("toConsole->"))
                {
                    Console.WriteLine(msg.Replace("toConsole->", ""));
                    Console.WriteLine("");
                }

                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            string configFile = Const.ConfigFileDefault;


            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            configFile = Directory.GetCurrentDirectory() + "\\" + configFile;
            ToConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["allwaysToConsole"]);
          

            AppLogger = new PerDayFileLogSource(Directory.GetCurrentDirectory() + "\\Log", Directory.GetCurrentDirectory() + "\\Log\\Backup")
            {
                FilePattern = "Log.{0:yyyy-MM-dd}.log",
                DeleteDays = 20
            };



            App = new MainApp(AppLogger, AppLogger, AppLogger, configFile, Program.DoLog);

            App.Run();

            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}

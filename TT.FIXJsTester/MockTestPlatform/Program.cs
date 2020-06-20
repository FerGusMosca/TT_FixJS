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


        public static void DoLog(string msg, Constants.MessageType type)
        {
            if (type == Fwk.Main.Common.Util.Constants.MessageType.Error ||
                type == Fwk.Main.Common.Util.Constants.MessageType.Exception)
                Console.ForegroundColor = ConsoleColor.Red;

            if (type == Fwk.Main.Common.Util.Constants.MessageType.Debug)
                Console.ForegroundColor = ConsoleColor.Yellow;

            if (type == Fwk.Main.Common.Util.Constants.MessageType.AssertFailed)
                Console.ForegroundColor = ConsoleColor.DarkRed;

            if (type == Fwk.Main.Common.Util.Constants.MessageType.AssertOk)
                Console.ForegroundColor = ConsoleColor.Green;

            if (ToConsole)
                Console.WriteLine(msg);
            else if (msg.StartsWith("toConsole->"))
            {
                Console.WriteLine(msg.Replace("toConsole->", ""));
                Console.WriteLine("");
            }

            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            string configFile = Const.ConfigFileDefault;


            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            configFile = Directory.GetCurrentDirectory() + "\\" + configFile;
            ToConsole = Convert.ToBoolean(ConfigurationManager.AppSettings["allwaysToConsole"]);
            ILogSource appLogger;

            appLogger = new PerDayFileLogSource(Directory.GetCurrentDirectory() + "\\Log", Directory.GetCurrentDirectory() + "\\Log\\Backup")
            {
                FilePattern = "Log.{0:yyyy-MM-dd}.log",
                DeleteDays = 20
            };



            App = new MainApp(appLogger, appLogger, appLogger, configFile, Program.DoLog);

            App.Run();

            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}

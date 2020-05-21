using QuickFix;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolsShared.Logging;

namespace FIXAcceptor
{
    public class Program:Application
    {
        #region Public Static Attributes

        public static PerDayFileLogSource AppLogger { get; set; }

        protected static SessionSettings SessionSettings { get; set; }
        protected static FileStoreFactory FileStoreFactory { get; set; }
        protected static ScreenLogFactory ScreenLogFactory { get; set; }
        protected static SessionID SessionID { get; set; }
        protected static MessageFactory MessageFactory { get; set; }
        protected static SocketAcceptor Acceptor { get; set; }

        #endregion

        static void Main(string[] args)
        {
            AppLogger = new PerDayFileLogSource(Directory.GetCurrentDirectory() + "\\Log", Directory.GetCurrentDirectory() + "\\Log\\Backup")
            {
                FilePattern = "Log.{0:yyyy-MM-dd}.log",
                DeleteDays = 20
            };

            string path = ConfigurationManager.AppSettings["AcceptorPath"];

            SessionSettings = new SessionSettings(path);
            FileStoreFactory = new FileStoreFactory(SessionSettings);
            ScreenLogFactory = new ScreenLogFactory(SessionSettings);
            MessageFactory = new DefaultMessageFactory();

            Program myProgram = new Program();

            Acceptor = new SocketAcceptor(myProgram, FileStoreFactory, SessionSettings, ScreenLogFactory, MessageFactory);

            Acceptor.start();

            Console.WriteLine("Acceptor successfully started...");
            Console.ReadKey();
        }

        #region Application Methods

        public void fromAdmin(Message value, SessionID sessionId)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@fromAdmin:{0}", value.ToString()));
        }

        public void fromApp(Message value, SessionID sessionId)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@fromApp:{0}", value.ToString()));
        }

        public void onCreate(SessionID value)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@onCreate:{0}", value.ToString()));
        }

        public void onLogon(SessionID value)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@onLogon:{0}", value.ToString()));
            SessionID = value;
        }

        public void onLogout(SessionID value)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@onLogout:{0}", value.ToString()));
        }

        public void toAdmin(Message value, SessionID sessionId)
        {
            AppLogger.Debug(value.ToString());

            if (value is QuickFix42.Logon)
            {
                //QuickFix42.Logon logon = (QuickFix42.Logon)value;

                //int msgSeq =  Convert.ToInt32(logon.getHeader().getField(MsgSeqNum.FIELD));

                //if(msgSeq==1)//Reset On StartTime
                //{
                //    logon.setChar(ResetSeqNumFlag.FIELD, ResetSeqNumFlag.YES);
                //}
            }

            Console.WriteLine(string.Format("@toAdmin:{0}", value.ToString()));
        }

        public void toApp(Message value, SessionID sessionId)
        {
            AppLogger.Debug(value.ToString());
            Console.WriteLine(string.Format("@toApp:{0}", value.ToString()));
        }

        #endregion
    }
}

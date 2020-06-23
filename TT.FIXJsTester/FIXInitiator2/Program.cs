using QuickFix;
using QuickFix.FIX44;
using QuickFix.Transport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolsShared.Logging;

namespace FIXInitiator2
{
    class Program:IApplication
    {
        #region Public Static Attributes

        public static PerDayFileLogSource AppLogger { get; set; }

        protected static SessionSettings SessionSettings { get; set; }
        protected static FileStoreFactory FileStoreFactory { get; set; }
        protected static ScreenLogFactory ScreenLogFactory { get; set; }
        protected static SessionID SessionID { get; set; }
        protected static MessageFactory MessageFactory { get; set; }
        protected static SocketInitiator Initiator { get; set; }

        #endregion

        #region Private Static Conts

        #endregion

        #region Private Methods

        public void StartPublishingExecutionReports()
        {
            Thread exReportsThread = new Thread(PublishFakeExecutionReport);

            exReportsThread.Start();
        }

        public void PublishFakeExecutionReport()
        {

            int i = 1;
            while (true)
            {
                ExecutionReport exReport = new ExecutionReport();

                //exReport.setField(new ExecID(i.ToString()));
                //exReport.setField(new ExecTransType(ExecTransType.NEW));
                //exReport.setField(new ClOrdID(i.ToString()));
                //exReport.setField(new OrderID(i.ToString()));
                //exReport.setField(new ExecType(ExecType.NEW));
                //exReport.setField(new OrdStatus(OrdStatus.NEW));
                //exReport.setField(new TransactTime(DateTime.Now));
                //exReport.setField(new LeavesQty(1));
                //exReport.setField(new CumQty(0));
                //exReport.setField(new AvgPx(0));
                //exReport.setField(new MultiLegReportingType(MultiLegReportingType.SINGLE));
                //exReport.setField(new SecurityID("TVPP"));
                //exReport.setField(new Symbol("TVPP"));
                //exReport.setField(new Account("1"));
                ////exReport.setField(new OrderCapacity(OrderCapacity.PRINCIPAL));
                ////exReport.setField(new CustOrderCapacity(CustOrderCapacity.MEMBER_TRADING_FOR_THEIR_OWN_ACCOUNT));
                //exReport.setField(new Side(Side.BUY));
                //exReport.setField(new OrdType(OrdType.MARKET));
                //exReport.setField(new OpenClose(OpenClose.OPEN));
                //exReport.setField(new TimeInForce(TimeInForce.GOOD_TILL_CANCEL));

                //Session.sendToTarget(exReport, SessionID);

                i++;
                Thread.Sleep(1000);
            }


        }

        #endregion

        static void Main(string[] args)
        {
            AppLogger = new PerDayFileLogSource(Directory.GetCurrentDirectory() + "\\Log", Directory.GetCurrentDirectory() + "\\Log\\Backup")
            {
                FilePattern = "Log.{0:yyyy-MM-dd}.log",
                DeleteDays = 20
            };

            string path = ConfigurationManager.AppSettings["InitiatorPath"];

            SessionSettings = new SessionSettings(path);
            FileStoreFactory = new FileStoreFactory(SessionSettings);
            ScreenLogFactory = new ScreenLogFactory(SessionSettings);
            MessageFactory = new QuickFix.FIX44.MessageFactory();

            Program myProgram = new Program();

            Initiator = new SocketInitiator(myProgram, FileStoreFactory, SessionSettings, ScreenLogFactory);
            
            Initiator.Start();


            Console.WriteLine("Initiator successfully started...");
            Console.ReadKey();
        }

     



        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            AppLogger.Debug(message.ToString());

            Console.WriteLine(string.Format("@fromAdmin:{0}", message.ToString()));
 
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
        {
            AppLogger.Debug(message.ToString());
            Console.WriteLine(string.Format("@fromApp:{0}", message.ToString()));

        }

        public void OnCreate(SessionID sessionID)
        {
            AppLogger.Debug(sessionID.ToString());
            Console.WriteLine(string.Format("@onCreate:{0}", sessionID.ToString()));
        }

        public void OnLogon(SessionID sessionID)
        {
            AppLogger.Debug(sessionID.ToString());
            Console.WriteLine(string.Format("@onLogon:{0}", sessionID.ToString()));
            SessionID = sessionID;
            
            StartPublishingExecutionReports();
        }

        public void OnLogout(SessionID sessionID)
        {
            AppLogger.Debug(sessionID.ToString());
            Console.WriteLine(string.Format("@onLogout:{0}", sessionID.ToString()));
        }

        public void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            AppLogger.Debug(message.ToString());
            Console.WriteLine(string.Format("@toAdmin:{0}", message.ToString()));
        }

        public void ToApp(QuickFix.Message message, SessionID sessionId)
        {
            AppLogger.Debug(message.ToString());
            Console.WriteLine(string.Format("@toApp:{0}", message.ToString()));
        }
    }
}

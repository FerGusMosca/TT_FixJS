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

        protected void ProcesssNewOrderSingle(QuickFix44.NewOrderSingle order)
        {
            string clOrdId = order.getString(ClOrdID.FIELD);
            string account = order.getString(Account.FIELD);
            string symbol = order.getString(Symbol.FIELD);
            double lvsQty = order.getDouble(OrderQty.FIELD);
            char ordType = order.getChar(OrdType.FIELD);
            char side = order.getChar(Side.FIELD);

            QuickFix44.ExecutionReport exReport = new QuickFix44.ExecutionReport();

            int i = 0;

            exReport.setField(new ExecID(i.ToString()));
            exReport.setField(new ExecTransType(ExecTransType.NEW));
            exReport.setField(new ClOrdID(clOrdId));
            exReport.setField(new OrderID(Guid.NewGuid().ToString()));
            exReport.setField(new ExecType(ExecType.NEW));
            exReport.setField(new OrdStatus(OrdStatus.NEW));
            exReport.setField(new TransactTime(DateTime.Now));
            exReport.setField(new LeavesQty(lvsQty));
            exReport.setField(new CumQty(0));
            exReport.setField(new AvgPx(0));
            exReport.setField(new MultiLegReportingType(MultiLegReportingType.SINGLE));
            exReport.setField(new Symbol(symbol));
            exReport.setField(new Account(account));
            //exReport.setField(new OrderCapacity(OrderCapacity.PRINCIPAL));
            //exReport.setField(new CustOrderCapacity(CustOrderCapacity.MEMBER_TRADING_FOR_THEIR_OWN_ACCOUNT));
            exReport.setField(new Side(side));
            exReport.setField(new OrdType(ordType));
            exReport.setField(new OpenClose(OpenClose.OPEN));
            exReport.setField(new TimeInForce(TimeInForce.GOOD_TILL_CANCEL));

            Session.sendToTarget(exReport, SessionID);
        
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

            if (value is QuickFix44.NewOrderSingle)
            {
                ProcesssNewOrderSingle((QuickFix44.NewOrderSingle)value);
            }
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

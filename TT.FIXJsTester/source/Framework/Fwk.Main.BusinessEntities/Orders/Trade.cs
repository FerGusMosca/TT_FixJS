using Fwk.Main.BusinessEntities.Securities;
using Fwk.Main.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.BusinessEntities.Orders
{
    public class Trade
    {

        #region Public Static Consts

        public static string _TRADE_ID_PREFIX = "trd_";

        #endregion

        #region Private Static Consts

        private static string _New = "New";
        private static string _DoneForDay = "DoneForDay";
        private static string _Canceled = "Canceled";
        private static string _Replaced = "Replaced";
        private static string _PendingCancel = "PendingCancel";
        private static string _Stopped = "Stopped";
        private static string _Rejected = "Rejected";
        private static string _Suspended = "Suspended";
        private static string _PendingNew = "PendingNew";
        private static string _Calculated = "Calculated";
        private static string _Expired = "Expired";
        private static string _PendingReplace = "PendingReplace";
        private static string _PartiallyFilled = "PartiallyFilled";
        private static string _Filled = "Filled";
        private static string _Unknown = "Unknown";

        #endregion

        #region Public Attribute

        public string TradeId { get; set; }

        public Security Security { get; set; }

        public int QuantityRequested { get; set; }

        public int QuantityFilled { get; set; }

        public Side Side { get; set; }

        public OrdStatus Status { get; set; }

        public string StatusDesc { get { return GetStrStatus(); } }

        public int LeavesQty { get; set; }

        public DateTime? LastFillTime { get; set; }

        public decimal? AveragePrice { get; set; }

        public string OrderId { get; set; }

        public int PositionId { get; set; }

        public string AccountId { get; set; }

        public string AccountDesc { get; set; }

        public string Msg { get; set; }

        public DateTime CreateTime { get; set; }

        public bool? CFD { get; set; }

        public int Timestamp 
        { 
            get 
            {
                TimeSpan elapsed = CreateTime - new DateTime(1970, 1, 1);
                return Convert.ToInt32(elapsed.TotalSeconds);
            } 
        
        }

        //public int Timestamp {
            
        //    get {

        //        TimeSpan elapsed = (LastFillTime.HasValue ? LastFillTime.Value : DateTime.Now) - new DateTime(1970, 1, 1);
        //            return Convert.ToInt32(elapsed.TotalSeconds);
              
        //    }
        
        //}

        #endregion

        #region Public Methods

        public string GetStrStatus()
        {

            if (Status == OrdStatus.New)
                return _New;
            else if (Status == OrdStatus.DoneForDay)
                return _DoneForDay;
            else if (Status == OrdStatus.Canceled)
                return _Canceled;
            else if (Status == OrdStatus.Replaced)
                return _Replaced;
            else if (Status == OrdStatus.PendingCancel)
                return _PendingCancel;
            else if (Status == OrdStatus.Stopped)
                return _Stopped;
            else if (Status == OrdStatus.Rejected)
                return _Rejected;
            else if (Status == OrdStatus.Suspended)
                return _Suspended;
            else if (Status == OrdStatus.PendingNew)
                return _PendingNew;
            else if (Status == OrdStatus.Calculated)
                return _Calculated;
            else if (Status == OrdStatus.Expired)
                return _Expired;
            else if (Status == OrdStatus.PendingReplace)
                return _PendingReplace;
            else if (Status == OrdStatus.PartiallyFilled)
                return _PartiallyFilled;
            else if (Status == OrdStatus.Filled)
                return _Filled;
            else
                return _Unknown;
        
        }

        public static OrdStatus GetStatus(string Status)
        {

            if (Status == _New)
                return OrdStatus.New;
            else if (Status == _DoneForDay)
                return  OrdStatus.DoneForDay;
            else if (Status == _Canceled)
                return  OrdStatus.Canceled;
            else if (Status == _Replaced)
                return  OrdStatus.Replaced;
            else if (Status == _PendingCancel)
                return OrdStatus.PendingCancel;
            else if (Status == _Stopped)
                return  OrdStatus.Stopped;
            else if (Status == _Rejected)
                return  OrdStatus.Rejected;
            else if (Status == _Suspended)
                return  OrdStatus.Suspended;
            else if (Status == _PendingNew)
                return  OrdStatus.PendingNew;
            else if (Status == _Calculated)
                return  OrdStatus.Calculated;
            else if (Status == _Expired)
                return  OrdStatus.Expired;
            else if (Status == _PendingReplace)
                return  OrdStatus.PendingReplace;
            else if (Status == _PartiallyFilled)
                return  OrdStatus.PartiallyFilled;
            else if (Status == _Filled)
                return  OrdStatus.Filled;
            else
                return OrdStatus.Undefined;

        }

        public static string GetTradeId(string symbol,string orderId)
        {
            return string.Format("{0}_{1}_{2}", _TRADE_ID_PREFIX, symbol, orderId);
        }

        public bool IsTraded()
        {
            return (Status == OrdStatus.PartiallyFilled || Status == OrdStatus.Filled)
                    || (QuantityFilled>0);
        
        }

        public bool IsStillActiveTrade() 
        {
            return Status == OrdStatus.AcceptedForBidding || Status == OrdStatus.Calculated
                  || Status == OrdStatus.New || Status == OrdStatus.PartiallyFilled
                  || Status == OrdStatus.PendingCancel || Status == OrdStatus.PendingNew
                  || Status == OrdStatus.PendingReplace || Status == OrdStatus.Replaced;
        }

        public bool IsLongTrade()
        { 
            return Side ==Side.Buy || Side==Side.BuyToClose;
        }


       


        #endregion
    }
}

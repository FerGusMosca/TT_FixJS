using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.Common.Enums
{
    public enum Actions
    {
        MARKET_DATA,
        MARKET_DATA_REQUEST,
        ORDER_BOOK,
        OFFER,
        EXECUTION_REPORT,
        NEW_POSITION,
        SECURITY,
        NEW_POSITION_CANCELED,
        NEW_ORDER,
        UPDATE_ORDER,
        CANCEL_ORDER,
        CANCEL_POSITION,
        CANCEL_ALL_POSITIONS,
        SECURITY_LIST,
        SECURITY_LIST_REQUEST,
        ORDER_CANCEL_REJECT,
        BUSINESS_MESSAGE_REJECT,
        REJECT,
        ORDER_LIST,
        POSITION_LIST_REQUEST,
        POSITION_LIST,
        EXECUTION_REPORT_LIST_REQUEST,
        EXECUTION_REPORT_LIST,
        PORTFOLIO_POSITIONS_REQUEST,
        PORTFOLIO_POSITIONS,
        PORTFOLIO_POSITION,
        PORTFOLIO_POSITION_TRADE_LIST_REQUEST,
        TRADE_LIST,
        TRADE,
        ACCOUNTS_REQUEST,
        ACCOUNT_LIST,
        BROKERS_REQUEST,
        BROKERS_LIST,
        POSITION_LOAD,
        POSITION_UPDATE,
        POSITION_DELETE,
        POSITION_SUSPEND,
        POSITION_OVERRIDE_BAND_1,
        POSITION_OVERRIDE_LAST_TRADE_REF_PRICE,
        POSITION_OVERRIDE_QTY,
        RESYNC_POSITION_REQ,
        DIV_SPLIT_SYNC_REQ,
        DIV_SPLIT,
        DIV_SPLIT_LIST,
        ERROR
    }
}

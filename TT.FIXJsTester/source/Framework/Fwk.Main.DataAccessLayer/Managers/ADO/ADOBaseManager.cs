using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fwk.Main.DataAccessLayer.Managers.ADO
{
    public class ADOBaseManager
    {
        #region Protected Attributes

        protected SqlConnection Conn { get; set; }


        #endregion

        #region Public Methods

        public void Dispose()
        {
            Conn.Dispose();
        }

        #endregion
    }
}

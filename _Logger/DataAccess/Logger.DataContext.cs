using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Logger.DataAccess
{
    public class LoggerDataContext : DbContext
    {
        public LoggerDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert a new record into server log
        /// </summary>
        public void insertServerLog(string serverSessionID, string clientSessionID, string recordType, string recordValue)
        {
            this.Database.ExecuteSqlCommand("Application.usp_InsServerLog @p0, @p1, @p2, @p3", serverSessionID, clientSessionID, recordType, recordValue);
        }

        /// <summary>
        /// Insert a new record into client log
        /// </summary>
        public void insertClientLog(string clientSessionID, string clientTime, string user, string recordType, string recordValue)
        {
            this.Database.ExecuteSqlCommand("Application.usp_InsClientLog @p0, @p1, @p2, @p3, @p4", clientSessionID, clientTime, user, recordType, recordValue);
        }


    }
}
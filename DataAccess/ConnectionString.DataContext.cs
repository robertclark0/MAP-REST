using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models;

namespace MAP_REST.DataAccess
{
    public class ConnectionStringDataContext : DbContext
    {
        public ConnectionStringDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get DB Connection String
        /// </summary>
        /// <returns>Returns MAP product lines</returns>
        public Models.Connection getConnectionString(string entityCode, string environmentCode, string userType = "U")
        {
            return this.Database.SqlQuery<Models.Connection>("usp_GetConnectionString @p0, @p1, @p2", entityCode, environmentCode, userType).FirstOrDefault();
        }

    }
}
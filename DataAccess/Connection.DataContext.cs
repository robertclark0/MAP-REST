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
    public class ConnectionDataContext : DbContext
    {
        public ConnectionDataContext(string nameOrConnectionString = "name=MAP")
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
            return this.Database.SqlQuery<Models.Connection>("Application.usp_GetConnectionString @p0, @p1, @p2", entityCode, environmentCode, userType).FirstOrDefault();
        }

        public Models.DataSource getDataSource(string alias)
        {
            return this.Database.SqlQuery<Models.DataSource>("Product.usp_GetDataSourceByAlias @p0", alias).FirstOrDefault();
        }

    }
}
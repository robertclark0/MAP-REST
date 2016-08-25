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
    public class ProductLineDataContext : DbContext
    {
        public ProductLineDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }


        public List<Models.ProductLine> getProductLine()
        {
            return this.Database.SqlQuery<Models.ProductLine>("usp_GetProductLine").ToList();
        }

        public List<Models.DataSource> getDataSource(string EntityCode)
        {
            return this.Database.SqlQuery<Models.DataSource>("usp_GetDataSource @p0", EntityCode).ToList();
        }

        public List<Models.DataSourceParameters> getDataSourceParameters(int DataSourceParameterID )
        {
            return this.Database.SqlQuery<Models.DataSourceParameters>("usp_GetDataSourceParameters @p0", DataSourceParameterID).ToList();
        }

    }
}
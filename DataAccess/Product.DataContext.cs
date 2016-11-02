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
    public class ProductDataContext : DbContext
    {
        public ProductDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }


        public List<Models.Product> getProductLine()
        {
            return this.Database.SqlQuery<Models.Product>("Product.usp_GetProductLine").ToList();
        }
        public List<Models.ModuleList> getModules(string EntityCode)
        {
            return this.Database.SqlQuery<Models.ModuleList>("Product.usp_GetProductLineModule @p0", EntityCode).ToList();
        }

        public List<Models.DataSource> getDataSource(string EntityCode)
        {
            return this.Database.SqlQuery<Models.DataSource>("Product.usp_GetDataSource @p0", EntityCode).ToList();
        }
        public List<Models.DataSourceParameters> getDataSourceParameters(int DataSourceParameterID )
        {
            return this.Database.SqlQuery<Models.DataSourceParameters>("Product.usp_GetDataSourceParameters @p0", DataSourceParameterID).ToList();
        }

    }
}
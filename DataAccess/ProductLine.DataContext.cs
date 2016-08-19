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

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get MAP Product Lines
        /// </summary>
        /// <returns>Returns MAP product lines</returns>
        public List<Models.ProductLine> getProductLine()
        {
            return this.Database.SqlQuery<Models.ProductLine>("usp_GetProductLine").ToList();
        }

    }
}
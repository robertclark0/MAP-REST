using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;


namespace MAP_REST.DataAccess
{
    public class SchemaDataContext : DbContext
    {
        public SchemaDataContext(string nameOrConnectionString = "name=Master")
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public List<Models.Schema> getTableSchema(string entityCode, string tableName)
        {
            string SQL = "SELECT [COLUMN_NAME],[DATA_TYPE] FROM [MAP_{0}].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{1}' ORDER BY 1";

            return this.Database.SqlQuery<Models.Schema>(String.Format(SQL, entityCode, tableName)).ToList();
        }


    }
}
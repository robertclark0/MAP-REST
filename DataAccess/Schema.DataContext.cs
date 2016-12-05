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
        public SchemaDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Models.DataSource getDataSource(string alias)
        {
            return this.Database.SqlQuery<Models.DataSource>("Product.usp_GetDataSourceByAlias @p0", alias).FirstOrDefault();
        }

        public List<Models.Schema> getTableSchema(string catalog, string tableName)
        {
            string SQL = "SELECT [COLUMN_NAME],[DATA_TYPE] FROM [{0}].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{1}' ORDER BY 1";

            return this.Database.SqlQuery<Models.Schema>(String.Format(SQL, catalog, tableName)).ToList();
        }

        public dynamic getColumnDistinct(string entityCode, string tableName, string columnName, string order = null)
        {
            var items = new List<object>();

            this.Database.Connection.Open();
            var cmd = this.Database.Connection.CreateCommand();

            switch (order)
            {
                case "asc":
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}] ORDER BY 1 ASC", columnName, entityCode, tableName);
                    break;

                case "desc":
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}] ORDER BY 1 DESC", columnName, entityCode, tableName);
                    break;

                default:
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}]", columnName, entityCode, tableName);
                    break;
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(reader[0]);
            }

            return items;
        }


    }
}
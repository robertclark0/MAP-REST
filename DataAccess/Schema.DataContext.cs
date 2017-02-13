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
        public SchemaDataContext(string nameOrConnectionString = "name=MAP")
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

        public dynamic getColumnDistinct(string entityCode, string tableName, string[] columnNames, string order = null)
        {
            var items = new List<object>();

            this.Database.Connection.Open();
            var cmd = this.Database.Connection.CreateCommand();

            switch (order)
            {
                case "asc":
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}] ORDER BY {3}", distinctColumns(columnNames), entityCode, tableName, multiDistinctOrder(columnNames, "ASC"));
                    break;

                case "desc":
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}] ORDER BY {3}", distinctColumns(columnNames), entityCode, tableName, multiDistinctOrder(columnNames, "DESC"));
                    break;

                default:
                    cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [{1}].dbo.[{2}]", distinctColumns(columnNames), entityCode, tableName);
                    break;
            }

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new List<Object>();             

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    item.Add(reader[i]);
                }

                items.Add(item);
            }            
            return items;
        }

        public string distinctColumns(string[] columnNames)
        {
            return String.Join("],[", columnNames);
        }

        public string multiDistinctOrder(string[] columnNames, string direction)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                columnNames[i] = String.Format("[{0}] {1}", columnNames[i], direction);
            }
            return String.Join(",", columnNames);
        }

    }
}
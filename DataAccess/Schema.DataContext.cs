﻿using System;
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

        public List<Models.Schema> getTableSchema(string entityCode, string tableName)
        {
            string SQL = "SELECT [COLUMN_NAME],[DATA_TYPE] FROM [MAP_{0}].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{1}' ORDER BY 1";

            return this.Database.SqlQuery<Models.Schema>(String.Format(SQL, entityCode, tableName)).ToList();
        }

        public dynamic getColumnDistinct(string entityCode, string tableName, string columnName)
        {
            var items = new List<object>();

            this.Database.Connection.Open();
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = String.Format("SELECT DISTINCT [{0}] FROM [MAP_{1}].dbo.[{2}]", columnName, entityCode, tableName);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(reader[0]);
            }

            return items;
        }


    }
}
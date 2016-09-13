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
    public class QueryDataContext : DbContext
    {
        public QueryDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }


        public dynamic QueryData(string SQLString)
        {
            var items = new List<List<object>>();

            this.Database.Connection.Open();
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = SQLString;

            var reader = cmd.ExecuteReader();
            int readerRow = 0;
            while (reader.Read())
            {
                if (readerRow == 0)
                {
                    var header = new List<Object>();
                    items.Add(header);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        header.Add(reader.GetName(i));
                    }
                }

                var item = new List<Object>();
                items.Add(item);

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    item.Add(reader[i]);
                }
                readerRow++; 
            }

            return items;
        }

    }
}
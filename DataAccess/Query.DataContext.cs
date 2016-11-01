using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models;
using System.IO;
using Logger.BusinessLogic;

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

            try
            {
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
            catch (Exception e)
            {
                Log.ServerLog(Log.GenerateServerSessionID(), "query-error", e.ToString(), null);
                return null;
            }
            finally
            {
                this.Database.Connection.Close();
            }


        }

        public void QueryCSVWriter(string SQLString, string path)
        {

            try
            {
                this.Database.Connection.Open();
                var cmd = this.Database.Connection.CreateCommand();
                cmd.CommandText = SQLString;

                var reader = cmd.ExecuteReader();
                int readerRow = 0;

                //using (var writer = new StreamWriter(path))
                using (StreamWriter writer = File.AppendText(path))
                {
                    while (reader.Read())
                    {
                        if (readerRow == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader.GetName(i) + ",");
                            }
                            writer.Write("\r\n");
                        }

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string cell = Convert.ToString(reader[i]).Replace(',', '/');
                            writer.Write(cell + ",");
                        }

                        writer.Write("\r\n");
                        readerRow++;
                    }
                }
            }
            catch (Exception e)
            {
                Log.ServerLog(Log.GenerateServerSessionID(), "cvs-query-error", e.ToString(), null);
            }
            finally
            {
                this.Database.Connection.Close();
            }

        }

    }
}
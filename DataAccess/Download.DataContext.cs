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
    public class DownloadDataContext : DbContext
    {
        public DownloadDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }

        public void setQueryJSON(string GUID, string JSON)
        {
            this.Database.ExecuteSqlCommand("usp_InsDownload @p0, @p1", GUID, JSON);
        }
        public Models.QueryJSON getQueryJSON(string GUID)
        {
            return this.Database.SqlQuery<Models.QueryJSON>("usp_GetDownload @p0", GUID).FirstOrDefault();
        }
        public void deleteQueryJSON(string GUID)
        {
            this.Database.ExecuteSqlCommand("usp_DeleteDownload @p0", GUID);
        }

    }
}
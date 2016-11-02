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

        public void setDownloadStatus(string GUID, string JSON, string status)
        {
            this.Database.ExecuteSqlCommand("Application.usp_InsDownload @p0, @p1, @p2", GUID, JSON, status);
        }
        public Models.DownloadStatus getDownloadStatus(string GUID)
        {
            return this.Database.SqlQuery<Models.DownloadStatus>("Application.usp_GetDownload @p0", GUID).FirstOrDefault();
        }
        public void deleteDownloadStatus(string GUID)
        {
            this.Database.ExecuteSqlCommand("Application.usp_DeleteDownload @p0", GUID);
        }
        public void updateDownloadStatus(string GUID, string status)
        {
            this.Database.ExecuteSqlCommand("Application.usp_UpdateDownload @p0, @p1", GUID, status);
        }

    }
}
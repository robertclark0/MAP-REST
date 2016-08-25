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
    public class ReportDataContext : DbContext
    {
        public ReportDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }



        public List<Models.ReportList> getReportList(string EntityCode)
        {
            return this.Database.SqlQuery<Models.ReportList>("usp_GetReportList @p0", EntityCode).ToList();
        }

        public List<Models.Report> getReport(string GUID)
        {
            return this.Database.SqlQuery<Models.Report>("usp_GetReport @p0", GUID).ToList();
        }

        public void createReport()
        {
            this.Database.ExecuteSqlCommand("usp_CreateReport");
        }

        public void updateReport()
        {
            this.Database.ExecuteSqlCommand("usp_UpdateReport");
        }

        public void deleteReport()
        {
            this.Database.ExecuteSqlCommand("usp_DeleteReport");
        }

    }
}
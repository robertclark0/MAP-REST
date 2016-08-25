﻿using System;
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

        public void createReport(string EntityCode, string GUID, string User, string Report_Name, string Report_Type, string JSON)
        {
            this.Database.ExecuteSqlCommand("usp_CreateReport @p0 @p1 @p2 @p3 @p4 @p5", EntityCode, GUID, User, Report_Name, Report_Type, JSON);
        }

        public void updateReport(string GUID, string User, string Report_Name, string Report_Type, string JSON)
        {
            this.Database.ExecuteSqlCommand("usp_UpdateReport @p0 @p1 @p2 @p3 @p4", GUID, User, Report_Name, Report_Type, JSON);
        }

        public void deleteReport(string GUID)
        {
            this.Database.ExecuteSqlCommand("usp_DeleteReport @p0", GUID);
        }

    }
}
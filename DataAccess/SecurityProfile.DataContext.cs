using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models.SecurityProfile;


namespace MAP_REST.DataAccess
{
    public class SecurityProfileDataContext : DbContext
    {
        public SecurityProfileDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }

        public Definition getProfileDefinition(string entityCode)
        {
            return this.Database.SqlQuery<Definition>("[SecurityProfile].[usp_GetProfileDefinition] @p0", entityCode).FirstOrDefault();
        }

        public List<AnalysisModule> getAnalysisModule(int SecurityProfileID)
        {
            return this.Database.SqlQuery<AnalysisModule>("[SecurityProfile].[usp_GetAnalysisModule] @p0", SecurityProfileID).ToList();
        }

        public List<DataExport> getDataExport(int SecurityProfileID)
        {
            return this.Database.SqlQuery<DataExport>("[SecurityProfile].[usp_GetDataExport] @p0", SecurityProfileID).ToList();
        }

        public List<DataFilter> getDataFilter(int SecurityProfileID)
        {
            return this.Database.SqlQuery<DataFilter>("[SecurityProfile].[usp_GetDataFilter] @p0", SecurityProfileID).ToList();
        }

        public List<ReportVisibility> getReportVisibility(int SecurityProfileID)
        {
            return this.Database.SqlQuery<ReportVisibility>("[SecurityProfile].[usp_GetReportVisibility] @p0", SecurityProfileID).ToList();
        }

        public List<SaveReport> getSaveReport(int SecurityProfileID)
        {
            return this.Database.SqlQuery<SaveReport>("[SecurityProfile].[usp_GetSaveReport] @p0", SecurityProfileID).ToList();
        }

    }
}
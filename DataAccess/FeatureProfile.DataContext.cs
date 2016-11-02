using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models.FeatureProfile;


namespace MAP_REST.DataAccess
{
    public class FeatureProfileDataContext : DbContext
    {
        public FeatureProfileDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }

        public Profile getProfileDefinition(string entityCode)
        {
            return this.Database.SqlQuery<Profile>("FeatureProfile.usp_GetProfileDefinition @p0", entityCode).FirstOrDefault();
        }

        public List<AnalysisModule> getAnalysisModule(int SecurityProfileID)
        {
            return this.Database.SqlQuery<AnalysisModule>("FeatureProfile.usp_GetAnalysisModule @p0", SecurityProfileID).ToList();
        }

        public List<DataExport> getDataExport(int SecurityProfileID)
        {
            return this.Database.SqlQuery<DataExport>("FeatureProfile.usp_GetDataExport @p0", SecurityProfileID).ToList();
        }

        public List<DataFilter> getDataFilter(int SecurityProfileID)
        {
            return this.Database.SqlQuery<DataFilter>("FeatureProfile.usp_GetDataFilter @p0", SecurityProfileID).ToList();
        }

        public List<ReportVisibility> getReportVisibility(int SecurityProfileID)
        {
            return this.Database.SqlQuery<ReportVisibility>("FeatureProfile.usp_GetReportVisibility @p0", SecurityProfileID).ToList();
        }

        public List<SaveReport> getSaveReport(int SecurityProfileID)
        {
            return this.Database.SqlQuery<SaveReport>("FeatureProfile.usp_GetSaveReport @p0", SecurityProfileID).ToList();
        }

    }
}
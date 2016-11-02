using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models.FeatureProfile
{
    public class Profile
    {
        public int FeatureProfileID { get; set; }
        public string DataQuery { get; set; }
        public string ReportVisibility { get; set; }
        public string DataExport { get; set; }
        public string AnalysisModule { get; set; }
        public string SaveCanvas { get; set; }
        public string SaveReport { get; set; }
        public string DataFilter { get; set; }

        public List<ReportVisibility> ReportVisbilityRoles { get; set; }
        public List<DataExport> DataExportRoles { get; set; }
        public List<AnalysisModule> AnalysisModuleRoles { get; set; }
        public List<SaveReport> SaveReportRoles { get; set; }
    }

    public class ReportVisibility
    {
        public int ReportVisibilityID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class DataExport
    {
        public int DataExportID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class AnalysisModule
    {
        public int AnalysisModuleID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class SaveReport
    {
        public int SaveReportID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class DataFilter
    {
        public int DataFilterID { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public int DataSourceID { get; set; }
        public string SourceName { get; set; }
        public string DataValue { get; set; }
    }
}
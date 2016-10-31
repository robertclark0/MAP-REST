using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.DataAccess;
using MAP_REST.Models.SecurityProfile;

namespace MAP_REST.BusinessLogic
{
    public class SecurityProfile
    {
        public Profile getSecurityProfile(string entityCode)
        {
            var db = new SecurityProfileDataContext();
            var definition = new Definition();

            definition = db.getProfileDefinition(entityCode);

            var profile = new Profile();
            profile.Code = definition.Code;
            profile.DataQuery = definition.DataQuery;
            profile.SaveCanvas = definition.SaveCanvas;

            profile.ReportVisibility = definition.ReportVisibility;
            if (profile.ReportVisibility == "rol")
            {
                profile.ReportVisbilityRoles = db.getReportVisibility(definition.SecurityProfileID);
            }

            profile.DataExport = definition.DataExport;
            if (profile.DataExport == "rol")
            {
                profile.DataExportRoles = db.getDataExport(definition.SecurityProfileID);
            }

            profile.AnalysisModule = definition.AnalysisModule;
            if (profile.AnalysisModule == "rol")
            {
                profile.AnalysisModuleRoles = db.getAnalysisModule(definition.SecurityProfileID);
            }

            profile.SaveReport = definition.SaveReport;
            if (profile.SaveReport == "rol")
            {
                profile.SaveReportRoles = db.getSaveReport(definition.SecurityProfileID);
            }

            profile.DataFilter = definition.DataFilter;
            if (profile.DataFilter == "rol" || profile.DataFilter == "res")
            {
                profile.DataFilterRoles = db.getDataFilter(definition.SecurityProfileID);
            }

            return profile;
        }
    }
}
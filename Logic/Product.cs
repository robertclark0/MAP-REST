using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.Models;
using MAP_REST.DataAccess;

namespace MAP_REST.BusinessLogic
{
    public class Product
    {
        public List<Models.Product> getProducts()
        {
            var db = new ProductDataContext();
            var fp = new FeatureProfileDataContext();
            var products = new List<Models.Product>();
            products = db.getProductLine();

            foreach (Models.Product p in products)
            {
                p.Modules = db.getModules(p.Code);
                p.DataSources = db.getDataSource(p.Code);
                p.FeatureProfile = fp.getProfileDefinition(p.Code);

                if (p.FeatureProfile != null)
                {
                    p.FeatureProfile.ReportVisbilityRoles = fp.getReportVisibility(p.FeatureProfile.FeatureProfileID);
                    p.FeatureProfile.DataExportRoles = fp.getDataExport(p.FeatureProfile.FeatureProfileID);
                    p.FeatureProfile.AnalysisModuleRoles = fp.getAnalysisModule(p.FeatureProfile.FeatureProfileID);
                    p.FeatureProfile.SaveReportRoles = fp.getSaveReport(p.FeatureProfile.FeatureProfileID);
                }
            }

            return products;
        }
    }
}
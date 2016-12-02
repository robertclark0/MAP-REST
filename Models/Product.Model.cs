using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public string Icon { get; set; }
        public string IconClass { get; set; }
        public List<ModuleList> Modules { get; set; }
        public List<SourceAlias> DataSources { get; set; }
        public FeatureProfile.Profile FeatureProfile { get; set;}
    }

    public class ModuleList
    {
        public string Module { get; set; }
        public int IsDefault { get; set; }
    }

    public class SourceAlias
    {
        public int DataSourceID { get; set; }
        public string Alias { get; set; }
        public string SourceType { get; set; }
    }

    public class DataSourceParameters
    {
        public int DataSourceParameterID { get; set; }
        public string ParameterName { get; set; }
        //public string DataType { get; set; }
        //public string ParameterType { get; set; }
        //public string TableReference { get; set; }
        //public string ColumnReference { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class ProductLine
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public string Icon { get; set; }
        public string IconClass { get; set; }
        public int HasPII { get; set; }
        public string Module { get; set; }
        public string ModuleName { get; set; }

    }

    public class DataSource
    {
        public string Code { get; set; }
        public string DataSourceID { get; set; }
        public string SourceName { get; set; }
        public string SourceType { get; set; }
    }

    public class DataSourceParameters
    {
        public string DataSourceParameterID { get; set; }
        public string Parametername { get; set; }
        public string DataType { get; set; }
        public string ParameterType { get; set; }
        public string TableReference { get; set; }
        public string ColumnReference { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class Schema
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
    }

    public class SchemaDataSource
    {
        public string SourceName { get; set; }
        public string Catalog { get; set; }
    }
}
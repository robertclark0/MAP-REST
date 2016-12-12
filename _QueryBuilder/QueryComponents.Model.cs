using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.QueryBuilder
{
    public class QueryComponents
    {
        public string selections { get; set; }
        public string filters { get; set; }
        public string grouping { get; set; }
        public string ordering { get; set; }
        public string source { get; set; }
    }
}
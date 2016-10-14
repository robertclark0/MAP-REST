using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class ReportList
    {
        public int ReportID { get; set; }
        public string GUID { get; set; }
        public string User { get; set; }
        public string Report_Name { get; set; }
        public string Report_Type { get; set; }
        public int Position{ get; set; }
        public string Category { get; set; }
        public DateTime AuditDate { get; set; }
    }

    public class Report
    {
        public int ReportID { get; set; }
        public string GUID { get; set; }
        public string User { get; set; }
        public string Report_Name { get; set; }
        public string Report_Type { get; set; }
        public string JSON { get; set; }
        public int Position { get; set; }
        public string Category { get; set; }
        public DateTime AuditDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class CanvasList
    {
        public int CanvasID { get; set; }
        public string GUID { get; set; }
        public string User { get; set; }
        public string Canvas_Name { get; set; }
        public DateTime AuditDate { get; set; }
    }

    public class Canvas
    {
        public int CanvasID { get; set; }
        public string GUID { get; set; }
        public string User { get; set; }
        public string Canvas_Name { get; set; }
        public string JSON { get; set; }
        public DateTime AuditDate { get; set; }
    }
}
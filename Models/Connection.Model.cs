using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.Models
{
    public class Connection
    {
        public string Code { get; set; }
        public int ConnectionStringID { get; set; }
        public string Environment { get; set; }
        public string ConnectionString { get; set; }
    }
}
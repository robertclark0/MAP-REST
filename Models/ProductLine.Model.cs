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
}
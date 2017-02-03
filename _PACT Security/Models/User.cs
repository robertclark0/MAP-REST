using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PACT.Models
{
    public class User
    {
        public int UID { get; set; }
        public string EDIPN { get; set; }
        public string akoUserID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string dmisID { get; set; }
        public string RHCName { get; set; }
        public string userEmail { get; set; }
        public List<AuthorizedProduct> AuthorizedProducts { get; set; }
    }

    public class AuthorizedProduct
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public List<Authorization> Authorizations { get; set; }
    }

    public class Authorization
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
    }

    //This eventually need to change. PACT changes should allow better data restriction ability to conform to the feature profile
    public class DMIS
    {
        public string dmisID { get; set; }
    }
}
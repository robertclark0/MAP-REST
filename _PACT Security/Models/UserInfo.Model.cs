using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PACT.Models
{
    public class UserInfo
    {
        public int UID { get; set; }
        public string EDIPN { get; set; }
        public string akoUserID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string dmisID { get; set; }
        public string RHCName { get; set; }
        public string userEmail { get; set; }
    }

    public class ProductUserInfo
    {
        public User UserInfo { get; set; }
        public List<Role> RoleInfo { get; set; }
        public List<Dmis> DmisInfo { get; set; }
    }

    public class User
    {
        public string akoUserID { get; set; }
        public string ediPN { get; set; }
        public string dmisID { get; set; }
        public string RHCName { get; set; }
    }

    public class Role
    {
        public string roleName { get; set; }
    }

    public class Dmis
    {
        public string dmisID { get; set; }
        public string parentDmis { get; set; }
        public string RHCName { get; set; }
        public string DMISFacilityName { get; set; }
        public string FacilityName { get; set; }
    }
}
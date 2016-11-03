using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.BusinessLogic;
using PACT.Models;
using PACT.DataAccess;
using System.Text.RegularExpressions;

namespace PACT.BusinessLogic
{
    public class User
    {
        public Models.User getUserData()
        {
            var connection = Credentials.getConnectionString("PACT");
            var db = new SecurityDataContext(connection.ConnectionString);

            var AKOID = getAKOID();
            var EDIPN = getEDIPN();
            var user = new Models.User();
           
            if (EDIPN != null)
                user = db.getUser(EDIPN, "EDIPN");
            else
                user = db.getUser(AKOID, "AKOID");

            if (user != null)
            {
                user.AuthorizedProducts = db.getProducts(user.EDIPN, "EDIPN");

                foreach (AuthorizedProduct p in user.AuthorizedProducts)
                {
                    p.Authorizations = db.GetUserRoles(user.akoUserID, p.productName, user.EDIPN);
                }
                return user;
            }
            return unauthorizedUser(AKOID, EDIPN);
        }

        public string getAKOID()
        {
            var akoUserId = ServerVariables.GetServerVariable("HTTP_UID");

            if (akoUserId == null && System.Web.HttpContext.Current.Request.LogonUserIdentity != null)
            {
                akoUserId = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            if (akoUserId != null)
            {
                var index = akoUserId.IndexOf("\\") + 1;
                if (index != -1)
                {
                    akoUserId = akoUserId.Substring(index, akoUserId.Length - index);
                }
            }
            return akoUserId;
        }

        public string getEDIPN()
        {
            return ServerVariables.GetServerVariable("HTTP_ARMYEDIPI");
        }

        public Models.User unauthorizedUser(string AKOID, string EDIPN)
        {
            string[] AKOPartials = AKOID.Split('.');

            var user = new Models.User();
            user.UID = -1;
            user.EDIPN = EDIPN;
            user.akoUserID = AKOID;
            user.fName = AKOPartials[0];

            return user;
        }
    }
}
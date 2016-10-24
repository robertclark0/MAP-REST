using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.BusinessLogic;
using PACT.DAL;

namespace PACT.BLL
{
    public class UserProduct
    {
        public static Models.ProductUserInfo GetUserProduct(string productName)
        {
            var akoUserId = ServerVariables.GetServerVariable("HTTP_UID");  //PRODUCTION
            var EDIPI = ServerVariables.GetServerVariable("HTTP_ARMYEDIPI");  // PRODUCTION
            if (akoUserId == null && HttpContext.Current.Request.LogonUserIdentity != null)
                akoUserId = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (akoUserId != null)
            {
                var index = akoUserId.IndexOf("\\") + 1;
                if (index != -1) //Domain string parse it out
                {
                    akoUserId = akoUserId.Substring(index, akoUserId.Length - index);
                }
            }

            var userProduct = new Models.ProductUserInfo();
            var connection = Credentials.getConnectionString("PACT");
            var db = new SecurityDataContext(connection.ConnectionString);

            if (EDIPI != null)
            {
                userProduct.UserInfo = db.GetUserInfo(akoUserId, productName, EDIPI);
                userProduct.RoleInfo = db.GetUserRoles(akoUserId, productName, EDIPI);
                userProduct.DmisInfo = db.GetUserDmis(akoUserId, productName, EDIPI);
            }
            else
            {
                userProduct.UserInfo = db.GetUserInfo(akoUserId, productName);
                userProduct.RoleInfo = db.GetUserRoles(akoUserId, productName);
                userProduct.DmisInfo = db.GetUserDmis(akoUserId, productName);
            }

            return userProduct;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MAP_REST.BusinessLogic;
using PACT.DAL;

namespace PACT.BLL
{
    public class UserInfo
    {
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns>current user info</returns>
        public static Models.UserInfo GetUserInfo()
        {
            var akoUserId = ServerVariables.GetServerVariable("HTTP_UID");  //PRODUCTION
            var EDIPI = ServerVariables.GetServerVariable("HTTP_ARMYEDIPI");  // PRODUCTION
            if (akoUserId == null && HttpContext.Current.Request.LogonUserIdentity != null)
                    akoUserId = HttpContext.Current.Request.LogonUserIdentity.Name;
            if (akoUserId != null)
            {
                var index = akoUserId.IndexOf("\\") + 1;
                if (index != -1) //Domain string parse it out
                    akoUserId = akoUserId.Substring(index, akoUserId.Length - index);
            }

            var userInfo = new Models.UserInfo();
            var connection = Credentials.getConnectionString("PACT");
            var db = new SecurityDataContext(connection.ConnectionString);

            if (EDIPI != null)
                userInfo = db.getUserInfo(EDIPI, "EDIPN");
            else
                userInfo = db.getUserInfo(akoUserId, "AKOID");
           
            return userInfo;
        }

    }
}
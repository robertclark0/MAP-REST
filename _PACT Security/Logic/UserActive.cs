using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PACT.DAL;
using MAP_REST.BusinessLogic;

namespace PACT.BLL
{
    public class UserActive
    {
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns>current user info</returns>
        public static List<Models.UserActive> GetUserActive(Models.UserInfo user)
        {

            var userActive = new  List<Models.UserActive>();
            var connection = Credentials.getConnectionString("PACT");
            var db = new SecurityDataContext(connection.ConnectionString);

            if (user.EDIPN != null)
                userActive = db.getUserActive(user.EDIPN, "EDIPN");
            else
                userActive = db.getUserActive(user.akoUserID, "AKOID");

            return userActive;
        }
    }
}
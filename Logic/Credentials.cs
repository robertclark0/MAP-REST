using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.Models;
using MAP_REST.DataAccess;
using PACT.BLL;


namespace MAP_REST.BusinessLogic
{
    public static class Credentials
    {
        public static Connection getConnectionString(string entityCode, string userType = "U")
        {
            string serverName = ServerVariables.GetServerVariable("SERVER_NAME");
            string environmentCode = ServerEnvironment.serverNametoEnvCode(serverName);

            //MAYBE get userType programatically here

            var db = new ConnectionStringDataContext();
            return db.getConnectionString(entityCode, environmentCode, userType);
        }

        public static bool getQueryAuth(dynamic queryObject)
        {
            string entityCode = queryObject.source.product;

            var userInfo = UserInfo.GetUserInfo();

            if (userInfo != null)
            {
                var result = UserActive.GetUserActive(userInfo);

                if (result.Exists(x => x.productName == entityCode))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
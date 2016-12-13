using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.Models;
using MAP_REST.DataAccess;
using PACT.BusinessLogic;


namespace MAP_REST.BusinessLogic
{
    public static class Credentials
    {
        public static Connection getConnectionString(string entityCode, string userType = "U")
        {
            string serverName = ServerVariables.GetServerVariable("SERVER_NAME");
            string environmentCode = ServerEnvironment.serverNametoEnvCode(serverName);

            var db = new ConnectionDataContext();
            return db.getConnectionString(entityCode, environmentCode, userType);
        }

        public static bool isUserAuthorized(string product)
        {
            var userLogic = new PACT.BusinessLogic.User();
            PACT.Models.User user = userLogic.getUserData();

            if (user != null)
            {
                if (user.AuthorizedProducts.Exists(x => x.productName == product))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
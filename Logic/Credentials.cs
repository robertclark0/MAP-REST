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
        public static Connection getConnectionString(string entityCode)
        {
            string serverName = ServerVariables.GetServerVariable("SERVER_NAME");
            string environmentCode = ServerEnvironment.serverNametoEnvCode(serverName);

            var db = new ConnectionStringDataContext();
            return db.getConnectionString(entityCode, environmentCode);
        }
    }
}
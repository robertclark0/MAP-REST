using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAP_REST.BusinessLogic
{
    public static class ServerEnvironment
    {
        public static string serverNametoEnvCode(string serverName)
        {
            switch (serverName)
            {
                case "localhost":
                    return "DEV";

                case "pasbadevweb":
                    return "DEV";

                case "pasbastageweb.amedd.army.mil":
                    return "STAGE";

                case "pasba.army.mil":
                    return "PROD";

                default:
                    return "DEV";
            }
        }
    }
}
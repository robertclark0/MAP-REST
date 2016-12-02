using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAP_REST.DataAccess;
using PACT.BusinessLogic;
using PACT.Models;

namespace MAP_REST.BusinessLogic
{
    public class DataSourceConnection
    {
        public void getDataSourceConnection(string alias)
        {
            bool authorizedUser = false;

            var userData = new PACT.BusinessLogic.User();
            var user = userData.getUserData();

            var db = new ConnectionDataContext();
            var souce = db.getDataSource(alias);

            foreach (AuthorizedProduct auth in user.AuthorizedProducts)
            {
                if (auth.productName == souce.Name)
                {
                    authorizedUser = true;
                }
            }

            if (authorizedUser)
            {
                var connection = Credentials.getConnectionString(souce.Name);
            }
            else
            {
                var connection = Credentials.getConnectionString(souce.Name, "R");
            }

        }
    }
}
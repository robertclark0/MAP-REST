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
        public string authorizedConnectionString(Models.FeatureProfile.Profile featureProfile, Models.DataSource dataSource)
        {
            if (featureProfile.DataQuery == "res" || featureProfile.DataQuery == "par")
            {
                if (Credentials.isUserAuthorized(dataSource.Code))
                {
                    return Credentials.getConnectionString(dataSource.Code).ConnectionString;
                }
                else
                {
                    if (featureProfile.DataQuery == "par")
                    {
                        return Credentials.getConnectionString(dataSource.Code, "R").ConnectionString;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else if (featureProfile.DataQuery == "unr")
            {
                return Credentials.getConnectionString(dataSource.Code).ConnectionString;
            }
            else
            {
                return null;
            }
        }
    }
}
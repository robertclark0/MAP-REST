using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Helpers = System.Web.Helpers;
using MAP_REST.DataAccess;
using System.Text.RegularExpressions;
using System.IO;
using Logger.DataAccess;

namespace MAP_REST.BusinessLogic
{
    public class Download
    {
        public void generateDownloadFile(string GUID, string queryObject, string filePath, string connectionString)
        {

            var db = new DownloadDataContext();
            db.setDownloadStatus(GUID, new Regex("'").Replace(queryObject, "''"), "started");

            try
            {
                dynamic query = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(queryObject);

                var builder = new QueryBuilder.Builder();
                var queryString = builder.BuildQueryString(query, true);
                var CSVQuery = new QueryDataContext(connectionString);

                CSVQuery.QueryCSVWriter(queryString, Path.Combine(filePath, GUID + ".csv"));

                db.updateDownloadStatus(GUID, "complete");
            }
            catch (Exception e)
            {
                db.updateDownloadStatus(GUID, "failed");

                var loggerDB = new LoggerDataContext();
                loggerDB.insertServerLog(GUID, null, "download", e.ToString());
            }
        }
    }
}
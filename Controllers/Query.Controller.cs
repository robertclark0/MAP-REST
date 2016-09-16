using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using Logger.BusinessLogic;
using MAP_REST.QueryBuilder;
using MAP_REST.BusinessLogic;
using System.Text.RegularExpressions;
using Helpers = System.Web.Helpers;
using System.IO;
using Hangfire;
using System.Threading;


namespace MAP_REST.Controllers
{
    public class QueryController : ApiController
    {

        [Route("filter")]
        [HttpPost]
        public HttpResponseMessage FilterValues([FromBody] dynamic postObject)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "filter");
        }

        [Route("query")]
        [HttpPost]
        public HttpResponseMessage Query([FromBody] dynamic postObject)
        {
            var builder = new QueryBuilder.Builder();
            var queryString = builder.BuildQueryString(postObject.query);

            var connection = Credentials.getConnectionString("TELE360", "U");
            var db = new QueryDataContext(connection.ConnectionString);

            var result = db.QueryData(queryString);

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("download")]
        [HttpPost]
        public HttpResponseMessage Download([FromBody] dynamic postObject)
        {
            var guid = Guid.NewGuid();
            string query = Convert.ToString(postObject.query);
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Temp");
            var connection = Credentials.getConnectionString("TELE360", "U");

            BackgroundJob.Enqueue(() => generateDownloadFile(guid.ToString(), query, path, connection.ConnectionString));

            return Request.CreateResponse(HttpStatusCode.OK, new { GUID = guid });
        }
        [Route("download")]
        [HttpGet]
        public HttpResponseMessage Download(string GUID)
        {
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/Downloads/test.xlsx");
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

            var stream = new FileStream(Path.Combine(path, "MAP_D_" + GUID + ".csv"), FileMode.Open);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "RecordExport.csv";

            return result;
        }

        public void generateDownloadFile(string GUID, string queryObject, string filePath, string connectionString)
        {
            
            var db = new DownloadDataContext();
            db.setDownloadStatus(GUID, new Regex("'").Replace(queryObject, "''"), "started");
            
            //try
            //{
                
                //Thread.Sleep(20000);
                dynamic query = Helpers.Json.Decode(queryObject);

                var builder = new QueryBuilder.Builder();
                var queryString = builder.BuildQueryString(query);
                var CSVQuery = new QueryDataContext(connectionString);

                CSVQuery.QueryCSVWriter(queryString, Path.Combine(filePath, "MAP_D_" + GUID + ".csv"));

                db.updateDownloadStatus(GUID, "complete");
            //}
            //catch(Exception e)
            //{
            //    db.updateDownloadStatus(GUID, "failed");
            //    System.Diagnostics.Debug.Write(e);
            //}
        }

        [Route("download-update")]
        [HttpGet]
        public HttpResponseMessage downloadUpdate(string GUID)
        {
            var db = new DownloadDataContext();
            Models.DownloadStatus download = db.getDownloadStatus(GUID);

            return Request.CreateResponse(HttpStatusCode.OK, download);
        }


    }
}
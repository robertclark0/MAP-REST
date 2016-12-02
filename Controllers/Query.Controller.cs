using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Logger.BusinessLogic;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.QueryBuilder;
using MAP_REST.BusinessLogic;
using System.IO;
using Hangfire;
using System.Web.Script.Serialization;
using Helpers = System.Web.Helpers;


namespace MAP_REST.Controllers
{
    public class QueryController : ApiController
    {

        [Route("query")]
        [HttpPost]
        public HttpResponseMessage Query([FromBody] dynamic postObject)
        {
            var builder = new QueryBuilder.Builder();
            string queryString = builder.BuildQueryString(postObject.query);

            //get query connection
            if (Credentials.getQueryAuth(postObject.query))
            {
                var connection = Credentials.getConnectionString("CHUP", "U");
                var db = new QueryDataContext(connection.ConnectionString);

                System.Diagnostics.Debug.Write(queryString);
                var result = db.QueryData(queryString);

                return Request.CreateResponse(HttpStatusCode.OK, new { result });
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Query Not Authorized");
        }

        [Route("download")]
        [HttpPost]
        public HttpResponseMessage Download([FromBody] dynamic postObject)
        {
            var guid = Guid.NewGuid();

            //string query = Convert.ToString(postObject.query);
            string query = Newtonsoft.Json.JsonConvert.SerializeObject(postObject.query);

            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["Download"];
            var connection = Credentials.getConnectionString("CHUP", "U");

            var download = new BusinessLogic.Download();

            BackgroundJob.Enqueue(() => download.generateDownloadFile(guid.ToString(), query, path, connection.ConnectionString));

            return Request.CreateResponse(HttpStatusCode.OK, new { GUID = guid });
        }
        [Route("download")]
        [HttpGet]
        public HttpResponseMessage Download(string GUID)
        {
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["Download"];
            string filePath = Path.Combine(path, GUID + ".csv");

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new CustomStreamContent(filePath);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "RecordExport.csv";

            return result;
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
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
            string queryString = builder.BuildQueryString(postObject.query);

            //get query connection
            var connection = Credentials.getConnectionString("TELE360", "U");
            var db = new QueryDataContext(connection.ConnectionString);

            System.Diagnostics.Debug.Write(queryString);
            var result = db.QueryData(queryString);

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("download")]
        [HttpPost]
        public HttpResponseMessage Download([FromBody] dynamic postObject)
        {
            var guid = Guid.NewGuid();
            string query = Convert.ToString(postObject.query);
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/Temp");
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["Download"];
            var connection = Credentials.getConnectionString("TELE360", "U");

            var download = new BusinessLogic.Download();

            BackgroundJob.Enqueue(() => download.generateDownloadFile(guid.ToString(), query, path, connection.ConnectionString));

            return Request.CreateResponse(HttpStatusCode.OK, new { GUID = guid });
        }
        [Route("download")]
        [HttpGet]
        public HttpResponseMessage Download(string GUID)
        {
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/Temp");
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["DEV_Download"];

            //var stream = new FileStream(Path.Combine(path, "MAP_D_" + GUID + ".csv"), FileMode.Open);
            string filePath = Path.Combine(path, "MAP_D_" + GUID + ".csv");

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            //result.Content = new StreamContent(stream);
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
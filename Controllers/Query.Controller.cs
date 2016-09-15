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
        [HttpGet]
        public HttpResponseMessage Download([FromUri] string GUID = "1")
        {
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/Temp");

            //var db = new DownloadDataContext();
            //Models.QueryJSON dataResult = db.getQueryJSON(GUID);
            //dynamic query = Helpers.Json.Decode(dataResult.JSON);

            //var builder = new QueryBuilder.Builder();
            //var queryString = builder.BuildQueryString(query);

            //var connection = Credentials.getConnectionString("TELE360", "U");
            //var QueryDb = new QueryDataContext(connection.ConnectionString);

            //var result = QueryDb.QueryData(queryString);


            //var response = new HttpResponseMessage();
            //response.Content = new StringContent(csv);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "RecordExport.csv";

            //return response;

            /// +========================== OR
            
            //var connection = Credentials.getConnectionString("TELE360", "U");
            //var QueryDb = new QueryDataContext(connection.ConnectionString);
            //var CSV = QueryDb.CSVData("SELECT TOP 1000 * FROM TeleHealth_360_CAPER WHERE [FY] = '2016' AND [FM] = '1' ORDER BY [Region] asc, [Gender] asc, [PA_Work_RVU] asc");

            string path = System.Web.HttpContext.Current.Server.MapPath("~/Downloads/test.xlsx");

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "RecordExport.csv";
            return result;

        }

        [Route("download")]
        [HttpPost]
        public HttpResponseMessage Download([FromBody] dynamic postObject)
        {

            var db = new DownloadDataContext();
            var guid = Guid.NewGuid();

            db.setQueryJSON(guid.ToString(), new Regex("'").Replace(Convert.ToString(postObject.query),"''"));

            var result = new
            {
                GUID = guid
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("hangfire-test")]
        [HttpGet]
        public HttpResponseMessage hangfire()
        {
            var temp = System.IO.Path.GetTempPath();
            //sleeping();
            //BackgroundJob.Enqueue(() => sleeping());

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //public void sleeping()
        //{
        //    Thread.Sleep(30000);
        //    System.Diagnostics.Debug.Write("TESTING HANGFIRE!");
        //}

        //[Route("hangfire-recover")]
        //[HttpGet]
        //public HttpResponseMessage hangfireUpdate()
        //{

        //}

    }
}
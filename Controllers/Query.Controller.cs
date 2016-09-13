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
        public HttpResponseMessage Download([FromBody] dynamic postObject)
        {
            //var builder = new QueryBuilder.Builder();
            //var queryString = builder.BuildQueryString(postObject.query);

            //var connection = Credentials.getConnectionString("TELE360", "U");
            //var db = new QueryDataContext(connection.ConnectionString);

            //var result = db.QueryData(queryString);

            //string csv = string.Empty;

            //foreach (List<object> row in result)
            //{
            //    foreach (object cell in row)
            //    {
            //        csv += cell.ToString() + ",";
            //    }
            //    csv += "\r\n";
            //}

            var response = new HttpResponseMessage();
            response.Content = new StringContent("1,2,3,4\r\na,b,c,d\r\ne,f,g,h\r\ni,j,k,l");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "RecordExport.csv";

            return response;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.BusinessLogic;
using Logger;

namespace MAP_REST.Controllers
{
    public class SchemaController : ApiController
    {
        [Route("schema")]
        [HttpPost]
        public HttpResponseMessage tableSchema([FromBody] dynamic postObject)
        {
            string alias = postObject.post.alias;
            string type = postObject.post.type;           
            

            var result = new Object();
            var distinct = new Object();
            var db = new SchemaDataContext();

            var source = db.getDataSource(alias);


            switch (type)
            {
                case "table":
                    result = new List<Models.Schema>();
                    result = db.getTableSchema(source.Catalog, source.SourceName);
                    break;

                case "column":
                    string[] columns = postObject.post.columnName.ToObject<string[]>();
                    string order = postObject.post.order;

                    result = new List<object>();
                    result = db.getColumnDistinct(source.Catalog, source.SourceName, columns, order);
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("source-test")]
        [HttpGet]
        public HttpResponseMessage test()
        {


            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}

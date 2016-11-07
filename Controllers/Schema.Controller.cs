using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.BusinessLogic;

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
            string column = postObject.post.columnName;

            var connection = Credentials.getConnectionString("MASTER", "U");
            var db = new SchemaDataContext(connection.ConnectionString);

            var source = db.getDataSource(alias);
            var result = new Object();

            switch (type)
            {
                case "table":
                    result = new List<Models.Schema>();
                    result = db.getTableSchema(source.Catalog, source.SourceName);
                    break;

                case "column":
                    result = new List<object>();
                    result = db.getColumnDistinct(source.Catalog, source.SourceName, column);
                    break;
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

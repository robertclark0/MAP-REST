using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using Logger.BusinessLogic;


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
            return Request.CreateResponse(HttpStatusCode.OK, "query");
        }

    }
}
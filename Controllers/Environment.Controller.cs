using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PACT.BusinessLogic;

namespace MAP_REST.Controllers
{
    public class EnvironmentController : ApiController
    {
        [Route("environment")]
        public HttpResponseMessage GetEnvironment()
        {
            var result = ServerVariables.GetServerVariable("SERVER_NAME");
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }
    }
}

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
    public class ReportController : ApiController
    {

        [Route("report")]
        [HttpPost]
        public HttpResponseMessage Report([FromBody] dynamic postObject)
        {

            switch ((string)postObject.type)
            {
                case "reportList":
                    break;
                case "getReport":
                    break;
                case "updateReport":
                    break;
                case "deleteReport":
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, "reports");
        }

    }
}

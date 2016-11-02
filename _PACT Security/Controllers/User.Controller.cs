using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PACT.BusinessLogic;
using PACT.Models;
using PACT.DataAccess;
using MAP_REST.BusinessLogic;
using Logger.BusinessLogic;


namespace PACT.Controllers
{
    public class UserController : ApiController
    {

        [Route("user")]
        [HttpGet]
        public HttpResponseMessage user()
        {
            var user = new BusinessLogic.User();
            var result = user.getUserData();

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }
    }
}
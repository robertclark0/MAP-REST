using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PACT.BLL;
using PACT.Models;
using Logger.BusinessLogic;


namespace PACT.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// Gets All PACT User Security Credentials and Role Authorizations/Filters
        /// </summary>
        /// <returns></returns>
        [Route("user-info")]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage GetUserInfo([FromBody] dynamic postObject)
        {
            var result = BLL.UserInfo.GetUserInfo();

            Log.ServerLog(Log.GenerateServerSessionID(), "user-info", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("user-product")]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage GetUserProductInfo([FromBody] dynamic postObject)
        {
            var result = BLL.UserProduct.GetUserProduct("CHUP");

            Log.ServerLog(Log.GenerateServerSessionID(), "user-product", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("user-active")]
        [HttpPost]
        [HttpGet]
        public HttpResponseMessage getUserActiveInfo([FromBody] dynamic postObject)
        {
            var userInfo = BLL.UserInfo.GetUserInfo();

            if (userInfo != null)
            {
                var result = BLL.UserActive.GetUserActive(userInfo);

                Log.ServerLog(Log.GenerateServerSessionID(), "user-active", Log.SerializeObject(result), postObject);
                return Request.CreateResponse(HttpStatusCode.OK, new { result });
            }
            else
            {
                Log.ServerLog(Log.GenerateServerSessionID(), "user-active", "User not authorized in PACT", postObject);
                return Request.CreateResponse(HttpStatusCode.OK, "User not Active");
            }



        }
    }
}
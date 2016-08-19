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
    public class ProductLineController : ApiController
    {

        [Route("product-lines")]
        [HttpPost]
        public HttpResponseMessage Get([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.ProductLine>() ;
            result = db.getProductLine();

            Log.ServerLog(Log.GenerateServerSessionID(), "product-lines", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }
    }
}

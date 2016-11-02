using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Logger.BusinessLogic;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.BusinessLogic;

namespace MAP_REST.Controllers
{
    public class ProductController : ApiController
    {

        [Route("products")]
        [HttpGet]
        public HttpResponseMessage Products()
        {
            var products = new BusinessLogic.Product();
            var result = products.getProducts();

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

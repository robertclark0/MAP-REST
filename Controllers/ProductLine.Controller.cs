using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using Logger.BusinessLogic;
using MAP_REST.BusinessLogic;

namespace MAP_REST.Controllers
{
    public class ProductLineController : ApiController
    {

        [Route("product-lines")]
        [HttpPost]
        public HttpResponseMessage ProductLines([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.ProductLine>();
            result = db.getProductLine();

            foreach (ProductLine product in result)
            {
                product.Modules = db.getModules(product.Code);
            }

            Log.ServerLog(Log.GenerateServerSessionID(), "product-lines", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("data-sources")]
        [HttpPost]
        public HttpResponseMessage DataSources([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.DataSource>();
            result = db.getDataSource((string)postObject.post.entityCode);

            Log.ServerLog(Log.GenerateServerSessionID(), "data-sources", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("data-source-parameters")]
        [HttpPost]
        public HttpResponseMessage DataSourceParameters([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.DataSourceParameters>();
            result = db.getDataSourceParameters((int)postObject.dataSourceID);

            //Log.ServerLog(Log.GenerateServerSessionID(), "data-source-parameters", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("security-profile")]
        [HttpGet]
        public HttpResponseMessage profile()
        {
            var securityProfile = new SecurityProfile();
            var result = new Models.SecurityProfile.Profile();

            result = securityProfile.getSecurityProfile("CHUP");
            
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

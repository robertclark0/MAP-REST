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
        public HttpResponseMessage ProductLines([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.ProductLine>() ;
            result = db.getProductLine();

            Log.ServerLog(Log.GenerateServerSessionID(), "product-lines", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("data-sources")]
        [HttpPost]
        public HttpResponseMessage DataSources([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.DataSource>();
            result = db.getDataSource(postObject.entityCode);

            Log.ServerLog(Log.GenerateServerSessionID(), "data-sources", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("data-source-parameters")]
        [HttpPost]
        public HttpResponseMessage DataSourceParameters([FromBody] dynamic postObject)
        {
            var db = new ProductLineDataContext();
            var result = new List<Models.DataSourceParameters>();
            result = db.getDataSourceParameters(postObject.paramID);

            Log.ServerLog(Log.GenerateServerSessionID(), "data-source-parameters", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }
    }
}

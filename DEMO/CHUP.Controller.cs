using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.BusinessLogic;
using Logger.BusinessLogic;


namespace MAP_REST.DEMO
{
    public class CHUPController : ApiController
    {

        [Route("chup")]
        [HttpPost]
        public HttpResponseMessage Report([FromBody] dynamic postObject)
        {
            var connection = Credentials.getConnectionString("CHUP");
            var db = new CHUPDataContext(connection.ConnectionString);

            var result = new List<DEMO.CHUP>();
            result = db.getCHUP(postObject.chup.region, 
                postObject.chup.DMISID,
                postObject.chup.MEPRSCode,
                postObject.chup.PCMNPI,
                postObject.chup.ChupFlag,
                postObject.chup.HUFlag,
                postObject.chup.PainFlag,
                postObject.chup.PolyFlag,
                postObject.chup.FY,
                postObject.chup.FM,
                postObject.chup.RowStart,
                postObject.chup.RowEnd);

            //Log.ServerLog(Log.GenerateServerSessionID(), "report", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

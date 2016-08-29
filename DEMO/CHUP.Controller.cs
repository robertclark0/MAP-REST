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
            result = db.getCHUP(
                (string)postObject.region,
                (string)postObject.DMISID,
                (string)postObject.MEPRSCode,
                (string)postObject.PCMNPI,
                (int)postObject.ChupFlag,
                (int)postObject.HUFlag,
                (int)postObject.PainFlag,
                (int)postObject.PolyFlag,
                (int)postObject.FY,
                (int)postObject.FM,
                (int)postObject.RowStart,
                (int)postObject.RowEnd);

            //Log.ServerLog(Log.GenerateServerSessionID(), "report", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

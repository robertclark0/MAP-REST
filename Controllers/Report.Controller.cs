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
        public HttpResponseMessage Reports([FromBody] dynamic postObject)
        {
            var result = new Object();
            var db = new ReportDataContext();
            var sessionID = Log.GenerateServerSessionID();

            if (postObject != null)
            {
                switch ((string)postObject.post.type)
                {
                    case "get":                        
                        result = new List<Models.Report>();
                        result = db.getReport((string)postObject.post.report.GUID);

                        Log.ServerLog(sessionID, "report:get", Log.SerializeObject(result), postObject);
                        
                        break;

                    case "list":
                        result = new List<Models.ReportList>();
                        result = db.getReportList((string)postObject.post.entityCode);

                        Log.ServerLog(sessionID, "report:list", Log.SerializeObject(result), postObject);

                        break;

                    case "create":
                        db.createReport((string)postObject.post.entityCode, (string)postObject.post.report.GUID, (string)postObject.post.report.user, (string)postObject.post.report.name, (string)postObject.post.report.type, (string)postObject.post.report.json, (string)postObject.post.report.category, (string)postObject.post.report.position);
                        result = "created";

                        Log.ServerLog(sessionID, "report:create", (string)postObject.post.report.json, postObject);

                        break;

                    case "update":
                        db.updateReport((string)postObject.post.report.GUID, (string)postObject.post.report.user, (string)postObject.post.report.name, (string)postObject.post.report.type, (string)postObject.post.report.json, (string)postObject.post.report.category, (string)postObject.post.report.position);
                        result = "updated";

                        Log.ServerLog(sessionID, "report:update", (string)postObject.post.report.json, postObject);

                        break;

                    case "delete":
                        db.deleteReport((string)postObject.post.report.GUID);
                        result = "deleted";

                        Log.ServerLog(sessionID, "report:delete", (string)postObject.post.report.GUID, postObject);

                        break;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

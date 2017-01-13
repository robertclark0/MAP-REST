using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using Logger.BusinessLogic;
using PACT.BusinessLogic;

namespace MAP_REST.Controllers
{
    public class CanvasController : ApiController
    {
        [Route("canvas")]
        [HttpPost]
        public HttpResponseMessage Reports([FromBody] dynamic postObject)
        {
            var result = new Object();

            if (postObject != null)
            {
               
                var db = new CanvasDataContext();
                var sessionID = Log.GenerateServerSessionID();

                var userData = new PACT.BusinessLogic.User();
                var user = userData.getUserData();

                switch ((string)postObject.post.type)
                {
                    case "get":
                        result = new List<Models.Report>();
                        result = db.getCanvas((string)postObject.post.canvas.GUID);

                        Log.ServerLog(sessionID, "canvas:get", Log.SerializeObject(result), postObject);

                        break;

                    case "list":
                        result = new List<Models.ReportList>();
                        result = db.getCanvasList(user.akoUserID);

                        Log.ServerLog(sessionID, "canvas:list", Log.SerializeObject(result), postObject);

                        break;

                    case "create":
                        db.createCanvas((string)postObject.post.entityCode, (string)postObject.post.canvas.GUID, user.akoUserID, (string)postObject.post.canvas.name, (string)postObject.post.canvas.json);
                        result = "created";

                        Log.ServerLog(sessionID, "canvas:create", (string)postObject.post.canvas.json, postObject);

                        break;

                    case "update":
                        db.updateCanvas((string)postObject.post.canvas.GUID, user.akoUserID, (string)postObject.post.canvas.name, (string)postObject.post.canvas.json);
                        result = "updated";

                        Log.ServerLog(sessionID, "canvas:update", (string)postObject.post.canvas.json, postObject);

                        break;

                    case "delete":
                        db.deleteCanvas((string)postObject.post.canvas.GUID);
                        result = "deleted";

                        Log.ServerLog(sessionID, "canvas:delete", (string)postObject.post.canvas.GUID, postObject);

                        break;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }
    }
}

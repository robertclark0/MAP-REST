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
        public HttpResponseMessage Report([FromBody] dynamic postObject)
        {
            var db = new ReportDataContext();
            var result = new List<Models.Report>();
            result = db.getReport(postObject.report.GUID);

            Log.ServerLog(Log.GenerateServerSessionID(), "report", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("report/update")]
        [HttpPost]
        public HttpResponseMessage Update([FromBody] dynamic postObject)
        {
            var db = new ReportDataContext();
            db.updateReport(postObject.report.GUID, postObject.report.user, postObject.report.name, postObject.report.type, postObject.report.json);
            var result = "updated";

            Log.ServerLog(Log.GenerateServerSessionID(), "report/update", Log.SerializeObject(postObject.report.GUID), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("report/create")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] dynamic postObject)
        {
            var db = new ReportDataContext();
            db.createReport(postObject.entityCode, postObject.report.GUID, postObject.report.user, postObject.report.name, postObject.report.type, postObject.report.json);
            var result = "created";

            Log.ServerLog(Log.GenerateServerSessionID(), "report/create", Log.SerializeObject(postObject.report.GUID), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("report/delete")]
        [HttpPost]
        public HttpResponseMessage Delete([FromBody] dynamic postObject)
        {
            var db = new ReportDataContext();
            db.deleteReport(postObject.report.GUID);
            var result = "deleted";

            Log.ServerLog(Log.GenerateServerSessionID(), "report/delete", Log.SerializeObject(postObject.report.GUID), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        [Route("report/list")]
        [HttpPost]
        public HttpResponseMessage List([FromBody] dynamic postObject)
        {
            var db = new ReportDataContext();
            var result = new List<Models.ReportList>();
            result = db.getReportList(postObject.entityCode);

            Log.ServerLog(Log.GenerateServerSessionID(), "report/list", Log.SerializeObject(result), postObject);
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

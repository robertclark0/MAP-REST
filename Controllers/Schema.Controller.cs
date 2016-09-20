﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MAP_REST.DataAccess;
using MAP_REST.Models;
using MAP_REST.BusinessLogic;

namespace MAP_REST.Controllers
{
    public class SchemaController : ApiController
    {
        [Route("schema/table")]
        [HttpPost]
        public HttpResponseMessage Query([FromBody] dynamic postObject)
        {
            string entityCode = postObject.post.entityCode;
            string tableName = postObject.post.tableName;

            var result = new List<Models.Schema>();

            var connection = Credentials.getConnectionString("MASTER", "U");
            var db = new SchemaDataContext(connection.ConnectionString);
            result = db.getTableSchema(entityCode, tableName);


            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

    }
}

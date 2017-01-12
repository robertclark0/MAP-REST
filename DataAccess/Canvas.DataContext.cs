using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models;

namespace MAP_REST.DataAccess
{
    public class CanvasDataContext : DbContext
    {
        public CanvasDataContext(string nameOrConnectionString = "name=MAP")
            : base(nameOrConnectionString)
        { }


        public List<Models.CanvasList> getCanvasList(string EntityCode)
        {
            return this.Database.SqlQuery<Models.CanvasList>("UserCreated.usp_GetCanvasList @p0", EntityCode).ToList();
        }

        public List<Models.Canvas> getCanvas(string GUID)
        {
            return this.Database.SqlQuery<Models.Canvas>("UserCreated.usp_GetCanvas @p0", GUID).ToList();
        }

        public void createCanvas(string EntityCode, string GUID, string User, string Canvas_Name, string JSON)
        {
            this.Database.ExecuteSqlCommand("UserCreated.usp_CreateCanvas @p0, @p1, @p2, @p3, @p4", EntityCode, GUID, User, Canvas_Name, JSON);
        }

        public void updateCanvas(string GUID, string User, string Canvas_Name, string JSON)
        {
            this.Database.ExecuteSqlCommand("UserCreated.usp_UpdateCanvas @p0, @p1, @p2, @p3", GUID, User, Canvas_Name);
        }

        public void deleteCanvas(string GUID)
        {
            this.Database.ExecuteSqlCommand("UserCreated.usp_DeleteCanvas @p0", GUID);
        }
    }
}
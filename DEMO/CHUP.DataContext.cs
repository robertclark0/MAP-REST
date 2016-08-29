using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using MAP_REST.Models;

namespace MAP_REST.DEMO
{
    public class CHUPDataContext : DbContext
    {
        public CHUPDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }


        public List<DEMO.CHUP> getCHUP(string region, string DMISID, string MEPRSCode, string PCMNPI, int ChupFlag, int HUFlag, int PainFlag, int PolyFlag, int FY, int FM, int RowStart, int RowEnd)
        {
            return this.Database.SqlQuery<DEMO.CHUP>("sp_Rpt_DrillDown @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11", region, DMISID, MEPRSCode, PCMNPI, ChupFlag, HUFlag, PainFlag, PolyFlag, FY, FM, RowStart, RowEnd).ToList();
        }

    }
}
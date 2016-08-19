using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using PACT.Models;


namespace PACT.DAL
{
    public class SecurityDataContext : DbContext
    {
        public SecurityDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="ID">either AKO ID or EDIPI</param>
        /// <param name="IDType">string ("AKOID" or "EDIPN")</param>
        /// <returns>Returns user parameters</returns>
        public UserInfo getUserInfo(string ID, string IDType)
        {
            return this.Database.SqlQuery<UserInfo>("usp_GetUserInfo @p0, @p1", ID, IDType).FirstOrDefault();
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="ID">either AKO ID or EDIPI</param>
        /// <param name="IDType">string ("AKOID" or "EDIPN")</param>
        /// <returns>Returns user parameters</returns>
        public List<UserActive> getUserActive(string ID, string IDType)
        {
            return this.Database.SqlQuery<UserActive>("usp_GetUserActiveProducts @p0, @p1", ID, IDType).ToList();
        }

    }
}
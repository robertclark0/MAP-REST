using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using PACT.Models;


namespace PACT.DataAccess
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


        public User getUser(string ID, string IDType)
        {
            return this.Database.SqlQuery<User>("usp_GetUserInfo @p0, @p1", ID, IDType).FirstOrDefault();
        }

        public List<AuthorizedProduct> getProducts(string ID, string IDType)
        {
            return this.Database.SqlQuery<AuthorizedProduct>("usp_GetUserActiveProducts @p0, @p1", ID, IDType).ToList();
        }

        public List<Authorization> GetUserRoles(string akoUserId, string productName)
        {
            return this.Database.SqlQuery<Authorization>("usp_GetRolesUserProduct @p0, @p1", akoUserId, productName).ToList();
        }
        public List<Authorization> GetUserRoles(string akoUserId, string productName, string ediPn)
        {
            return this.Database.SqlQuery<Authorization>("usp_GetRolesUserProduct @p0, @p1, @p2", akoUserId, productName, ediPn).ToList();
        }

    }
}
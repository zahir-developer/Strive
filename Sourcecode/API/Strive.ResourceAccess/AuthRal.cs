using Dapper;
using Strive.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.Repository;
using System.Linq;
using System.Data;
using Strive.Common;

namespace Strive.ResourceAccess
{
    public class AuthRal
    {
        public Db db;
        IDbConnection _dbconnection;

        public AuthRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }

        public AuthRal()
        {
            db = new Db();
        }
        public TenantSchema Login(Authentication authentication)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@LoginId", authentication.Email);
                dynParams.Add("@Password", authentication.PasswordHash);
                var res = db.Fetch<TenantSchema>(SPEnum.USPLogin.ToString(), dynParams);
                if (res.Count() == 0) throw new Exception("data returned null value");
                return res?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

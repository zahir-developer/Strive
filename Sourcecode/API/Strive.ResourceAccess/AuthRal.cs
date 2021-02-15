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

        public TenantSchema Login(Authentication authentication)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@LoginId", authentication.Email);
                dynParams.Add("@Password", authentication.PasswordHash);
                var res = db.Fetch<TenantSchema>(EnumSP.Authentication.USPLOGIN.ToString(), dynParams);
                if (res.Count() == 0) throw new Exception("data returned null value");
                return res?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TenantSchema GetSchema(Guid userGuid)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@UserGuid", userGuid);
                var res = db.Fetch<TenantSchema>(EnumSP.Authentication.USPGETSCHEMABYGUID.ToString(), dynParams);
                if (res.Count() == 0) throw new Exception("data returned null value");
                return res?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetPassword(string email)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@UserName", email);
                var res = db.Get<string>(EnumSP.Authentication.USPGETPASSWORDHASH.ToString(), dynParams);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

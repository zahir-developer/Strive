using Dapper;
using Strive.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.Repository;
using System.Linq;

namespace Strive.ResourceAccess
{
    public class AuthRal
    {
        public Db db;

        public AuthRal()
        {
            db = new Db();
        }
        public User Login(Authentication authentication)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@LoginId", authentication.Email);
                dynParams.Add("@Password", authentication.PasswordHash);
                var res = db.Fetch<User>(SPEnum.USPGETUSERBYLOGIN.ToString(), dynParams);
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

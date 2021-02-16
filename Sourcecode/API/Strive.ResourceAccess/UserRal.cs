using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Strive.ResourceAccess
{
    public class UserRal
    {
        public Db db;

        public UserRal()
        {
            //db = new Db();
        }

        public List<User> GetUsers()
        {
            try
            {
                var dynParams = new DynamicParameters();
                var res = db.Fetch<User>(EnumSP.Authentication.USPGETALLUSERS.ToString(), dynParams);
                if (res.Count() == 0) throw new Exception("data returned null value");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUser(User user)
        {
            try
            {
                List<User> lstUser = new List<User>();
                lstUser.Add(user);
                var dynParams = new DynamicParameters();
                dynParams.Add("@UserTVP", lstUser.ToDataTable().AsTableValuedParameter("UserTVP"));
                CommandDefinition cmd = new CommandDefinition(EnumSP.Authentication.USPSAVEUSER.ToString(), dynParams, commandType: CommandType.StoredProcedure);
                db.Save(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.Auth;

namespace Strive.ResourceAccess
{
    public class CommonRal : RalBase
    {
        public CommonRal(ITenantHelper tenant, bool isAuth = false) : base(tenant, isAuth)
        {
        }

        public List<Code> GetAllCodes()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(SPEnum.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        public List<Code> GetCodeByCategory(GlobalCodes codeCategory)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("Category", codeCategory.ToString());
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(SPEnum.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        public List<Code> GetCodeByCategoryId(int CategoryId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("CategoryId", CategoryId);
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(SPEnum.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        //public void SaveUserLogin(UserLogin userLogin)
        //{
        //    DynamicParameters dynParams = new DynamicParameters();
        //    dynParams.Add("@Logintbl", lstEmployeeLogin.ToDataTable().AsTableValuedParameter("tvpAuthMaster"));
        //    CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOGIN.ToString(), dynParams, commandType: CommandType.StoredProcedure);
        //    db.Save(cmd);
        //}

        public int CreateLogin(UserLogin userLogin)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Logintbl", userLogin.TableName("tvpAuthMaster"));
            dynParams.Add("@TenantGuid", _tenant.TenantGuid);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOGIN.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            return db.SaveGetId(cmd).toInt();
        }

        public void SaveOTP(string userId, string otp)
        {            
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Email", userId);
            dynParams.Add("@OTP", otp);
            dynParams.Add("@DateEntered", DateTime.Now);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEOTP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
        }

        public bool ResetPassword(string userId, string otp, string newPass)
        {
            try
            {
                DynamicParameters dynParams = new DynamicParameters();
                dynParams.Add("@Email", userId);
                dynParams.Add("@OTP", otp);
                dynParams.Add("@NewPassword", newPass);
                CommandDefinition cmd = new CommandDefinition(SPEnum.USPRESETPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
                db.Save(cmd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

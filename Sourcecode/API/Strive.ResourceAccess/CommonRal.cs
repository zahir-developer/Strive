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
using Strive.BusinessEntities.Model;

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
            dynParams.Add("CategoryId", null);
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(SPEnum.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        public object DoSearch(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public List<Code> GetCodeByCategoryId(int CategoryId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("Category", null);
            dynParams.Add("CategoryId", CategoryId);
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(SPEnum.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        public int CreateLogin(AuthMaster authMaster)
        {
            int authId = dbRepo.Add<AuthMaster>(authMaster);

            SaveTenantUserMap(authId, _tenant.TenantGuid);

            return authId;
        }

        public void SaveTenantUserMap(int authId, string tenentGuid)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@AuthId", authId);
            dynParams.Add("@TenantGuid", tenentGuid);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVETENANTUSERMAP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
        }

        public bool GetEmailIdExist(string email)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Email", email);
            return db.FetchSingle<bool>(SPEnum.USPEMAILEXIST.ToString(), dynParams);
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

        public int VerifyOTP(string emailId, string otp)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Email", emailId);
            dynParams.Add("@OTP", otp);
            return db.QuerySingleOrDefault<int>(SPEnum.USPVERIFYOTP.ToString(), dynParams);
        }
    }
}

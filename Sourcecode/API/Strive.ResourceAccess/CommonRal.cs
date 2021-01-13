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
using Strive.BusinessEntities.DTO.Employee;

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

        public List<Email> GetAllEmail()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Email> lstEmail = new List<Email>();
            lstEmail = db.Fetch<Email>(SPEnum.USPGETALLEMAIL.ToString(), dynParams);
            return lstEmail;
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

        public void UpdateClientAuth(Client client)
        {
            dbRepo.Update<Client>(client);
        }

        public bool GetEmailIdExist(string email)
        {
            _prm.Add("@Email", email);
            var result = db.FetchSingle<bool>(SPEnum.USPEMAILEXIST.ToString(), _prm);
            return result;
        }
        public List<Code> GetCityByStateId(int stateId)
        {
            _prm.Add("stateId", stateId);
            return db.Fetch<Code>(SPEnum.USPGETCITYBYSTATE.ToString(), _prm);
        }
    }
}

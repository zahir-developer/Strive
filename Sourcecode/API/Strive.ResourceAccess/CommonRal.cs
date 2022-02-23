using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Strive.BusinessEntities.Code;

using Strive.BusinessEntities.City;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;

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
            lstCode = db.Fetch<Code>(EnumSP.Employee.USPGETCODES.ToString(), dynParams);
            return lstCode;
        }

        public List<Code> GetCodeByCategory(GlobalCodes codeCategory)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("Category", codeCategory.ToString());
            dynParams.Add("CategoryId", null);
            List<Code> lstCode = new List<Code>();
            lstCode = db.Fetch<Code>(EnumSP.Employee.USPGETCODES.ToString(), dynParams);
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
            lstCode = db.Fetch<Code>(EnumSP.Employee.USPGETCODES.ToString(), dynParams);
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
            CommandDefinition cmd = new CommandDefinition(EnumSP.Authentication.USPSAVETENANTUSERMAP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
        }

        public List<Email> GetAllEmail()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Email> lstEmail = new List<Email>();
            lstEmail = db.Fetch<Email>(EnumSP.Authentication.USPGETALLEMAIL.ToString(), dynParams);
            return lstEmail;
        }
        public void SaveOTP(string userId, string otp)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Email", userId);
            dynParams.Add("@OTP", otp);
            dynParams.Add("@DateEntered", DateTime.Now);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Authentication.USPSAVEOTP.ToString(), dynParams, commandType: CommandType.StoredProcedure);
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
                CommandDefinition cmd = new CommandDefinition(EnumSP.Authentication.USPRESETPASSWORD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
                db.Save(cmd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(int authId)
        {
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("@AuthId", authId);

            CommandDefinition cmd = new CommandDefinition(EnumSP.Authentication.USPDELETEUSER.ToString(), dynamic, commandType: CommandType.StoredProcedure);

            db.Save(cmd);

        }

        public int VerifyOTP(string emailId, string otp)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Email", emailId);
            dynParams.Add("@OTP", otp);
            return db.QuerySingleOrDefault<int>(EnumSP.Authentication.USPVERIFYOTP.ToString(), dynParams);
        }

        public void UpdateClientAuth(Client client)
        {
            dbRepo.Update<Client>(client);
        }

        public bool GetEmailIdExist(string email)
        {
            _prm.Add("@Email", email);
            var result = db.FetchSingle<bool>(EnumSP.Authentication.USPEMAILEXIST.ToString(), _prm);
            return result;
        }
        public List<CityDto> GetCityByStateId(int stateId)
        {
            _prm.Add("stateId", stateId);
            return db.Fetch<CityDto>(EnumSP.Employee.USPGETCITYBYSTATE.ToString(), _prm);
        }

        public TicketDto GetTicketNumber(int locationId)
        {
            _prm.Add("@locationId", locationId);
            return db.FetchSingle<TicketDto>(SPEnum.USPGETJOBTICKETNUMBER.ToString(), _prm);

        }

        public List<EmailListViewModel> GetEmailIdByRole(string locationId, DateTime? startDate = null, DateTime? endDate = null)
        {
            _prm.Add("@locationId", locationId);
            _prm.Add("@startDate", startDate == null ? DateTime.Now.ToString("yyy-MM-dd") : startDate.ToString());
            _prm.Add("@endDate", endDate == null ? DateTime.Now.ToString("yyy-MM-dd") : endDate.ToString());

            return db.Fetch<EmailListViewModel>(EnumSP.Sales.USPGETEMAILID.ToString(), _prm);

        }


        public List<ModelDto> GetModelByMakeId(int makeId)
        {
            _prm.Add("makeId", makeId);
            return db.Fetch<ModelDto>(EnumSP.Vehicle.USPGETMODELBYMAKE.ToString(), _prm);
        }

        public List<MakeDto> GetAllMake()
        {
            return db.Fetch<MakeDto>(EnumSP.Vehicle.USPGETALLMAKE.ToString(), _prm);
        }

        public List<UpchargeViewModel> GetUpchargeByType(UpchargeDto upchargeDto)
        {
            _prm.Add("ModelId", upchargeDto.ModelId);
            _prm.Add("ServiceType", upchargeDto.UpchargeServiceType);
            _prm.Add("LocationId", upchargeDto.LocationId);
            return db.Fetch<UpchargeViewModel>(EnumSP.Vehicle.USPGETUPCHARGEBYTYPE.ToString(), _prm);
        }

        public bool InsertPaymentGateway(PaymentGatewayDTO oPayment)
        {
            return dbRepo.UpdatePc(oPayment);
        }

        public MaxLocationViewModel GetLoationMaxLimit(int tenantId)
        {
            _prm.Add("tenantId", tenantId);
            return db.FetchSingle<MaxLocationViewModel>(EnumSP.Tenant.USPGETLOCATIONLIMIT.ToString(), _prm);
        }

        public bool DeleteJobItem(string jobItemId)
        {
            _prm.Add("@jobItemId", jobItemId);
            db.Save(EnumSP.Job.USPDELETEJOBITEMBYID.ToString(), _prm);
            return true;
        }
        public UserDetailsViewModel GetUserPassword(string email, UserType userType)
        {
            _prm.Add("@email", email);
            _prm.Add("@userType", userType);
            return db.FetchSingle<UserDetailsViewModel>(EnumSP.Job.USPGETCLIENTMAIL.ToString(), _prm);
        }
        public List<PaymentGatewayViewModel> GetAllPaymentGateway()
        {
            return db.Fetch<PaymentGatewayViewModel>(SPEnum.USPGETPAYMENTGATEWAYDETAILS.ToString(), _prm);
        }
        public bool UpdateLoginId(BusinessEntities.Auth.LoginDetailDTO loginInfo)
        {
            //db = new TenantHelper().dbAuth;
            var _prm = new DynamicParameters();
            _prm.Add("AuthId", loginInfo.AuthId);
            _prm.Add("LoginId", loginInfo.LoginId);
            db.Save(EnumSP.Authentication.USPUPDATELOGINID.ToString(), _prm);

            return true;
        }
    }
}

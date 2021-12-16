using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Code;
using Strive.RepositoryCqrs;
using Strive.BusinessEntities.DTO.Sales;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.DTO.Client;
using Strive.BusinessEntities.DTO.Report;

namespace Strive.ResourceAccess
{
    public class ClientRal : RalBase
    {
        public ClientRal(ITenantHelper tenant) : base(tenant) { }

        public int InsertClientDetails(ClientDto client)
        {
            return dbRepo.InsertPK(client, "ClientId");
        }
        public bool UpdateClientVehicle(ClientDto client)
        {
            return dbRepo.UpdatePc(client);
        }
        public int InsertCreditDetails(CreditDTO credit)
        {
            if (credit.CreditAccount.CreditAccountId == 0)
                return dbRepo.InsertPK(credit, "CreditAccountId");
            else
                return (dbRepo.UpdatePc(credit, "CreditAccountId") ? 1 : 0);
        }

        public bool AddCreditAccountHistory(CreditHistoryDTO creditAccountHistoryDto)
        {
            return dbRepo.SavePc(creditAccountHistoryDto, "CreditAccountHistoryId");
        }
        //public bool UpdateAccountHistory(CreditHistoryDTO creditAccountHistoryDto)
        //{
        //    return dbRepo.SavePc(creditAccountHistoryDto, "CreditAccountHistoryId");
        //}
        //public bool UpdateAccountBalance(ClientAmountUpdateDto clientAmountUpdate)
        //{
        //    _prm.Add("@ClientId", clientAmountUpdate.ClientId);
        //    _prm.Add("@Amount", clientAmountUpdate.Amount);
        //    db.Save(SPEnum.USPUPDATEACCOUNTDETAILS.ToString(), _prm);
        //    return true;
        //}
        public ClientListViewModel GetAllClient(SearchDto searchDto)
        {

            _prm.Add("@locationId", searchDto.LocationId);
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            var result = db.FetchMultiResult<ClientListViewModel>(SPEnum.USPGETALLCLIENT.ToString(), _prm);
            return result;

        }

        public ClientLoginViewModel GetClientByAuthId(int authId)
        {
            _prm.Add("@AuthId", authId);
            return db.FetchMultiResult<ClientLoginViewModel>(EnumSP.Authentication.USPGETCLIENTUSERBYAUTHID.ToString(), _prm);
        }

        public List<ClientDetailViewModel> GetClientById(int? clientId)
        {
            _prm.Add("@ClientId", clientId);
            return db.Fetch<ClientDetailViewModel>(SPEnum.USPGETCLIENT.ToString(), _prm);

        }
        public ClientVehicleDetailModelView GetClientVehicleById(int clientId)
        {
            _prm.Add("@ClientId", clientId);
            return db.FetchMultiResult<ClientVehicleDetailModelView>(SPEnum.uspGetClientAndVehicle.ToString(), _prm);
        }

        public BusinessEntities.Model.Client GetById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@ClientId", id);
            var lstClientInfo = db.FetchSingle<BusinessEntities.Model.Client>(SPEnum.USPGETCLIENTBYID.ToString(), dynParams);
            return lstClientInfo;
        }

        public BusinessEntities.Model.Client GetClientByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteClient(int clientId)
        {
            _prm.Add("ClientId", clientId);
            db.Save(SPEnum.USPDELETECLIENT.ToString(), _prm);
            return true;
        }
        public List<ClientSearchViewModel> GetClientSearch(ClientSearchDto search)
        {
            _prm.Add("@ClientName", search.ClientName);
            return db.Fetch<ClientSearchViewModel>(SPEnum.uspGetClientName.ToString(), _prm);
        }
        public List<Code> GetClientCode()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.SCORE);
        }
        public List<ClientStatementViewModel> GetStatementByClientId(int id)
        {
            _prm.Add("ClientId", id);
            return db.Fetch<ClientStatementViewModel>(SPEnum.USPGETVEHICLESTATEMENTBYCLIENTID.ToString(), _prm);
        }
        public List<ClientHistoryViewModel> GetHistoryByClientId(int id)
        {
            _prm.Add("ClientId", id);
            return db.Fetch<ClientHistoryViewModel>(SPEnum.USPGETVEHICLEHISTORYBYCLIENTID.ToString(), _prm);
        }


        public bool IsClientName(ClientNameDto clientNameDto)
        {
            _prm.Add("FirstName", clientNameDto.FirstName);
            _prm.Add("LastName", clientNameDto.LastName);
            _prm.Add("PhoneNumber", clientNameDto.PhoneNumber);

            var result = db.Fetch<ClientViewModel>(SPEnum.USPISCLIENTAVAILABLE.ToString(), _prm);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<ClientNameViewModel> GetAllClientName(string name)
        {
            _prm.Add("Name", name);
            return db.Fetch<ClientNameViewModel>(SPEnum.USPGETALLCLIENTNAME.ToString(), _prm);
        }
        public bool ClientEmailExist(string email)
        {

            _prm.Add("Email", email);

            var result = db.Fetch<ClientEmailValidationViewModel>(EnumSP.Client.USPCLIENTEMAILEXIST.ToString(), _prm);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public List<ClientEmailBlastViewModel> GetClientList(EmailBlastDto emailBlast)
        {
            _prm.Add("fromDate", emailBlast.fromDate);
            _prm.Add("toDate", emailBlast.toDate);
            _prm.Add("IsMemebership", emailBlast.IsMembership);

            return db.Fetch<ClientEmailBlastViewModel>(EnumSP.Client.USPGETCLIENTLIST.ToString(), _prm);

        }

        public ClientActivityBalanceHistoryViewModel GetCreditAccountBalanceHistory(string clientId)
        {
            _prm.Add("@ClientId", clientId);
            var result = db.FetchMultiResult<ClientActivityBalanceHistoryViewModel>(EnumSP.Client.USPGETCREDITACCOUNTBALANCEHISTORY.ToString(), _prm);
            return result;
        }

        //public List<ClientEmailBlastViewModel> GetStatementByClientId(int id)
        //{
        //    _prm.Add("ClientId", id);
        //    return db.Fetch<ClientStatementViewModel>(SPEnum.USPGETVEHICLESTATEMENTBYCLIENTID.ToString(), _prm);
        //}

        public List<ClientContactEmail> GetClientEmailList()
        {
            return db.Fetch<ClientContactEmail>(EnumSP.Client.USPGETCLIENTMAILLIST.ToString(), _prm);
        }

        public bool UpdateClientAddressIsNotified(int clientAddressId, bool IsNotified)
        {
            _prm.Add("@ClientAddressId", clientAddressId);
            _prm.Add("@IsNotified", IsNotified);
            db.Save(SPEnum.USPUPDATECLIENTADDRESSISNOTIFIED.ToString(), _prm);
            return true;
        }
    }
}

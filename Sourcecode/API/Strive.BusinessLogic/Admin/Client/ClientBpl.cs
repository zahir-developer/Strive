using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.Common;
using Strive.ResourceAccess;
using System.Net;
using Strive.BusinessEntities.Client;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessLogic.Client;
using Strive.BusinessLogic.Common;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.DTO.User;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;

namespace Strive.BusinessLogic
{
    public class ClientBpl : Strivebase, IClientBpl
    {
        public ClientBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }


        public Result Signup(UserSignupDto clientSignup)
        {
            var commonBpl = new CommonBpl(_cache, _tenant);
            //clientSignup.UserType = UserType.Client.toInt();
            //var authId = new CommonBpl(_cache, _tenant).Signup(clientSignup);
            if (clientSignup.InvitationCode is null) return null;
            var dec = commonBpl.GetDetailsFromInviteCode(clientSignup.InvitationCode);
            var details = dec.Split(',');

            if (details[2].toInt() != (int)UserType.Client) return null;

            clientSignup.TenantId = details[0].toInt();
            clientSignup.ClientId = details[3].toInt();

            var client = new ClientRal(_tenant).GetClientByClientId(clientSignup.ClientId);
            var res = commonBpl.Signup(clientSignup, client);

            return null;
        }

        public Result AddClient(ClientView client)
        {
            string enc = new CommonBpl(_cache, _tenant).GetUserSignupInviteCode(UserType.Client);
            return null;
        }

        public BusinessEntities.Model.Client GetClientByClientId(int id)
        {
            return new ClientRal(_tenant).GetById(id);
        }

        public Result SaveClientDetails(ClientDto client)
        {
            List<int> clientId = new List<int>();
            try
            {
                foreach (var item in client.ClientAddress)
                {
                    if (!string.IsNullOrEmpty(item.Email))
                    {
                        var comBpl = new CommonBpl(_cache, _tenant);
                        var clientLogin = comBpl.CreateLogin(UserType.Client, item.Email, item.PhoneNumber);
                        client.Client.AuthId = clientLogin.authId;
                        

                        if (clientLogin.authId > 0)
                        {
                            var clientSignup = new ClientRal(_tenant).InsertClientDetails(client);
                            if (clientSignup > 0)
                            {
                                var subject = EmailSubject.WelcomeEmail;
                                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                                keyValues.Add("{{emailId}}",item.Email);
                                keyValues.Add("{{password}}",clientLogin.password);
                                keyValues.Add("{{employeeName}}", client.Client.FirstName);
                                keyValues.Add("{{url}}", _tenant.ApplicationUrl);
                                keyValues.Add("{{appUrl}}", _tenant.MobileUrl);

                                comBpl.SendEmail(HtmlTemplate.ClientSignUp, item.Email,keyValues,subject);
                               // comBpl.SendLoginCreationEmail(HtmlTemplate.ClientSignUp, item.Email, clientLogin.password);
                                clientId.Add(clientSignup);
                            }
                            else if(clientLogin.authId > 0)
                            {
                                //Delete AuthMaster record from AuthDatabase in case client add failed.
                                comBpl.DeleteUser(clientLogin.authId);
                            }
                        }
                    }

                }
               
                return ResultWrap(clientId, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdateClientVehicle(ClientDto vehicle)
        {
            try
            {
                return ResultWrap(new ClientRal(_tenant).UpdateClientVehicle, vehicle, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdateAccountBalance(ClientAmountUpdateDto clientAmountUpdate)
        {
            try
            {
                return ResultWrap(new ClientRal(_tenant).UpdateAccountBalance, clientAmountUpdate, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetAllClient(SearchDto searchDto)
        {
            return ResultWrap(new ClientRal(_tenant).GetAllClient, searchDto, "Client");
        }
        public Result GetClientById(int? clientId)
        {
            return ResultWrap(new ClientRal(_tenant).GetClientById, clientId, "Status");
        }
        public Result GetClientVehicleById(int clientId)
        {
            return ResultWrap(new ClientRal(_tenant).GetClientVehicleById, clientId, "Status");
        }
        public Result DeleteClient(int clientId)
        {
            return ResultWrap(new ClientRal(_tenant).DeleteClient, clientId, "Status");
        }
        public Result GetClientSearch(ClientSearchDto search)
        {
            return ResultWrap(new ClientRal(_tenant).GetClientSearch, search, "ClientSearch");
        }
        public Result GetClientCodes()
        {
            return ResultWrap(new ClientRal(_tenant).GetClientCode, "ClientDetails");
        }
        public Result GetStatementByClientId(int id)
        {
            return ResultWrap(new ClientRal(_tenant).GetStatementByClientId, id, "VehicleStatement");
        }
        public Result GetHistoryByClientId(int id)
        {
            return ResultWrap(new ClientRal(_tenant).GetHistoryByClientId, id, "VehicleHistory");
        }

        public Result IsClientName(ClientNameDto clientNameDto)
        {
            return ResultWrap(new ClientRal(_tenant).IsClientName, clientNameDto, "IsClientNameAvailable");
        }

        public Result GetAllClientName(string name)
        {
            return ResultWrap(new ClientRal(_tenant).GetAllClientName, name, "ClientName");
        }
        public Result ClientEmailExist(string email)
        {
            return ResultWrap(new ClientRal(_tenant).ClientEmailExist, email, "emailExist");
        }
        public List<ClientEmailBlastViewModel> ClientExport(EmailBlastDto  emailBlast)
        {
            List<ClientEmailBlastViewModel> client = new List<ClientEmailBlastViewModel>();
            client =new ClientRal(_tenant).GetClientList(emailBlast);
            return client;
        }
        public Result ClientCSVExport(EmailBlastDto emailBlast)
        {
            return ResultWrap(new ClientRal(_tenant).GetClientList, emailBlast, "ClientCSVExport");
        }
    }
}

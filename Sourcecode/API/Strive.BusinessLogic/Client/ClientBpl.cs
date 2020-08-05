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

namespace Strive.BusinessLogic
{
    public class ClientBpl : Strivebase,IClientBpl
    {
        public ClientBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper,cache)
        {
        }

        public Result SaveClientDetails(ClientView lstClient)
        {
            try
            {

                    UserLogin lstclientlst = new UserLogin();
                    lstclientlst.AuthId = 0;
                    lstclientlst.EmailId = lstClient.ClientAddress.FirstOrDefault().Email;
                    lstclientlst.MobileNumber = lstClient.ClientAddress.FirstOrDefault().PhoneNumber;
                    lstclientlst.PasswordHash = "";
                    lstclientlst.CreatedDate = lstClient.CreatedDate;
                    var newitem = new CommonBpl(_cache, _tenant).CreateLogin(lstclientlst);
                    bool blnStatus = new ClientRal(_tenant).SaveClientDetails(lstClient);

                
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllClient()
        {
            try
            {
                var lstClient = new ClientRal(_tenant).GetAllClient();
                _resultContent.Add(lstClient.WithName("Clients"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetClientById(int id)
        {
            try
            {
                var lstClientById = new ClientRal(_tenant).GetClientById(id);
                _resultContent.Add(lstClientById.WithName("ClientDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteClient(int clientId)
        {
            try
            {
                var lstClient = new ClientRal(_tenant).DeleteClient(clientId);
                _resultContent.Add(lstClient.WithName("Client"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
    }
}

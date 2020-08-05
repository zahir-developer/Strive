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
using Strive.BusinessEntities.Model;

namespace Strive.BusinessLogic
{
    public class ClientBpl : Strivebase, IClientBpl
    {
        public ClientBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result SaveClientDetails(ClientList lstClient)
        {
            try
            {
                AuthMaster authMaster = new AuthMaster
                {
                    AuthMasterId = 0,
                    EmailId = lstClient.ClientAddress.FirstOrDefault().Email,
                    MobileNumber = lstClient.ClientAddress.FirstOrDefault().PhoneNumber,
                    PasswordHash = "",
                    CreatedDate = DateTime.Now
                };
                var newitem = new CommonBpl(_cache, _tenant).CreateLogin(authMaster);
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
    }
}

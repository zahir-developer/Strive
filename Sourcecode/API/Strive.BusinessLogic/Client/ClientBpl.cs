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

namespace Strive.BusinessLogic
{
    public class ClientBpl : Strivebase,IClientBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public ClientBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }

        public Result SaveClientDetails(List<ClientList> lstClient)
        {
            try
            {
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

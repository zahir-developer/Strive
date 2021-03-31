using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.PaymentGateway;
using Strive.Common;

namespace Strive.BusinessLogic.PaymentGateway
{
    public class PaymentGatewayBpl : Strivebase, IPaymentGatewayBpl
    {

        public PaymentGatewayBpl(IDistributedCache distributedCache, ITenantHelper tenantHelper) : base(tenantHelper, distributedCache) { }

        public Result VoidTrasaction(CreditCardDto cardDto)
        {

            Result result = new Result();

            var ccRestClient = new CardConnectRestClient(_tenant.CCUrl, _tenant.CCUserName, _tenant.CCPassword);

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", "496160873888");
            // Transaction amount
            request.Add("amount", "0");
            // Transaction currency
            request.Add("currency", "USD");

            //expiry
            request.Add("expiry", cardDto.Expiry);

            // Return Reference code from authorization request
            request.Add("retref", "retref");

            var response = ccRestClient.voidTransaction(request);

            return Helper.BindSuccessResult(response);
        }
    }
}

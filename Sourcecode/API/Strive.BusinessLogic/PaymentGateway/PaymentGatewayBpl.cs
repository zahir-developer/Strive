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
            request.Add("merchid", _tenant.MID);
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

        /**
    * Authorize Transaction with User Fields REST Example
    * @return
    */
        public Result authTransactionWithUserFields(PaymentDto paymentDto)
        {

            Result result = new Result();

            var ccRestClient = new CardConnectRestClient(_tenant.CCUrl, _tenant.CCUserName, _tenant.CCPassword);

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", _tenant.MID);
            // Card Type
            request.Add("accttype", "ZI");
            // Card Number
            request.Add("account", paymentDto.Account);
            // Card Expiry
            request.Add("expiry", paymentDto.Expiry);
            // Card CCV2
            request.Add("cvv2", paymentDto.CCV);
            // Transaction amount
            request.Add("amount", paymentDto.Amount);
            // Transaction currency
            request.Add("currency", paymentDto.Currency);
            // Order ID
            request.Add("orderid", paymentDto.OrderId);
            // Cardholder Name
            request.Add("name", paymentDto.Name);
            // Cardholder Address
            request.Add("Street", "123 Test St");
            // Cardholder City
            request.Add("city", "TestCity");
            // Cardholder State
            request.Add("region", "TestState");
            // Cardholder Country
            request.Add("country", "US");
            // Cardholder Zip-Code
            request.Add("postal", "11111");
            // Return a token for this card number
            request.Add("tokenize", "N");

            // Create user fields
            JArray fields = new JArray();
            JObject field = new JObject();
            field.Add("Tips", 11);
            fields.Add(field);
            request.Add("userfields", fields);

            // Send an AuthTransaction request
            JObject response = ccRestClient.authorizeTransaction(request);

            return Helper.BindSuccessResult(response);

        }
    }
}

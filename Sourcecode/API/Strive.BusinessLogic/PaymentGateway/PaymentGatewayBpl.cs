using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.PaymentGateway;
//using Strive.BusinessLogic.PaymentGateway;
using Strive.Common;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.PaymentGateway
{
    public class PaymentGatewayBpl : Strivebase, IPaymentGatewayBpl
    {

        public PaymentGatewayBpl(IDistributedCache distributedCache, ITenantHelper tenantHelper) : base(tenantHelper, distributedCache) { }

        public Result DeletePaymentGateway(int id)
        {
            return ResultWrap(new PaymentGatewayRal(_tenant).DeletePaymentGateway, id, "PaymentGatewayDelete");
        }


        /**
        * Authorize Transaction
        * @return
        */
        public JObject AuthTransaction(CardPaymentDto cardPaymentDto)
        {

            Result result = new Result();

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", _tenant.MID);
            // Order ID
            request.Add("orderid", cardPaymentDto.PaymentDetail.OrderId);
            // Card Number
            request.Add("account", cardPaymentDto.PaymentDetail.Account);
            // Card Expiry
            request.Add("expiry", cardPaymentDto.PaymentDetail.Expiry);
            // Card CCV2
            request.Add("cvv2", cardPaymentDto.PaymentDetail.CCV);
            // Transaction amount
            request.Add("amount", cardPaymentDto.PaymentDetail.Amount);

            // Cardholder Name
            request.Add("name", cardPaymentDto.BillingDetail.Name);
            // Cardholder Address
            request.Add("address", cardPaymentDto.BillingDetail.Address);
            // Cardholder City
            request.Add("city", cardPaymentDto.BillingDetail.City);
            // Cardholder Country
            request.Add("country", cardPaymentDto.BillingDetail.Country);
            // Cardholder State
            request.Add("region", cardPaymentDto.BillingDetail.Region);
            // Cardholder Zip-Code
            request.Add("postal", cardPaymentDto.BillingDetail.Postal);


            if (cardPaymentDto.IsRepeatTransaction)
            {
                // Profile
                request.Add("profile", cardPaymentDto.CardConnect.Profile);

                // flag these authorizations as recurring billing
                request.Add("ecomind", cardPaymentDto.CardConnect.EcomInd);

                //to identify these authorizations as merchant-initiated stored credential transactions
                request.Add("cof", "M");

                //cofscheduled
                request.Add("cofscheduled", "Y");
            }
            request.Add("track", cardPaymentDto.CardConnect.Track);
            request.Add("capture", cardPaymentDto.CardConnect.Capture);
            request.Add("bin", cardPaymentDto.CardConnect.Bin);


            var ccRestClient = new CardConnectRestClient(_tenant.CCUrl, _tenant.CCUserName, _tenant.CCPassword);

            // Send an AuthTransaction request
            JObject response = ccRestClient.authorizeTransaction(request);

            return response;

        }

        /**
       * Authorize Transaction
       * @return
       */
        public JObject CaptureTransaction(CaptureDetail captureDetail)
        {

            // Create Authorization Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", _tenant.MID);
            // Transaction amount
            request.Add("amount", captureDetail.Amount);
            // Transaction currency
            request.Add("authcode", captureDetail.AuthCode);
            // Order ID
            request.Add("retref", captureDetail.RetRef);
            // Invoice ID
            request.Add("invoiceid", captureDetail.InvoiceId);


            // Create the CardConnect REST client
            var ccRestClient = new CardConnectRestClient(_tenant.CCUrl, _tenant.CCUserName, _tenant.CCPassword);

            // Send a captureTransaction request
            JObject response = ccRestClient.captureTransaction(request);

            return response;

        }

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

        public JObject CreateUpdateProfile(CardPaymentDto cardPaymentDto)// string UserName, string Password, string url, string MID)
        {
            var oMerchantDetails = new PaymentGatewayRal(_tenant).GetMerchantDetails(cardPaymentDto.LocationId);
            Result result = new Result();
            if (oMerchantDetails.Count > 0)
            {
                var ccRestClient = new CardConnectRestClient(oMerchantDetails[0].URL, oMerchantDetails[0].UserName, oMerchantDetails[0].Password);

                // Create Update Transaction request
                JObject request = new JObject();
                // Cardholder Name
                request.Add("name", cardPaymentDto.BillingDetail.Name);
                // Cardholder Address
                request.Add("address", cardPaymentDto.BillingDetail.Address);
                // Cardholder City
                request.Add("city", cardPaymentDto.BillingDetail.City);
                // Cardholder Country
                request.Add("country", cardPaymentDto.BillingDetail.Country);
                // Cardholder State
                request.Add("region", cardPaymentDto.BillingDetail.Region);
                // Cardholder Zip-Code
                request.Add("postal", cardPaymentDto.BillingDetail.Postal);
                // Merchant ID
                request.Add("merchid", oMerchantDetails[0].MID);
                // Transaction currency
                request.Add("currency", "USD");
                //expiry
                request.Add("expiry", cardPaymentDto.PaymentDetail.Expiry);
                // Card Number
                request.Add("account", cardPaymentDto.PaymentDetail.Account);

                // Send a captureTransaction request
                JObject response = ccRestClient.profileTransaction(request);

                return response;
            }
            else
            {
                return null;
            }
        }
        public JObject AuthProfile(string profile, decimal amount, string MID, string url, string UserName, string Password)
        {

            Result result = new Result();

            var ccRestClient = new CardConnectRestClient(url + "auth", UserName, Password);

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", MID);
            // Transaction currency
            request.Add("currency", "USD");
            //expiry
            request.Add("profile", profile);
            // Card Number
            request.Add("amount", amount);
            request.Add("capture", "N");

            // Send a captureTransaction request
            JObject response = ccRestClient.captureTransaction(request);

            return response;
        }

        public bool MakeRecurringPayment(int attempts)
        {
            var paymentBpl = new PaymentGatewayBpl(_cache, _tenant);

            var allMerchantList = new PaymentGatewayRal(_tenant).GetMerchantDetails(attempts);

            foreach (var oMerchant in allMerchantList)
            {
                var allClientList = new PaymentGatewayRal(_tenant).GetRecurringPaymentDetails(oMerchant.LocationId, 0);
                foreach (var oClient in allClientList)
                {
                    JObject oProfile = CaptureProfile(oClient.ProfileId, oClient.Amount, oMerchant.MID, oClient.Username, oClient.Password, oMerchant.URL);
                }
            }

            return true;
        }

        private JObject CaptureProfile(string profileId, decimal amount, string mID, string username, string password, string url)
        {
            Result result = new Result();

            var ccRestClient = new CardConnectRestClient(url + "auth", username, password);

            // Create Update Transaction request
            JObject request = new JObject();
            // Merchant ID
            request.Add("merchid", mID);
            // Transaction currency
            request.Add("currency", "USD");
            //expiry
            request.Add("profile", profileId);
            // Card Number
            request.Add("amount", amount);

            // Send a authorizeTransaction request
            JObject response = ccRestClient.authorizeTransaction(request);

            return response;
        }

       
    }
}

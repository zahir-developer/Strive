using System;
using Greeter.Services.Api;
using Greeter.Services.Authentication;
using Greeter.Services.Network;

namespace Greeter.Common
{
    public class SingleTon
    {
        // App Level Instanaces
        // App Level Injection not dependent on Views

        private static INetworkService iNetworkService = null;
        public static INetworkService NetworkService
        {
            get => iNetworkService == null ? new NetworkService() : iNetworkService;
        }

        private static IApiService iApiService = null;
        public static IApiService ApiService
        {
            get => iApiService == null ? new ApiService() : iApiService;
        }

        private static IAuthenticationService iAuthenticationService = null;
        public static IAuthenticationService AuthenticationService
        {
            get => iAuthenticationService == null ? new AuthenticationService() : iAuthenticationService;
        }

        private static IGeneralApiService iGeneralApiService = null;
        public static IGeneralApiService GeneralApiService
        {
            get => iGeneralApiService == null ? new GeneralApiService() : iGeneralApiService;
        }

        private static IWashApiService iWashApiService = null;
        public static IWashApiService WashApiService
        {
            get => iWashApiService == null ? new WashApiService() : iWashApiService;
        }

        private static IPaymentApiService iPaymentApiService = null;
        public static IPaymentApiService PaymentApiService
        {
            get => iPaymentApiService == null ? new PaymentApiService() : iPaymentApiService;
        }

        private static ICheckoutApiService iCheckoutApiService = null;
        public static ICheckoutApiService ICheckoutApiService
        {
            get => iCheckoutApiService == null ? new CheckoutApiService() : iCheckoutApiService;
        }

        private static IMessageApiService iMessageApiService = null;
        public static IMessageApiService MessageApiService
        {
            get => iMessageApiService == null ? new MessageApiService() : iMessageApiService;
        }
    }
}

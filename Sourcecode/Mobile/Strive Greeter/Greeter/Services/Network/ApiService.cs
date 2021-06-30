using System.Diagnostics;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Newtonsoft.Json;

namespace Greeter.Services.Network
{
    public interface IApiService
    {
        Task<LoginResponse> DoLogin(LoginRequest req);
        Task<LocationsResponse> GetLocations();
        Task<BarcodeResponse> GetBarcode(string barcode);
    }

    public class ApiService : IApiService
    {
        readonly INetworkService iNetworkService;
        private object myProperty;

        public ApiService(INetworkService iNetworkService)
        {
            this.iNetworkService = iNetworkService;
        }

        public Task<LoginResponse> DoLogin(LoginRequest req)
        {
            //IRestRequest request = new RestRequest(Urls.LOGIN, HttpMethod.Post);
            //request.AddBody(req);
            //return iNetworkService.ExecuteAsync<LoginResponse, CommonResponse>(request);

            return DoApiCall<LoginResponse>(Urls.LOGIN, HttpMethod.Post, req, false);
        }

        public Task<LocationsResponse> GetLocations()
        {
            //IRestRequest request = new RestRequest(Urls.LOCATIONS, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<LocationsResponse, CommonResponse>(request);
            return DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        }

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            //IRestRequest request = new RestRequest(subUrl, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<BarcodeResponse, CommonResponse>(request);
            return DoApiCall<BarcodeResponse>(url);
        }

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            //IRestRequest request = new RestRequest(subUrl, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<BarcodeResponse, CommonResponse>(request);
            return DoApiCall<GlobalDataResponse>(url);
        }

        public Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req)
        {
            //IRestRequest request = new RestRequest(Urls.CHECKOUTS, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<CheckoutResponse, CommonResponse>(request);
            return DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Post, req);
        }

        async Task<T> DoApiCall<T>(string subUrl, HttpMethod method = HttpMethod.Get, object req = null, bool isBearerToken = true) where T : BaseResponse
        {
            IRestRequest request = new RestRequest(subUrl, method);

            if (isBearerToken)
            {
                request.AddHeader("Authorization", AppSettings.BearereToken);
                Debug.WriteLine("Bearer Token : " + AppSettings.BearereToken);
            }

            if ((method == HttpMethod.Post || method == HttpMethod.Put) && req is not null)
            {
                request.AddBody(req);
            }
            var commonResponse = await iNetworkService.ExecuteAsync<CommonResponse>(request);

            // Parse json string result to json 
            var response = ParseJsonString<T>(commonResponse.ResultData);
            response.StatusCode = commonResponse.StatusCode;
            response.Message = commonResponse.Message;
            return response;
        }

        T ParseJsonString<T>(string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);
    }
}

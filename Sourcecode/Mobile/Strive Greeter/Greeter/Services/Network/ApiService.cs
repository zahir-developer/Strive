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
            return DoApiCall<LocationsResponse>(Urls.LOCATIONS, HttpMethod.Get);
        }

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            //IRestRequest request = new RestRequest(subUrl, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<BarcodeResponse, CommonResponse>(request);
            return DoApiCall<BarcodeResponse>(url, HttpMethod.Get);
        }

        public Task<CheckoutResponse> GetCheckoutList()
        {
            //IRestRequest request = new RestRequest(Urls.CHECKOUTS, HttpMethod.Get);
            //request.AddHeader("Authorization", AppSettings.BearereToken);
            //return iNetworkService.ExecuteAsync<CheckoutResponse, CommonResponse>(request);
            return DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Get);
        }

        async Task<T> DoApiCall<T>(string subUrl, HttpMethod method, object req = null, bool isBearerToken = true) where T : BaseResponse
        {
            IRestRequest request = new RestRequest(subUrl, method);
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

        T ParseJsonString<T>(string jsonString) => (T)JsonConvert.DeserializeObject<T>(jsonString);
    }
}

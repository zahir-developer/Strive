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
            IRestRequest request = new RestRequest(Urls.LOGIN, HttpMethod.Post);
            request.AddBody(req);
            return iNetworkService.ExecuteAsync<LoginResponse, CommonResponse>(request);

            //return await DoApiCall<LoginResponse>(Urls.LOGIN, HttpMethod.Post, req);
        }

        public Task<LocationsResponse> GetLocations()
        {
            IRestRequest request = new RestRequest(Urls.LOCATIONS, HttpMethod.Get);
            request.AddHeader("Authorization", AppSettings.BearereToken);
            return iNetworkService.ExecuteAsync<LocationsResponse, CommonResponse>(request);
        }

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var subUrl = string.Format(Urls.BARCODE, barcode);
            IRestRequest request = new RestRequest(subUrl, HttpMethod.Get);
            request.AddHeader("Authorization", AppSettings.BearereToken);
            return iNetworkService.ExecuteAsync<BarcodeResponse, CommonResponse>(request);
        }

        public Task<CheckoutResponse> GetCheckoutList()
        {
            IRestRequest request = new RestRequest(Urls.CHECKOUTS, HttpMethod.Get);
            request.AddHeader("Authorization", AppSettings.BearereToken);
            return iNetworkService.ExecuteAsync<CheckoutResponse, CommonResponse>(request);
        }

        //async Task<T> DoApiCall<T>(string subUrl, HttpMethod method, object req = null)
        //{
        //    IRestRequest request = new RestRequest(subUrl, method);
        //(T)iNetworkService.ExecuteAsync<TResult>(request);

        //var resposne = await iNetworkService.ExecuteAsync<CheckoutResponse, CommonResponse>(request);

        // parse tresult Data
        //var some = ParseJsonString<>(resposne);
        //response.StatusCode = result.StatusCode;
        //response.Message = result.Message;
        //}

        T ParseJsonString<T>(string jsonString) => (T)JsonConvert.DeserializeObject<T>(jsonString);
    }
}

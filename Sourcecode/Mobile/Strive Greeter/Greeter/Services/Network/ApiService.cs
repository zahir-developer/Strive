using System.Collections.Generic;
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

        public ApiService(INetworkService iNetworkService)
        {
            this.iNetworkService = iNetworkService;
        }

        public Task<LoginResponse> DoLogin(LoginRequest req)
        {
            return DoApiCall<LoginResponse>(Urls.LOGIN, HttpMethod.Post, null, req, false);
        }

        public Task<LocationsResponse> GetLocations()
        {
            return DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        }

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            return DoApiCall<BarcodeResponse>(url);
        }

        public Task<MakeResponse> GetAllMake()
        {
            return DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        }

        public Task<ModelResponse> GetModelsByMake(int makeId)
        {
            var url = Urls.MODELS_BY_MAKE + makeId;
            return DoApiCall<ModelResponse>(url);
        }

        public Task<ServiceResponse> GetAllSericeDetails(int locationId)
        {
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString() } };
            return DoApiCall<ServiceResponse>(Urls.ALL_SERVICE_DETAILS, HttpMethod.Get, parameters);
        }

        //public Task<> GetAllServiceDetails()
        //{
        //    return DoApiCall<>(Urls.ALL_SERVICE_DETAILS);
        //}

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            return DoApiCall<GlobalDataResponse>(url);
        }

        public Task<TicketResponse> GetTicketNumber(int locationId)
        {
            var url = Urls.TICKET_NUMBER + locationId;
            return DoApiCall<TicketResponse>(url);
        }

        public Task<BaseResponse> CreateService(CreateServiceRequest req)
        {
            return DoApiCall<BaseResponse>(Urls.CREATE_SERVICE, HttpMethod.Post, null, req);
        }

        public Task<CheckoutResponse> GetCheckoutList(CheckoutRequest req)
        {
            return DoApiCall<CheckoutResponse>(Urls.CHECKOUTS, HttpMethod.Post, null, req);
        }

        async Task<T> DoApiCall<T>(string subUrl, HttpMethod method = HttpMethod.Get, Dictionary<string, string> parameters = null, object req = null, bool isBearerToken = true) where T : BaseResponse
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

            if (parameters is not null && parameters.Count > 0)
            {
                foreach (var keyValuePair in parameters)
                {
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
                }
            }

            var commonResponse = await iNetworkService.ExecuteAsync<CommonResponse>(request);

            // Parse json string result to json
            T response = null;
            if (commonResponse?.ResultData != null)
            {
                response = ParseJsonString<T>(commonResponse?.ResultData);
                response.StatusCode = commonResponse.StatusCode;
                response.Message = commonResponse.Message;
            }

            return response;
        }

        T ParseJsonString<T>(string jsonString) => JsonConvert.DeserializeObject<T>(jsonString);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IWashApi
    {
        // IApiService apiService => new ApiService(); // can't access in implementation class
        Task<TicketResponse> GetTicketNumber(int locationId);
        Task<BaseResponse> CreateService(CreateServiceRequest req);
        Task<MakeResponse> GetAllMake();
        Task<ServiceResponse> GetAllSericeDetails(int locationId);
        Task<BarcodeResponse> GetBarcode(string barcode);
        Task<GlobalDataResponse> GetGlobalData(string dataType);
        Task<ModelResponse> GetModelsByMake(int makeId);
        Task<BaseResponse> SendEmail(string email, string subject, string body);
    }

    public class WashApi : IWashApi
    {
        static readonly IApiService apiService = new ApiService();

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            return apiService.DoApiCall<BarcodeResponse>(url);
        }

        public Task<MakeResponse> GetAllMake()
        {
            return apiService.DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        }

        public Task<ModelResponse> GetModelsByMake(int makeId)
        {
            var url = Urls.MODELS_BY_MAKE + makeId;
            return apiService.DoApiCall<ModelResponse>(url);
        }

        //public Task<> GetAllServiceDetails()
        //{
        //    return apiService.DoApiCall<>(Urls.ALL_SERVICE_DETAILS);
        //}

        public Task<ServiceResponse> GetAllSericeDetails(int locationId)
        {
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString() } };
            return apiService.DoApiCall<ServiceResponse>(Urls.ALL_SERVICE_DETAILS, HttpMethod.Get, parameters);
        }

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            return apiService.DoApiCall<GlobalDataResponse>(url);
        }

        public Task<TicketResponse> GetTicketNumber(int locationId)
        {
            var url = Urls.TICKET_NUMBER + locationId;
            return apiService.DoApiCall<TicketResponse>(url);
        }

        public Task<BaseResponse> CreateService(CreateServiceRequest req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.CREATE_SERVICE, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> SendEmail(string email, string subject, string body)
        {
            var parameters = new Dictionary<string, string>() { { nameof(email), email }, { nameof(subject), email }, { nameof(body), body } };
            return apiService.DoApiCall<BaseResponse>(Urls.SEND_EMAIL, HttpMethod.Post);
        }
    }
}

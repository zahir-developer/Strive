using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IVehicleApiService
    {
        Task<BarcodeResponse> GetBarcode(string barcode);
        Task<ModelResponse> GetModelsByMake(long makeId);
        Task<BaseResponse> GetVehicleMembershipDetails(long vehicleId);
    }

    public class VehicleApiService : IVehicleApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            return apiService.DoApiCall<BarcodeResponse>(url);
        }

        public Task<ModelResponse> GetModelsByMake(long makeId)
        {
            var url = Urls.MODELS_BY_MAKE + makeId;
            return apiService.DoApiCall<ModelResponse>(url);
        }

        public Task<BaseResponse> GetVehicleMembershipDetails(long vehicleId)
        {
            var parameters = new Dictionary<string, string>() { { "id", vehicleId.ToString() } };
            return apiService.DoApiCall<BaseResponse>(Urls.GET_VEHICLE_MEMBERSHIP_DETAILS, HttpMethod.Get, parameters);
        }
    }
}

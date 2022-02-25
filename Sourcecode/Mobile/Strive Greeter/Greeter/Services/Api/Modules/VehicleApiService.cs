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
        Task<MembershipResponse> GetVehicleMembershipDetails(long vehicleId);
        Task<VehicleIssueResponse> GetVehicleIssue(long vehicleId);
        Task<DeleteResponse> DeleteVehicleIssue(int issueid);
        Task<BaseResponse> AddVehicleIssue(VehicleIssueAddRequest vehicleIssueAddRequest);
        Task<VehicleImageResponse> GetVehicleIssueImageById(int imageid);
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

        public Task<VehicleIssueResponse> GetVehicleIssue(long vehicleId)
        {
            var url = Urls.GET_VEHICLE_ISSUE + "/" + vehicleId;
            return apiService.DoApiCall<VehicleIssueResponse>(url);
        }

        public Task<DeleteResponse> DeleteVehicleIssue(int issueid)
        {
            var parameters = new Dictionary<string, string>() { { "vehicleIssueId", issueid.ToString() } };
            return apiService.DoApiCall<DeleteResponse>(Urls.DELETE_ISSUE, HttpMethod.Delete, parameters);

        }

        public Task<MembershipResponse> GetVehicleMembershipDetails(long vehicleId)
        {
            var parameters = new Dictionary<string, string>() { { "id", vehicleId.ToString() } };
            return apiService.DoApiCall<MembershipResponse>(Urls.GET_VEHICLE_MEMBERSHIP_DETAILS, HttpMethod.Get, parameters);
        }

        public Task<BaseResponse> AddVehicleIssue(VehicleIssueAddRequest vehicleIssueAddRequest)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.ADD_VEHICLE_ISSUE, HttpMethod.Post, null ,vehicleIssueAddRequest);
        }

        public Task<VehicleImageResponse> GetVehicleIssueImageById(int imageid)
        {
            return apiService.DoApiCall<VehicleImageResponse>(Urls.GET_VEHICLE_ISSUE_IMAGE_ID+imageid);
        }
        
    }
}

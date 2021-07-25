﻿using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;

namespace Greeter.Services.Api
{
    public interface IVehicleApiService
    {
        Task<BarcodeResponse> GetBarcode(string barcode);
        Task<ModelResponse> GetModelsByMake(int makeId);
    }

    public class VehicleApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        public Task<BarcodeResponse> GetBarcode(string barcode)
        {
            var url = Urls.BARCODE + barcode;
            return apiService.DoApiCall<BarcodeResponse>(url);
        }

        public Task<ModelResponse> GetModelsByMake(int makeId)
        {
            var url = Urls.MODELS_BY_MAKE + makeId;
            return apiService.DoApiCall<ModelResponse>(url);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;

namespace Greeter.Modules.Service
{
    public partial class LastVisitViewController
    {
        long clientId;
        string barcode;

        public LastVisitViewController(long clientId, string barcode)
        {
            this.clientId = clientId;
            this.barcode = barcode;
        }

        async Task<GetDetailsSercviesResponse> GetDetailServicesAsync(long clientId)
        {
            ShowActivityIndicator();
            var response = await SingleTon.WashApiService.GetDetailServices(clientId);
            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess())
            {
                return null;
            }

            return response;
        }

        async Task<DetailService> GetLastService(long clientId)
        {
            var response = await GetDetailServicesAsync(clientId);
            var vehicleServices = GetParticularVehicleServices(barcode, response?.DetailsGrid?.DetailServices);

            DetailService vehicleService = null;

            if (vehicleServices is null)
                return vehicleService;

            if (vehicleServices.Count > 1)
            {
                vehicleService = vehicleServices[vehicleServices.Count - 1];
            }
            else
            {
                vehicleService = vehicleServices[0];
            }

            return vehicleService;
        }

        List<DetailService> GetParticularVehicleServices(string barcode, List<DetailService> detailServices)
        {
            return detailServices.Where(x => x.Barcode.Equals(barcode)).ToList();
        }
    }
}
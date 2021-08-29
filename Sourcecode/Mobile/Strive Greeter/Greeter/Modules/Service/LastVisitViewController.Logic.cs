using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;

namespace Greeter.Modules.Service
{
    public partial class LastVisitViewController
    {
        long clientId;
        public LastVisitViewController(long clientId)
        {
            this.clientId = clientId;
        }

        async Task<GetDetailsSercviesResponse> GetLastServiceAsync(long clientId)
        {
            ShowActivityIndicator();
            var response = await SingleTon.WashApiService.GetDetailServices(clientId);
            HideActivityIndicator();

            HandleResponse(response);

            if (!response.IsSuccess())
            {
                return null;
            }

            UpdateDataToUI(response?.DetailsGrid?.DetailServices[response.DetailsGrid.DetailServices.Count -1]);

            return response;
        }
    }
}
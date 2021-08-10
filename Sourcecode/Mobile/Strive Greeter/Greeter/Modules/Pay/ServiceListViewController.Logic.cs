using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Newtonsoft.Json;

namespace Greeter.Modules.Pay
{
	public partial class ServiceListViewController
    {
		List<Checkout> Checkouts;

        public ServiceListViewController()
        {
            GetCheckoutListAsync().ConfigureAwait(false);
        }

        async Task GetCheckoutListAsync()
        {
            var checkoutRequest = new CheckoutRequest
            {
                StartDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API),
                EndDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API),
                LocationID = AppSettings.LocationID,
                SortBy = "TicketNumber",
                SortOrder = "ASC",
                Status = true,
            };

#if DEBUG
            checkoutRequest.StartDate = DateTime.Now.Date.AddMonths(-1).ToString(Constants.DATE_FORMAT_FOR_API);
#endif

            ShowActivityIndicator();
            var response = await new CheckoutApiService().GetCheckoutList(checkoutRequest);
            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess())
            {
                Checkouts = FilterUnpaidItems(response?.CheckinVehicleDetails?.CheckOutList);
                Debug.WriteLine("Unpaid Filtered Services " + JsonConvert.SerializeObject(Checkouts));
                if (IsViewLoaded)
                    checkoutTableView.ReloadData();
            }
        }

        List<Checkout> FilterUnpaidItems(List<Checkout> checkouts)
        {
            return checkouts?.Where(checkout => !checkout.PaymentStatus.Equals("Success")).ToList();
        }

        void PayBtnClicked(Checkout checkout)
        {
            NavigateToPayScreen(checkout);
        }

        void NavigateToPayScreen(Checkout checkout)
        {
            var vc = new PaymentViewController();
            vc.JobID = checkout.ID;
            vc.Make = checkout.VehicleMake;
            vc.Model = checkout.VehicleModel;
            vc.Color = checkout.VehicleColor;
            vc.ServiceName = checkout.Services;
            vc.CustName = checkout.CustomerFirstName + " "+ checkout.CustomerLastName;
            vc.Amount = checkout.Cost;
            vc.IsFromNewService = false;
            NavigateToWithAnim(vc);
        }
    }
}

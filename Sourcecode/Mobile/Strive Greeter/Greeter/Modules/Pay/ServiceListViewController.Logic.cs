using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

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
                StartDate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                LocationID = AppSettings.LocationID,
                SortBy = "TicketNumber",
                SortOrder = "ASC",
                Status = true,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().GetCheckoutList(checkoutRequest);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                Checkouts = FilterUnpaidItems(response?.CheckinVehicleDetails?.CheckOutList);
                if (IsViewLoaded)
                    checkoutTableView.ReloadData();
            }
        }

        List<Checkout> FilterUnpaidItems(List<Checkout> checkouts)
        {
            return checkouts?.Where(checkout => checkout.PaymentStatus.Equals("Success")).ToList();
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
            vc.Amount = checkout.Cost;
            vc.IsFromNewService = false;
            NavigateToWithAnim(vc);
        }
    }
}

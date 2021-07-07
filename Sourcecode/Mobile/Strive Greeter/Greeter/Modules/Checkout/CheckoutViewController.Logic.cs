using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;

namespace Greeter.Modules.Pay
{
    public partial class CheckoutViewController
    {
        List<Checkout> Checkouts;

        public CheckoutViewController()
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
            var response = await new ApiService(new NetworkService()).GetCheckoutList(checkoutRequest);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                Checkouts = response.CheckinVehicleDetails.CheckOutList;
                if (IsViewLoaded)
                    checkoutTableView.ReloadData();
            }
        }
    }
}

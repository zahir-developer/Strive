using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

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
                StartDate = DateTime.Now.Date.AddMonths(-1).ToString("yyyy-MM-dd"),
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
                Checkouts = response.CheckinVehicleDetails.CheckOutList;
                if (IsViewLoaded)
                    checkoutTableView.ReloadData();
            }
        }

        void HoldBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.HOLD_VERIFICATION_MSG, () =>
            {
                _ = HoldCheckout(checkout);
            }, true);
        }

        async Task HoldCheckout(Checkout checkout)
        {
            var checkoutHoldReq = new HoldCheckoutReq
            {
                ID = checkout.ID,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().HoldCheckout(checkoutHoldReq);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_HOLD_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                });
            }
        }

        void CompleteBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.COMPLETE_VERIFICATION_MSG, () =>
            {
                _ = CompleteCheckout(checkout);
            }, true);
        }

        async Task CompleteCheckout(Checkout checkout)
        {
            var checkoutCompleteReq = new CompleteCheckoutReq
            {
                JobId = checkout.ID,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().CompleteCheckout(checkoutCompleteReq);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_COMPLETED_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                });
            }
        }

        void CheckoutBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.CHECKOUT_VERIFICATION_MSG, () =>
            {
                _ = Checkout(checkout);
            }, true);
        }

        async Task Checkout(Checkout checkout)
        {
            var checkoutReq = new DoCheckoutReq
            {
                JobId = checkout.ID,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().DoCheckout(checkoutReq);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_CHECKED_OUT_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                });
            }
        }
    }
}

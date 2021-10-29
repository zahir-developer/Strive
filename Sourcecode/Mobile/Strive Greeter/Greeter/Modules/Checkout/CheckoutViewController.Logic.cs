using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

namespace Greeter.Modules.Pay
{
    public partial class CheckoutViewController
    {
        List<Checkout> Checkouts = new List<Checkout>();

        //bool isFinished = false;
        //short lastPagePos = 0;
        //readonly short limit = Constants.PAGINATION_LIMIT;

        public CheckoutViewController()
        {
            //LoadItems(lastPagePos).ConfigureAwait(false);
            //GetCheckouts().ConfigureAwait(false);
        }

        async Task GetCheckoutListAsync()
        {
            ShowActivityIndicator();
            Checkouts = await GetCheckoutListFromApiAsync();
            if (IsViewLoaded)
                checkoutTableView.ReloadData();
            HideActivityIndicator();
        }

        async Task<List<Checkout>> GetCheckoutListFromApiAsync()
        {
            var checkoutRequest = new CheckoutRequest
            {
                StartDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API),
                EndDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API),
                LocationID = AppSettings.LocationID,
                SortBy = "TicketNumber",
                SortOrder = "ASC",
                Status = true,
                //PageNo = pagePos,
                //Limit = limit
            };

            #if DEBUG
                Console.WriteLine("daswe " + DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss"));
                //checkoutRequest.StartDate = DateTime.Now.Date.AddMonths(-1).ToString(Constants.DATE_FORMAT_FOR_API);
            #endif

            var response = await new CheckoutApiService().GetCheckoutList(checkoutRequest);

            List<Checkout> checkouts = null;

            HandleResponse(response);

            if (response.IsSuccess())
            {
                checkouts = response.CheckinVehicleDetails.CheckOutList;
                //if (checkouts.Count == 0)
                //{
                //    isFinished = true;
                //}

                return checkouts;
            }

            return checkouts;
        }

        //async Task GetCheckouts()
        //{
        //    //RestartPagination();
        //    //_ = LoadItems(lastPagePos);
        //    Checkouts = await GetCheckoutListAsync();
        //    if (IsViewLoaded)
        //        checkoutTableView.ReloadData();
        //}

        

        //void RestartPagination()
        //{
        //    lastPagePos = 0;
        //    isFinished = false;
        //}

        //async Task LoadItems(short lastPagePos)
        //{
        //   var list = await GetCheckoutListAsync((short)(lastPagePos + 1));

        //    if (list.Count < limit)
        //    {
        //        isFinished = true;
        //    }

        //    if (list is not null)
        //    {
        //        Checkouts.AddRange(list);
        //        if (IsViewLoaded)
        //            checkoutTableView.ReloadData();
        //    }
        //}

        void HoldBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.HOLD_VERIFICATION_MSG, () =>
            {
                _ = HoldCheckout(checkout);
            }, true, Common.Messages.HOLD);
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

            HandleResponse(response);

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_HOLD_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                }, titleTxt: Common.Messages.HOLD);
            }
        }

        void CompleteBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.COMPLETE_VERIFICATION_MSG, () =>
            {
                _ = CompleteCheckout(checkout);
            }, true, Common.Messages.COMPLETE);
        }

        async Task CompleteCheckout(Checkout checkout)
        {
            var checkoutCompleteReq = new CompleteCheckoutReq
            {
                JobID = checkout.ID,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().CompleteCheckout(checkoutCompleteReq);
            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_COMPLETED_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                }, titleTxt: Common.Messages.COMPLETE);
            }
        }

        void CheckoutBtnClicked(Checkout checkout)
        {
            // Not Completed
            if (!checkout.JobStatus.Equals("Completed"))
            {
                ShowAlertMsg(Common.Messages.NOT_COMPLETED_ALERT_MSG, null, false, Common.Messages.IN_PROGRESS);
                return;
            }

            // For Paid 
            if (string.IsNullOrEmpty(checkout.MembershipName) && !checkout.PaymentStatus.Equals("Success"))
            {
                ShowAlertMsg(Common.Messages.NOT_PAID_ALERT_MSG, null, false, Common.Messages.NOT_PAID);
                return;
            }

            // Membership But Service Type Is Detail
            //if (string.IsNullOrEmpty(checkout.MembershipName) && checkout.Services && !checkout.PaymentStatus.Equals("Success"))
            //{
            //    ShowAlertMsg(Common.Messages.NOT_PAID_ALERT_MSG, null, false, Common.Messages.NOT_PAID);
            //    return;
            //}

            ShowAlertMsg(Common.Messages.CHECKOUT_VERIFICATION_MSG, () =>
            {
                _ = Checkout(checkout);
            }, true, Common.Messages.CHECKOUT);
        }

        async Task Checkout(Checkout checkout)
        {
            var checkoutReq = new DoCheckoutReq
            {
                JobID = checkout.ID,
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().DoCheckout(checkoutReq);
            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess())
            {
                ShowAlertMsg(Common.Messages.SERVICE_CHECKED_OUT_SUCCESS_MSG, () =>
                {
                    // Refreshing checkout list
                    _ = GetCheckoutListAsync();
                }, titleTxt:Common.Messages.CHECKOUT);
            }
        }

        void PrintReceipt(Checkout checkout)
        {
            string printContentHtml = MakeServiceReceipt(checkout);
            Print(printContentHtml);
        }

        string MakeServiceReceipt(Checkout checkout)
        {
            var body = "<p>Ticket Number : </p>" + checkout.ID + "<br /><br />";

            if (!string.IsNullOrEmpty(checkout.CustomerFirstName))
            {
                body += "<p>Customer Details : </p>" + ""
                    + "<p>Customer Name - " + checkout.CustomerFirstName + " " + checkout.CustomerLastName + "</p><br />";
            }

            body += "<p>Vehicle Details : </p>" +
                 "<p>Make - " + checkout.VehicleMake + "</p>" +
                "<p>Model - " + checkout.VehicleModel + "</p>" +
                 "<p>Color - " + checkout.VehicleColor + "</p><br />" +
                 "<p>Services : " + "</p>";

            if (!string.IsNullOrEmpty(checkout.Services))
            {
                body += "<p>" + checkout.Services + "</p>";
            }

            if (!string.IsNullOrEmpty(checkout.AdditionalServices) && !checkout.AdditionalServices.Equals("none", StringComparison.OrdinalIgnoreCase))
            {
                body += "<p>" + checkout.AdditionalServices + "</p>";
            }

            if (checkout.PaymentStatus.Equals("Success"))
            {
                body += "<br/ ><p>" + "Total Amount Paid: " + "$" + checkout.Cost.ToString() + "</p>";
            }
            else
            {
                body += "<br/ ><p>" + "Total Amount Due: " + "$" + checkout.Cost.ToString() + "</p>";
                body += "<br/ ><p>Note: Please avoid if you already paid.</p>";
            }

            Debug.WriteLine("Email Body :" + body);

            return body;
        }
    }
}

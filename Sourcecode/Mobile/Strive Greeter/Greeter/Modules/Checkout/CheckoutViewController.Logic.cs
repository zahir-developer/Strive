﻿using System;
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
        List<Checkout> Checkouts = new List<Checkout>();

        //bool isFinished = false;
        //short lastPagePos = 0;
        //readonly short limit = Constants.PAGINATION_LIMIT;

        public CheckoutViewController()
        {
            //LoadItems(lastPagePos).ConfigureAwait(false);
            GetCheckoutListAsync().ConfigureAwait(false);
        }

        async Task<List<Checkout>> GetCheckoutListAsync()
        {
            var checkoutRequest = new CheckoutRequest
            {
                StartDate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                EndDate = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                LocationID = AppSettings.LocationID,
                SortBy = "TicketNumber",
                SortOrder = "ASC",
                Status = true,
                //PageNo = pagePos,
                //Limit = limit
            };

            ShowActivityIndicator();
            var response = await new CheckoutApiService().GetCheckoutList(checkoutRequest);
            HideActivityIndicator();

            List<Checkout> checkouts = null;

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return checkouts;
            }

            if (response.IsSuccess())
            {
                //checkouts = response.CheckinVehicleDetails.CheckOutList;
                //if (checkouts.Count == 0)
                //{
                //    isFinished = true;
                //}

                return checkouts;
            }

            return checkouts;
        }

        async Task RefreshCheckouts()
        {
            //RestartPagination();
            //_ = LoadItems(lastPagePos);
            Checkouts = await GetCheckoutListAsync();
        }

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
                    RefreshCheckouts();
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
                    RefreshCheckouts();
                }, titleTxt: Common.Messages.COMPLETE);
            }
        }

        void CheckoutBtnClicked(Checkout checkout)
        {
            ShowAlertMsg(Common.Messages.CHECKOUT_VERIFICATION_MSG, () =>
            {
                _ = Checkout(checkout);
            }, true, Common.Messages.CHECKOUT);
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
                    RefreshCheckouts();
                }, titleTxt:Common.Messages.CHECKOUT);
            }
        }
    }
}
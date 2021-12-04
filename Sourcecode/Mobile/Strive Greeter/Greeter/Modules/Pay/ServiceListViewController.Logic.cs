﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Greeter.Storyboards;
using Newtonsoft.Json;

namespace Greeter.Modules.Pay
{
	public partial class ServiceListViewController
    {
		List<Checkout> Checkouts;
        List<Checkout> FilteredCheckouts = new List<Checkout>();

        public ServiceListViewController()
        {
            
        }

        async Task GetCheckoutListAsync()
        {
            ShowActivityIndicator();
            await GetCheckoutListFromApiAsync();
            if (IsViewLoaded)
                checkoutTableView.ReloadData();
            HideActivityIndicator();
        }

        async Task GetCheckoutListFromApiAsync()
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

            var response = await new CheckoutApiService().GetCheckoutList(checkoutRequest);

            HandleResponse(response);

            if (response.IsSuccess())
            {
                Checkouts = FilterUnpaidItems(response?.CheckinVehicleDetails?.CheckOutList);
                FilteredCheckouts = Checkouts;
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
            if (!string.IsNullOrEmpty(checkout.MembershipName) && checkout.JobType.Equals(ServiceType.Wash.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                ShowAlertMsg(Common.Messages.MEMBERSHIP_MESSAGE, () => { });
                return;
            }

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

            if (checkout.JobType.Equals(ServiceType.Wash.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                vc.ServiceType = ServiceType.Wash;
            }
            else if (checkout.JobType.Equals(ServiceType.Detail.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                vc.ServiceType = ServiceType.Detail;
            }

            vc.AdditionalServiceName = checkout.AdditionalServices;
            //vc.IsFromNewService = false;
            NavigateToWithAnim(vc);
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

            body += "<br/ ><p>" + "Total Amount Due: " + "$" + checkout.Cost.ToString() + "</p>";

            body += "<br/ ><p>Note: Please disregard if you already paid.</p>";

            Debug.WriteLine("Email Body :" + body);

            return body;
        }

        void dsa(string text)
        {
            if (string.IsNullOrWhiteSpace(text.Trim()))
            {
                FilteredCheckouts = Checkouts;
                checkoutTableView.ReloadData();
                return;
            }

            FilteredCheckouts = FilterCheckout(text, Checkouts);
            if (!FilteredCheckouts.IsNullOrEmpty())
            {
                checkoutTableView.ReloadData();
            }
        }

        List<Checkout> FilterCheckout(string ticketId, List<Checkout> checkouts)
        {
            List<Checkout> filteredCheckouts = null;
            filteredCheckouts = checkouts.Where(x => x.ID.ToString().Contains(ticketId)).ToList();
            return filteredCheckouts;
        }

        //ServiceType GetSerViceType(string jobType)
        //   => jobType switch
        //   {
        //       ServiceType.Wash.ToString() => ServiceType.Wash,
        //       ServiceType.Detail.ToString() => ServiceType.Detail,
        //       _ => throw new ArgumentException($"{jobType} JobType not supported")
        //   };
    }
}

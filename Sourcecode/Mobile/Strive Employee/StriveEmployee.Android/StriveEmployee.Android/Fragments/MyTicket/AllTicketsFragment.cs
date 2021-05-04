using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.MyTicket;
using StriveEmployee.Android.Adapter.MyTickets;
using StriveEmployee.Android.DemoTicketsData;

namespace StriveEmployee.Android.Fragments.MyTicket
{
    public class AllTicketsFragment : MvxFragment<AllTicketsViewModel>
    {
        private RecyclerView allTickets_RecyclerView;
        private AllTickets alltickets;
        private List<TicketsModule> tickets;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.AllTickets_Fragment, null);
            this.ViewModel = new AllTicketsViewModel();
            allTickets_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.allTickets_RecyclerView);

           // GetTickets();

           


            return rootView;
        }

        private async void GetTickets()
        {
            TicketsModule ticket1 = new TicketsModule();
            ticket1.TicketNumber = "345346";
            ticket1.MakeModelColor = "Chevrolet Black Sedan";
            ticket1.WashService = "Ultra Mammoth";
            ticket1.Upcharge = "Small SUV";
            ticket1.Barcode = "#qw4134";
            ticket1.Customer = "Felix";
            ticket1.AdditionalServices = "Sealed wax";

            TicketsModule ticket2 = new TicketsModule();
            ticket2.TicketNumber = "12368";
            ticket2.MakeModelColor = "Ferrari Red Sports";
            ticket2.WashService = "Mini Mammoth";
            ticket2.Upcharge = "Sports";
            ticket2.Barcode = "#Fx75323";
            ticket2.Customer = "Anika";
            ticket2.AdditionalServices = "Sealed wax";
            
            TicketsModule ticket3 = new TicketsModule();
            ticket3.TicketNumber = "951851";
            ticket3.MakeModelColor = "Chevrolet Red SUV";
            ticket3.WashService = "Ultra Mammoth";
            ticket3.Upcharge = "Small SUV";
            ticket3.Barcode = "#AW5421";
            ticket3.Customer = "Jonathan";
            ticket3.AdditionalServices = "Sealed wax";

            tickets = new List<TicketsModule>();
            tickets.Add(ticket1);
            tickets.Add(ticket2);
            tickets.Add(ticket3);

            alltickets = new AllTickets(Context, tickets);
            var layoutManager = new LinearLayoutManager(Context);
            allTickets_RecyclerView.SetLayoutManager(layoutManager);
            allTickets_RecyclerView.SetAdapter(alltickets);

        }
    }
}
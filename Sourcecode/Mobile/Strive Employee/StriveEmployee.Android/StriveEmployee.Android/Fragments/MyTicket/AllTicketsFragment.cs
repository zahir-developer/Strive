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
using StriveEmployee.Android.Adapter.MyTIckets;

namespace StriveEmployee.Android.Fragments.MyTicket
{
    public class AllTicketsFragment : MvxFragment<AllTicketsViewModel>
    {
        private AllTicketsAdapter allTickets_Adapter;
        private RecyclerView allTickets_RecyclerView;
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
            GetAllTickets();

            return rootView;
        }

        public async void GetAllTickets()
        {
            await this.ViewModel.GetAllTickets();
            if(this.ViewModel.allTickets != null)
            {
                if(this.ViewModel.allTickets.Washes.Count > 0)
                {
                    allTickets_Adapter = new AllTicketsAdapter(Context, this.ViewModel.allTickets);
                    var linearLayout = new LinearLayoutManager(Context);
                    allTickets_RecyclerView.SetLayoutManager(linearLayout);
                    allTickets_RecyclerView.SetAdapter(allTickets_Adapter);
                }
            }
        }
    }
}
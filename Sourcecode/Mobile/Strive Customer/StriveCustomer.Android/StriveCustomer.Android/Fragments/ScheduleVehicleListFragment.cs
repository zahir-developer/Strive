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
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class ScheduleVehicleListFragment : MvxFragment<ScheduleViewModel>
    {
        private RecyclerView ScheduleVehicleList_RecyclerView;
        private ScheduleVehicleListAdapter scheduleListAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleVehicleList, null);
            this.ViewModel = new ScheduleViewModel();
            
            ScheduleVehicleList_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.scheduleVehicle_RecyclerView);
            GetScheduleVehicleList();
            
            return rootView;
        }

        public async void GetScheduleVehicleList()
        {
           await this.ViewModel.GetScheduleVehicleList();
            if (this.ViewModel != null)
            {
             if (this.ViewModel.scheduleVehicleList != null || this.ViewModel.scheduleVehicleList.Status.Count > 0)
                {
                    scheduleListAdapter = new ScheduleVehicleListAdapter(this.ViewModel.scheduleVehicleList);

                    LinearLayoutManager layoutManager = new LinearLayoutManager(Context);
                    ScheduleVehicleList_RecyclerView.SetLayoutManager(layoutManager);
                    ScheduleVehicleList_RecyclerView.SetAdapter(scheduleListAdapter);
                }
            }
        }
    }
}
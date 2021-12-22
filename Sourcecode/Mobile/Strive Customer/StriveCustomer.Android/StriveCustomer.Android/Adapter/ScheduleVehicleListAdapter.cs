using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Adapter
{
    public class ScheduleVehicleListViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {

        public TextView ScheduleVehicleName_TextView;
        public TextView ScheduleVehicleServiceName_TextView;
        public Button scheduleNow_Button;
        public IItemClickListener itemClickListener;
        public ScheduleVehicleListViewHolder(View dealItem) : base(dealItem)
        {
            ScheduleVehicleName_TextView = dealItem.FindViewById<TextView>(Resource.Id.scheduleVehicleName_TextView);
            ScheduleVehicleServiceName_TextView = dealItem.FindViewById<TextView>(Resource.Id.scheduleServiceName_TextView);
            scheduleNow_Button = dealItem.FindViewById<Button>(Resource.Id.scheduleNow_Button);
            scheduleNow_Button.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            itemClickListener.OnClick(view, AdapterPosition, false);
        }
    }
    public class ScheduleVehicleListAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private ScheduleLocationsFragment selectLocationFragment;
        private VehicleList VehicleLists = new VehicleList();
        private ScheduleVehicleListViewHolder vehicleListViewHolder;
        public ScheduleVehicleListAdapter(VehicleList vehicleList)
        {
            VehicleLists.Status = new List<VehicleDetail>();
            this.VehicleLists = vehicleList;
        }
        public override int ItemCount
        { 
            get
            {
                return VehicleLists.Status.Count;
            }
        
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            vehicleListViewHolder = holder as ScheduleVehicleListViewHolder;
            vehicleListViewHolder.ScheduleVehicleName_TextView.Text = VehicleLists.Status[position].VehicleColor +" "+ VehicleLists.Status[position].VehicleMfr;
            vehicleListViewHolder.ScheduleVehicleServiceName_TextView.Text = VehicleLists.Status[position].MembershipName;
            vehicleListViewHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            selectLocationFragment = new ScheduleLocationsFragment();
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            CustomerScheduleInformation.ScheduledVehicleName = VehicleLists.Status[position].VehicleColor +" "+ VehicleLists.Status[position].VehicleMfr;
            CustomerScheduleInformation.ScheduleSelectedVehicle = VehicleLists.Status[position];
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, selectLocationFragment).Commit();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.ScheduleVehicleList_ItemView, parent, false);
            return new ScheduleVehicleListViewHolder(itemView);
        }
    }
}
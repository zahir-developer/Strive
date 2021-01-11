﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.TimInventory;

namespace StriveCustomer.Android.Adapter
{
    public class ScheduleVehicleListViewHolder : RecyclerView.ViewHolder
    {

        public TextView ScheduleVehicleName_TextView;
        public TextView ScheduleVehicleServiceName_TextView;
        public ScheduleVehicleListViewHolder(View dealItem) : base(dealItem)
        {
            ScheduleVehicleName_TextView = dealItem.FindViewById<TextView>(Resource.Id.scheduleVehicleName_TextView);
            ScheduleVehicleServiceName_TextView = dealItem.FindViewById<TextView>(Resource.Id.scheduleServiceName_TextView);
        }
    }
    public class ScheduleVehicleListAdapter : RecyclerView.Adapter
    {

        Context context;
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
            vehicleListViewHolder.ScheduleVehicleName_TextView.Text = VehicleLists.Status[position].VehicleColor + VehicleLists.Status[position].VehicleMfr;
            vehicleListViewHolder.ScheduleVehicleServiceName_TextView.Text = "";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.ScheduleVehicleList_ItemView, parent, false);
            return new ScheduleVehicleListViewHolder(itemView);
        }
    }
}
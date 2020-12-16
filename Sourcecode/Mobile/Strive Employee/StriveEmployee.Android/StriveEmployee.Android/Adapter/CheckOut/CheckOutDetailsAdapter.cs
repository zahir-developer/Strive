﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckOut;

namespace StriveEmployee.Android.Adapter.CheckOut
{
    public class CheckOutDetailsViewHolder : RecyclerView.ViewHolder
    {
        public TextView checkOutNumber_TextView;
        public TextView checkOutName_TextView;
        public TextView checkOutVehicle_TextView;
        public TextView checkOutServicePrice_TextView;
        public TextView checkOutServiceName_TextView;
        public TextView checkInTime_TextView;
        public TextView checkOutTime_TextView;
        public TextView checkoutPayment_TextView;
        public ImageView checkOut_ImageView;
        public LinearLayout verticalLine_LinearLayout;
        public LinearLayout membershipStatus_LinearLayout;
        public CheckOutDetailsViewHolder(View itemView) : base(itemView)
        {
            checkOutNumber_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutNumber_TextView);
            checkOutName_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutName_TextView);
            checkOutVehicle_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutVehicle_TextView);
            checkOutServicePrice_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutServicePrice_TextView);
            checkOutServiceName_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutServiceName_TextView);
            checkInTime_TextView = itemView.FindViewById<TextView>(Resource.Id.checkInTime_TextView);
            checkOutTime_TextView = itemView.FindViewById<TextView>(Resource.Id.checkOutTime_TextView);
            checkoutPayment_TextView = itemView.FindViewById<TextView>(Resource.Id.checkoutPayment_TextView);
            checkOut_ImageView = itemView.FindViewById<ImageView>(Resource.Id.checkOut_ImageView);
            verticalLine_LinearLayout = itemView.FindViewById<LinearLayout>(Resource.Id.verticalLine_LinearLayout);
            membershipStatus_LinearLayout = itemView.FindViewById<LinearLayout>(Resource.Id.membershipStatus_LinearLayout);

        }
    }
    public class CheckOutDetailsAdapter : RecyclerView.Adapter
    {

        Context context;
        private CheckOutDetailsViewHolder detailViewHolder;
        private CheckOutVehicleDetails vehicleDetails;

        public CheckOutDetailsAdapter(Context context, CheckOutVehicleDetails vehicleDetails)
        {
            this.context = context;
            this.vehicleDetails = vehicleDetails;
        }

        public override int ItemCount
        {
            get 
            {
                return vehicleDetails.GetCheckedInVehicleDetails.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            detailViewHolder = holder as CheckOutDetailsViewHolder;
            detailViewHolder.checkOutNumber_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].TicketNumber;
            detailViewHolder.checkOutName_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].CustomerName;
            detailViewHolder.checkOutVehicle_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].VehicleDescription;
            detailViewHolder.checkOutServicePrice_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].Cost.ToString();
            detailViewHolder.checkOutServiceName_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].Services;
            detailViewHolder.checkInTime_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].Checkin;
            detailViewHolder.checkOutTime_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails[position].Checkout;
            detailViewHolder.verticalLine_LinearLayout.SetBackgroundColor(Color.ParseColor(vehicleDetails.GetCheckedInVehicleDetails[position].ColorCode));
            detailViewHolder.checkOutNumber_TextView.SetTextColor(Color.ParseColor(vehicleDetails.GetCheckedInVehicleDetails[position].ColorCode));
            if(vehicleDetails.GetCheckedInVehicleDetails[position].MembershipNameOrPaymentStatus == "In Progress")
            {

            }
            else if(vehicleDetails.GetCheckedInVehicleDetails[position].MembershipNameOrPaymentStatus == "PAID")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "PAID";
                detailViewHolder.checkOut_ImageView.SetImageResource(Resource.Drawable.paidCheck);
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#AFE9E3"));
            }
            else if(vehicleDetails.GetCheckedInVehicleDetails[position].MembershipNameOrPaymentStatus == "Completed")
            {

            }
            else
            {

            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.CheckOut_ItemView, parent, false);
            return new CheckOutDetailsViewHolder(itemView);
        }
    }
}
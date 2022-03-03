using System;
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

namespace StriveOwner.Android.Adapter.CheckOut
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
        public LinearLayout membershipDetails_LinearLayout;
        public ImageView checkOut_MembershipBadge;
        public TextView checkOut_MembershipName;
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
            membershipDetails_LinearLayout = itemView.FindViewById<LinearLayout>(Resource.Id.membershipDetail_LinearLayout);
            checkOut_MembershipBadge = itemView.FindViewById<ImageView>(Resource.Id.checkOutMembership_ImageView);
            checkOut_MembershipName = itemView.FindViewById<TextView>(Resource.Id.checkoutMembership_TextView);

        }
    }
    public class CheckOutDetailsAdapter : RecyclerView.Adapter
    {

        Context context;
        private CheckOutDetailsViewHolder detailViewHolder;
        private CheckoutDetails vehicleDetails;

        public CheckOutDetailsAdapter(Context context, CheckoutDetails vehicleDetails)
        {
            this.context = context;
            this.vehicleDetails = vehicleDetails;
        }

        public override int ItemCount
        {
            get 
            {
                return vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            detailViewHolder = holder as CheckOutDetailsViewHolder;
            detailViewHolder.checkOutNumber_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].TicketNumber;
            detailViewHolder.checkOutName_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].CustomerFirstName + " "+ vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].CustomerLastName;
            detailViewHolder.checkOutVehicle_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].VehicleDescription;
            detailViewHolder.checkOutServicePrice_TextView.Text = "$" + vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].Cost.ToString();
            detailViewHolder.checkOutServiceName_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].Services;
            detailViewHolder.checkInTime_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].Checkin;
            detailViewHolder.checkOutTime_TextView.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].Checkout;
            if(!string.IsNullOrEmpty(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].ColorCode))
            {
                detailViewHolder.verticalLine_LinearLayout.SetBackgroundColor(Color.ParseColor(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].ColorCode));
                detailViewHolder.checkOutNumber_TextView.SetTextColor(Color.ParseColor(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].ColorCode));

            }
            if (string.IsNullOrEmpty(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipName) || string.IsNullOrWhiteSpace(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipName))
            {
                detailViewHolder.membershipDetails_LinearLayout.Visibility = ViewStates.Gone;
            }
            else
            {
                detailViewHolder.membershipDetails_LinearLayout.Visibility = ViewStates.Visible;
                detailViewHolder.checkOut_MembershipName.Text = vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipName;
                detailViewHolder.checkOut_MembershipBadge.SetImageResource(Resource.Drawable.membershipMark);
                detailViewHolder.membershipDetails_LinearLayout.SetBackgroundColor(Color.ParseColor("#AFE9E3"));

            }
            if (vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipNameOrPaymentStatus == "In Progress")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "IN PROGRESS";
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Gone;
            }
            else if(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipNameOrPaymentStatus == "Paid")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "PAID";
                detailViewHolder.checkOut_ImageView.SetImageResource(Resource.Drawable.paidCheck);
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Visible;
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#AFE9E3"));
            }
            else if(vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipNameOrPaymentStatus == "Completed")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "COMPLETED";
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Gone;
            }
            else if (vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipNameOrPaymentStatus == "Canceled")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "CANCELLED";
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Gone;
            }
            else if (vehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[position].MembershipNameOrPaymentStatus == "Open")
            {
                detailViewHolder.checkoutPayment_TextView.Text = "OPEN";
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Gone;
            }
            else
            {
                detailViewHolder.checkoutPayment_TextView.Text = "OPEN";
                detailViewHolder.membershipStatus_LinearLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));
                detailViewHolder.checkOut_ImageView.Visibility = ViewStates.Gone;
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
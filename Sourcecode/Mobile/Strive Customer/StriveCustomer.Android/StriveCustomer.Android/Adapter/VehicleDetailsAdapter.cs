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
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Fragments;
using StriveCustomer.Android.Resources.Enums;

namespace StriveCustomer.Android.Adapter
{
    public class VehicleRecyclerHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public IItemClickListener vehicleItemClickListener;
        public TextView vehicleReg;
        public TextView vehicleName;
        public TextView vehicleMembership;
        public ImageButton deleteButton;
        public ImageButton editButton;
        VehicleInfoDisplayFragment InfoFragment = new VehicleInfoDisplayFragment();
        public VehicleRecyclerHolder(View itemVehicle) : base(itemVehicle)
        {
            deleteButton = itemVehicle.FindViewById<ImageButton>(Resource.Id.deleteButton);
            editButton = itemVehicle.FindViewById<ImageButton>(Resource.Id.editButton);
            vehicleReg = itemVehicle.FindViewById<TextView>(Resource.Id.regNumber);
            vehicleName = itemVehicle.FindViewById<TextView>(Resource.Id.carNames);
            vehicleMembership = itemVehicle.FindViewById<TextView>(Resource.Id.vehicleMembership);
            deleteButton.Click += DeleteButton_Click;
            editButton.Click += EditButton_Click;
            itemVehicle.SetOnClickListener(this);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (CustomerVehiclesInformation.vehiclesList.Status.Count > 0)
            {
                var data = CustomerVehiclesInformation.vehiclesList.Status[Position];
                AppCompatActivity activity = (AppCompatActivity)this.ItemView.Context;
                CustomerVehiclesInformation.selectedVehicleInfo = data.VehicleId;
                MembershipDetails.clientVehicleID = data.VehicleId;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, InfoFragment).Commit();
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CustomerInfo.actionType = 1;
            vehicleItemClickListener.OnClick(null, AdapterPosition, false);
        }

        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.vehicleItemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            CustomerInfo.actionType = 2;
            vehicleItemClickListener.OnClick(view, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            return true;
        }
    }

    public class VehicleDetailsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        public VehicleList vehicleLists = new VehicleList();
        private VehicleRecyclerHolder vehicleRecyclerHolder;
        public override int ItemCount
        {
            get
            {
                return vehicleLists.Status.Count;
            }
        }
        public VehicleDetailsAdapter(Context context, VehicleList data)
        {
            this.context = context;
            this.vehicleLists = data;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            vehicleRecyclerHolder = holder as VehicleRecyclerHolder;
            vehicleRecyclerHolder.vehicleReg.Text = vehicleLists.Status[position].Barcode ?? "";
            vehicleRecyclerHolder.vehicleName.Text = vehicleLists.Status[position].VehicleColor +" "+vehicleLists.Status[position].VehicleMfr + " " + vehicleLists.Status[position].VehicleModel ?? "";
            vehicleRecyclerHolder.vehicleMembership.Text = vehicleLists.Status[position].MembershipName;
            vehicleRecyclerHolder.SetItemClickListener(this);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            if(CustomerInfo.actionType == (int)VehicleClickEnums.Delete)
            {
                VehicleInfoViewModel vehicleInfo = new VehicleInfoViewModel();
                var data = CustomerVehiclesInformation.vehiclesList.Status[position];
                var deleted = await vehicleInfo.DeleteCustomerVehicle(data.VehicleId);
                if(deleted)
                {
                    vehicleLists.Status.RemoveAt(position);
                    NotifyItemRemoved(position);
                    NotifyItemRangeChanged(position, vehicleLists.Status.Count);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.VehicleInfoList, parent, false);
            return new VehicleRecyclerHolder(itemView);
        }
    }
}
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
using StriveCustomer.Android.Fragments;
using StriveCustomer.Android.Views;

namespace StriveCustomer.Android.Adapter
{
    public class DetailsRecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView pastDetailsText;
        public IItemClickListener itemClickListener;
        public DetailsRecyclerViewHolder(View pastDetailView) : base(pastDetailView)
        {
            pastDetailsText = pastDetailView.FindViewById<TextView>(Resource.Id.pastDetailsOption);
            pastDetailView.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            itemClickListener.OnClick(view, AdapterPosition, false);
        }
        public bool OnLongClick(View v)
        {
            throw new NotImplementedException();
        }
    }
    public class PastDetailsAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public PastClientServices pastDetailsData = new PastClientServices();
        Context context;
        DashboardView dashboard = new DashboardView();
        int existingID = 0;
        public override int ItemCount
        {
            get
            {
                return pastDetailsData.PastClientDetails.Count;
            }
        }
        public PastDetailsAdapter(PastClientServices pastDetailsData, Context context)
        {
            this.pastDetailsData = pastDetailsData;
            this.context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder pastDetailholder, int position)
        {
            DetailsRecyclerViewHolder detailsViewHolder = pastDetailholder as DetailsRecyclerViewHolder;    
            detailsViewHolder.pastDetailsText.Text = pastDetailsData.PastClientDetails[position].Color + " " + pastDetailsData.PastClientDetails[position].Make + " " + pastDetailsData.PastClientDetails[position].Model;
            detailsViewHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            CustomerInfo.SelectedVehiclePastDetails = pastDetailsData.PastClientDetails[position].VehicleId;
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            PastDetailsInfoFragment pastDetailsInfoFragment = new PastDetailsInfoFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, pastDetailsInfoFragment).Commit();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
                LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
                View itemView = layoutInflater.Inflate(Resource.Layout.PastDetailsList, parent, false);
                return new DetailsRecyclerViewHolder(itemView);
        }
    }


}

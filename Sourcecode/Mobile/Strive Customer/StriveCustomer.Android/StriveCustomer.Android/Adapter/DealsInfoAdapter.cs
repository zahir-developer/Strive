using System;
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
using StriveCustomer.Android.DemoData;

namespace StriveCustomer.Android.Adapter
{
    public class InfoRecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView dealsInfoHeading;
        public TextView dealWashes;
        public TextView dealExpiryDate;
        public TextView dealDescription;
        public IItemClickListener itemClickListener;
        public InfoRecyclerViewHolder(View dealInfoItem) : base(dealInfoItem)
        {
            dealsInfoHeading = dealInfoItem.FindViewById<TextView>(Resource.Id.dealsCardHeading);
            dealWashes = dealInfoItem.FindViewById<TextView>(Resource.Id.leftBottomValue);
            dealExpiryDate = dealInfoItem.FindViewById<TextView>(Resource.Id.rightBottomValue);
            dealDescription = dealInfoItem.FindViewById<TextView>(Resource.Id.dealsCardDescription);
            dealInfoItem.SetOnClickListener(this);
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
    public class DealsInfoAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        DealsDemoData infoData = new DealsDemoData();
        public DealsInfoAdapter(DealsDemoData infoData,Context context)
        {
            this.context = context;
            this.infoData = infoData;
        }

        public override int ItemCount 
        {
            get
            {
                return 1;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder infoHolder, int position)
        {
            InfoRecyclerViewHolder infoRecyclerViewHolder = infoHolder as InfoRecyclerViewHolder;
            infoRecyclerViewHolder.dealsInfoHeading.Text = infoData.DealCost;
            infoRecyclerViewHolder.dealDescription.Text = infoData.Description;
            infoRecyclerViewHolder.dealWashes.Text = infoData.DealWashes;
            infoRecyclerViewHolder.dealExpiryDate.Text = infoData.ExpiryDate;
            infoRecyclerViewHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            //throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.DealsInfoCard, parent, false);
            return new InfoRecyclerViewHolder(itemView);
        }
    }
}
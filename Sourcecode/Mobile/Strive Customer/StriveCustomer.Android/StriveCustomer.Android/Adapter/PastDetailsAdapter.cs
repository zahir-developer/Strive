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
        public List<string> pastDetailsData = new List<string>();
        Context context;
        public override int ItemCount
        {
            get
            {
                return pastDetailsData.Count;
            }
        }
        public PastDetailsAdapter(List<string> pastDetailsData, Context context)
        {
            this.pastDetailsData = pastDetailsData;
            this.context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder pastDetailholder, int position)
        {
            DetailsRecyclerViewHolder detailsViewHolder = pastDetailholder as DetailsRecyclerViewHolder;
            detailsViewHolder.pastDetailsText.Text = pastDetailsData[position];
            detailsViewHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.PastDetailsList, parent, false);
            return new DetailsRecyclerViewHolder(itemView);
        }
    }

 
}
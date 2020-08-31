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
    public class InfoRecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView dealsInfoHeading;
        public IItemClickListener itemClickListener;
        public InfoRecyclerViewHolder(View dealInfoItem) : base(dealInfoItem)
        {
            dealsInfoHeading = dealInfoItem.FindViewById<TextView>(Resource.Id.dealsCardHeading);
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
        public List<string> infoListData = new List<string>();
        public DealsInfoAdapter(List<string> infoListData,Context context)
        {
            this.context = context;
            this.infoListData = infoListData;
        }

        public override int ItemCount => throw new NotImplementedException();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            InfoRecyclerViewHolder infoRecyclerViewHolder = holder as InfoRecyclerViewHolder;
            infoRecyclerViewHolder.dealsInfoHeading.Text = infoListData[position];
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
            return new RecyclerViewHolder(itemView);
        }
    }
}
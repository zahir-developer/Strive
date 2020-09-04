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
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Adapter
{
    public class RecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView dealsText;
        public IItemClickListener itemClickListener;
        public RecyclerViewHolder(View dealItem) : base(dealItem)
        {
            dealsText = dealItem.FindViewById<TextView>(Resource.Id.dealOptionHeading);
            dealItem.SetOnClickListener(this);
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
    public class DealsAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public List<string> listData = new List<string>();
        public Context context;
        public override int ItemCount
        {
            get
            {
                return listData.Count;
            } 
        }
        public DealsAdapter(List<string> listData, Context context)
        {
            this.listData = listData;
            this.context = context;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.dealsText.Text = listData[position];
            recyclerViewHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            DealsInfoFragment dealsInfoFragment = new DealsInfoFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, dealsInfoFragment).Commit();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.DealsList,parent,false);
            return new RecyclerViewHolder(itemView);
        }
    }
}
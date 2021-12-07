using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StriveCustomer.Android.DemoData;
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Adapter
{
    public class RecyclerViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public TextView dealsText;
        public TextView dealsValidity;
        public CheckBox dealCheckBox;
        public IItemClickListener itemClickListener;
        public RecyclerViewHolder(View dealItem) : base(dealItem)
        {
            dealsText = dealItem.FindViewById<TextView>(Resource.Id.dealOptionHeading);
            dealsValidity = dealItem.FindViewById<TextView>(Resource.Id.dealsValidity);
            dealCheckBox = dealItem.FindViewById<CheckBox>(Resource.Id.dealsCheck);
            dealCheckBox.SetOnClickListener(this);
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
            return true;
        }
    }
    public class DealsAdapter : RecyclerView.Adapter, IItemClickListener
    {
        public ObservableCollection<GetAllDeal> dealsData = new ObservableCollection<GetAllDeal>();
        RecyclerViewHolder recyclerViewHolder;
        public Context context;
        private int  match;
        private CheckBox dealCheckBox;
        public override int ItemCount
        {
            get
            {
                return dealsData.Count;
            } 
        }
        public DealsAdapter(ObservableCollection<GetAllDeal> dealsData, Context context)
        {
            this.dealsData = dealsData;
            this.context = context;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.dealsText.Text = dealsData[position].DealName;
            recyclerViewHolder.dealsValidity.Text = "Validity: "+ dealsData[position].StartDate.ToString() +" to "+ dealsData[position].EndDate.ToString();
            match = position;
            checkForSelected();
            recyclerViewHolder.SetItemClickListener(this);
        }

        private void checkForSelected()
        {
            if(CustomerInfo.selectedDeal == match)
            {
                recyclerViewHolder.dealCheckBox.Checked = true;
            }
        }
        public void OnClick(View itemView, int position, bool isLongClick)
        {          
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.DealsList,parent,false);
            return new RecyclerViewHolder(itemView);
        }
    }
}
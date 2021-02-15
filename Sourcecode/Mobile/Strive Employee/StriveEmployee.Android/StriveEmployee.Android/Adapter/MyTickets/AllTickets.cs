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
using StriveEmployee.Android.DemoTicketsData;

namespace StriveEmployee.Android.Adapter.MyTickets
{
    public class AllTicketsViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button DeclineButton;
        public Button CompleteButton;
        public TextView allTickets_TextView;
        public TextView makeModelColor;
        public TextView washService;
        public TextView upcharge;
        public TextView barcode;
        public TextView customer;
        public TextView additionalServices;
        public LinearLayout[] hiddenLayout = new LinearLayout[10];

        public AllTicketsViewHolder(View itemView) : base(itemView)
        {
            makeModelColor = itemView.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
            washService = itemView.FindViewById<TextView>(Resource.Id.washServiceValue_TextView);
            upcharge = itemView.FindViewById<TextView>(Resource.Id.upcharges_TextView);
            barcode = itemView.FindViewById<TextView>(Resource.Id.barcode_TextView);
            customer = itemView.FindViewById<TextView>(Resource.Id.customerValue_TextView);
            additionalServices = itemView.FindViewById<TextView>(Resource.Id.additionalService_TextView);
            DeclineButton = itemView.FindViewById<Button>(Resource.Id.decline_Button);
            CompleteButton = itemView.FindViewById<Button>(Resource.Id.completes_Button);
            allTickets_TextView = itemView.FindViewById<Button>(Resource.Id.allTickets_TextView);
            hiddenLayout[AdapterPosition] = itemView.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
        }

        public void OnClick(View v)
        {
           
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    }
    public class AllTickets : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private List<TicketsModule> ticketData = new List<TicketsModule>();
        AllTicketsViewHolder allTicketsViewHolder;
        public AllTickets(Context context, List<TicketsModule> ticketData)
        {
            this.context = context;
            this.ticketData = ticketData;
        }
       

        //Fill in cound here, currently 0
        public  int Count
        {
            get
            {
                return ticketData.Count;
            }
        }

        public override int ItemCount
        {
            get
            {
                return ticketData.Count;
            }
        
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            allTicketsViewHolder = holder as AllTicketsViewHolder;

            allTicketsViewHolder.makeModelColor.Text = ticketData[position].MakeModelColor;
            allTicketsViewHolder.washService.Text = ticketData[position].WashService;
            allTicketsViewHolder.upcharge.Text = ticketData[position].Upcharge;
            allTicketsViewHolder.barcode.Text = ticketData[position].Barcode;
            allTicketsViewHolder.customer.Text = ticketData[position].Customer;
            allTicketsViewHolder.additionalServices.Text = ticketData[position].AdditionalServices;
            allTicketsViewHolder.allTickets_TextView.Text = "Ticket No:"+" "+ ticketData[position].TicketNumber;
            allTicketsViewHolder.hiddenLayout[position].Visibility = ViewStates.Gone;
            if(position == 0)
            {
                allTicketsViewHolder.allTickets_TextView.Click += AllTickets_TextView_Click0;
            }
            else if(position == 1)
            {
                allTicketsViewHolder.allTickets_TextView.Click += AllTickets_TextView_Click1; ;
            }
            else if(position == 1)
            {
                allTicketsViewHolder.allTickets_TextView.Click += AllTickets_TextView_Click2;
            }
        }

        private void AllTickets_TextView_Click2(object sender, EventArgs e)
        {
            if (allTicketsViewHolder.hiddenLayout[2].Visibility == ViewStates.Gone)
            {
                allTicketsViewHolder.hiddenLayout[2].Visibility = ViewStates.Visible;
            }
            else
            {
                allTicketsViewHolder.hiddenLayout[2].Visibility = ViewStates.Gone;
            }
        }

        private void AllTickets_TextView_Click1(object sender, EventArgs e)
        {
            if(allTicketsViewHolder.hiddenLayout[1].Visibility == ViewStates.Gone)
            {
                allTicketsViewHolder.hiddenLayout[1].Visibility = ViewStates.Visible;
            }
            else
            {
                allTicketsViewHolder.hiddenLayout[1].Visibility = ViewStates.Gone;
            }

            
        }

        private void AllTickets_TextView_Click0(object sender, EventArgs e)
        {
            if (allTicketsViewHolder.hiddenLayout[0].Visibility == ViewStates.Gone)
            {
                allTicketsViewHolder.hiddenLayout[0].Visibility = ViewStates.Visible;
            }
            else
            {
                allTicketsViewHolder.hiddenLayout[0].Visibility = ViewStates.Gone;
            }
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
           
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.AllTickets_ItemView, parent, false);
            return new AllTicketsViewHolder(itemView);
        }
    }
}
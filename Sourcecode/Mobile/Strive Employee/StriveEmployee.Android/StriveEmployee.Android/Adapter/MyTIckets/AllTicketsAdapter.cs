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
using Strive.Core.Models.Employee.MyTickets;

namespace StriveEmployee.Android.Adapter.MyTIckets
{
    public class AllTicketsViewHolder : RecyclerView.ViewHolder
    {
        public Button decline_Button;
        public Button complete_Button;
        public TextView ticket_TextView;
        public TextView ticketNumber_TextView;
        public TextView makeModelColorValue_TextView;
        public TextView washServiceValue_TextView;
        public TextView upchargeValue_TextView;
        public TextView barcodeValue_TextView;
        public TextView customerValue_TextView;
        public TextView additionalServicesValue_TextView;
        public LinearLayout details_LinearLayout;

        public AllTicketsViewHolder(View itemView) : base(itemView)
        {
            decline_Button = itemView.FindViewById<Button>(Resource.Id.decline_Button);
            complete_Button = itemView.FindViewById<Button>(Resource.Id.complete_Button);
            ticket_TextView = itemView.FindViewById<TextView>(Resource.Id.ticket_TextView);
            ticketNumber_TextView = itemView.FindViewById<TextView>(Resource.Id.ticketNumber_TextView);
            makeModelColorValue_TextView = itemView.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
            washServiceValue_TextView = itemView.FindViewById<TextView>(Resource.Id.washServiceValue_TextView);
            upchargeValue_TextView = itemView.FindViewById<TextView>(Resource.Id.upchargeValue_TextView);
            barcodeValue_TextView = itemView.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
            customerValue_TextView = itemView.FindViewById<TextView>(Resource.Id.customerValue_TextView);
            additionalServicesValue_TextView = itemView.FindViewById<TextView>(Resource.Id.additionalServicesValue_TextView);
            details_LinearLayout = itemView.FindViewById<LinearLayout>(Resource.Id.details_LinearLayout);
        }
    }
    public class AllTicketsAdapter : RecyclerView.Adapter
    {

        Context context;
        private AllTicketsViewHolder allTicketsViewHolder;
        private AllTickets allTickets = new AllTickets();

        public AllTicketsAdapter(Context context, AllTickets allTickets)
        {
            this.context = context;
            this.allTickets = allTickets;
        }

        public override int ItemCount
        {
            get
            {
                return allTickets.Washes.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            allTicketsViewHolder = holder as AllTicketsViewHolder;
            allTicketsViewHolder.ticket_TextView.PaintFlags = PaintFlags.UnderlineText;
            allTicketsViewHolder.ticket_TextView.Click += Ticket_TextView_Click;
            allTicketsViewHolder.ticketNumber_TextView.Text = allTickets.Washes[position].TicketNumber;
            allTicketsViewHolder.makeModelColorValue_TextView.Text = allTickets.Washes[position].VehicleName;
            allTicketsViewHolder.washServiceValue_TextView.Text = allTickets.Washes[position].ServiceName;
            allTicketsViewHolder.upchargeValue_TextView.Text = " ";
            allTicketsViewHolder.barcodeValue_TextView.Text = " " ;
            allTicketsViewHolder.customerValue_TextView.Text = allTickets.Washes[position].ClientName;
            allTicketsViewHolder.additionalServicesValue_TextView.Text = " ";
            allTicketsViewHolder.details_LinearLayout.Visibility = ViewStates.Gone;
        }

        private void Ticket_TextView_Click(object sender, EventArgs e)
        {
            if(allTicketsViewHolder.details_LinearLayout.Visibility == ViewStates.Visible)
            {
                allTicketsViewHolder.details_LinearLayout.Visibility = ViewStates.Gone;
            }
            else
            {
                allTicketsViewHolder.details_LinearLayout.Visibility = ViewStates.Visible;
            }
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.TicketDetails_CardView, parent, false);
            return new AllTicketsViewHolder(itemView);
        }
    }
}
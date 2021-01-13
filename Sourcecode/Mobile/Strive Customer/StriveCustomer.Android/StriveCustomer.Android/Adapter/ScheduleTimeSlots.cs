using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Customer.Schedule;

namespace StriveCustomer.Android.Adapter
{
    public class ScheduleTimeSlots : BaseAdapter
    {

        Context context;
        int slotsAvailable { get; set; }
        Button slotSelection;
        AvailableScheduleSlots availableScheduleSlots = new AvailableScheduleSlots();

        public ScheduleTimeSlots(Context context, AvailableScheduleSlots slotsAvailable)
        {
            this.context = context;
            availableScheduleSlots.GetTimeInDetails = new List<GetTimeInDetails>();
            availableScheduleSlots = slotsAvailable;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
           

            if (convertView == null)
            {
                slotSelection = new Button(context);
                slotSelection.LayoutParameters = new GridView.LayoutParams(100, 85);
                slotSelection.SetPadding(8, 8, 8, 8);
            }
            else
            {
                slotSelection = (Button)convertView;
                
            }
            slotSelection.Click += SlotSelection_Click;
            slotSelection.SetBackgroundColor(Color.Gray);
            slotSelection.Text = availableScheduleSlots.GetTimeInDetails[position].TimeIn;

            return slotSelection;
        }

        private void SlotSelection_Click(object sender, EventArgs e)
        {
            slotSelection = sender as Button;
            foreach(var data in availableScheduleSlots.GetTimeInDetails)
            {
                if (string.Equals(slotSelection.Text,data.TimeIn))
                {
                    slotSelection.SetBackgroundColor(Color.Gray);
                }
                else
                {
                    slotSelection.SetBackgroundColor(Color.DarkCyan);
                }
            }
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return availableScheduleSlots.GetTimeInDetails.Count;
            }
        }

    }

    class ScheduleTimeSlotsViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}
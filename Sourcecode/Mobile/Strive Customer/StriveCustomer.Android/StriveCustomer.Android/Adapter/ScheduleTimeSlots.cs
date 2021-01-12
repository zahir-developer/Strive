using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.Adapter
{
    public class ScheduleTimeSlots : BaseAdapter
    {

        Context context;
        int slotsAvailable { get; set; }

        public ScheduleTimeSlots(Context context, int slotsAvailable)
        {
            this.context = context;
            slotsAvailable = slotsAvailable;
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
            Button slotSelection;
           

            if (convertView == null)
            {
                slotSelection = new Button(context);
                slotSelection.LayoutParameters = new GridView.LayoutParams(85, 85);
                slotSelection.SetPadding(8, 8, 8, 8);
            }
            else
            {
                slotSelection = (Button)convertView;
            }
            slotSelection.Text = "Button"+ position;

            return slotSelection;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return 20;
            }
        }

    }

    class ScheduleTimeSlotsViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}
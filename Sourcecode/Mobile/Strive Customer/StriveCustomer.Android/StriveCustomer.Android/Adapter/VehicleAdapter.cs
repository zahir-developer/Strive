using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Adapter
{
    public class VehicleAdapter<T> : ArrayAdapter<String>
    {
        private Context mContext;
        public VehicleAdapter(Context context, int textViewResourceId, List<String> makes )
            : base(context, textViewResourceId, makes)
        {
            this.mContext = context;
        }

       

        public override bool IsEnabled(int position)
        {

            // Disable the first item from Spinner
            // First item will be use for hint
            return position == 0 ? false : true;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            View v;
            //View view = base.GetDropDownView(position, convertView, parent);

            //TextView tv = (TextView)view;
            //if (position == 0)
            //{
            //    // Set the hint text color gray
            //    tv.SetTextColor(Color.Gray);
            //}
            //else
            //{
            //    tv.SetTextColor(Color.Black);
            //}
            //return view;
            if (position == 0)
            {
                TextView tv = new TextView(mContext);
                tv.Visibility = ViewStates.Gone;
                tv.SetHeight(0);
                v = tv;
                v.Visibility = ViewStates.Gone;
            }
            else
                v = base.GetDropDownView(position, null, parent);
            return v;
        }
        public override int Count => (base.Count);


    }
}

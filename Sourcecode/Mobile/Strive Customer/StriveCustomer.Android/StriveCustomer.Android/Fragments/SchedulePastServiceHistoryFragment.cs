using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class SchedulePastServiceHistoryFragment : MvxFragment<ScheduleViewModel>
    {
        private LinearLayout PastServiceList_LinearLayout;
        private View layout;
        private View[] moreInfo;
        int a = 0;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.ServiceHistoryFragment, null);
            PastServiceList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);
            moreInfo = new View[10];
            for (int i = 0; i <=3; i++)
            {
                a = i;
                layout = LayoutInflater.From(Context).Inflate(Resource.Layout.ServiceHistoryItemView, PastServiceList_LinearLayout, false);
                moreInfo[i] = layout.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
                moreInfo[i].Visibility = ViewStates.Gone;
                var ticketNumber = layout.FindViewById<TextView>(Resource.Id.scheduleTicket_TextView);
                ticketNumber.PaintFlags = PaintFlags.UnderlineText;
                ticketNumber.Click += TicketNumber_Click;
                PastServiceList_LinearLayout.AddView(layout);
            }

            return rootview;
        }

        private void TicketNumber_Click(object sender, EventArgs e)
        {
            
            moreInfo[a].Visibility = ViewStates.Visible;
        }
    }
}
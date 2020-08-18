using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.Views
{
    public class NotificationSettingsView : Dialog
    {
        public NotificationSettingsView(Context context) : base(context)
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.NotificationSettingsDialog);
        }
    }
}
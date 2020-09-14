using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.Services
{
    public class GeoFencingTransitionService : IntentService
    {
        private string geoFencingError;
        public GeoFencingTransitionService(string name) : base(name)
        {

        }

        protected override void OnHandleIntent(Intent intent)
        {
            GeofencingEvent geofencingEvent = GeofencingEvent.FromIntent(intent);

            if(geofencingEvent.HasError)
            {

            }
        }
    }
}
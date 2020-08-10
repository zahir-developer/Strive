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
    [BroadcastReceiver]
    public class GeofenceBroadcastReceiver : BroadcastReceiver
    {
        IList<IGeofence> geofencingTriggers;
        GeofencingEvent geofencingEvent;
        int TransitionType;
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
            geofencingEvent = GeofencingEvent.FromIntent(intent);

            if (geofencingEvent.HasError)
            {

            }
            geofencingTriggers = geofencingEvent.TriggeringGeofences;

            TransitionType = geofencingEvent.GeofenceTransition;

            switch (TransitionType)
            {
                case Geofence.GeofenceTransitionEnter:
                    Toast.MakeText(context, "Entered", ToastLength.Short).Show();
                    break;

                case Geofence.GeofenceTransitionExit:
                    Toast.MakeText(context, "Exited", ToastLength.Short).Show();
                    break;
            }
        }
    }
}
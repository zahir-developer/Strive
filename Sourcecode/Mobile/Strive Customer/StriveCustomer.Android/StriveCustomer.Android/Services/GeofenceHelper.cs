using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.Services
{
    public class GeofenceHelper : ContextWrapper
    {
        PendingIntent pendingIntent;
        public GeofenceHelper(Context @base) : base(@base)
        {
        }

        protected GeofenceHelper(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public GeofencingRequest GetGeofencingRequest(IGeofence geofence)
        {
            return new GeofencingRequest.Builder()
                        .AddGeofence(geofence)
                        .SetInitialTrigger(Geofence.GeofenceTransitionEnter)
                        .Build();
        }
        public GeofencingRequest GetGeofencingRequests(IList<IGeofence> geofences)
        {
            return new GeofencingRequest.Builder()
                        .AddGeofences(geofences)
                        .SetInitialTrigger(Geofence.GeofenceTransitionEnter)
                        .Build();
        }
        public IGeofence getGeofence(String ID, LatLng latlng,float Radius,int TransititonTypes )
        {
            return new GeofenceBuilder()
                        .SetCircularRegion(latlng.Latitude, latlng.Longitude, Radius)
                        .SetRequestId(ID)
                        .SetTransitionTypes(TransititonTypes)
                        .SetLoiteringDelay(5000)
                        .SetExpirationDuration(Geofence.NeverExpire)
                        .Build();
        }
        public PendingIntent getPendingIntent()
        {
            if(pendingIntent != null)
            {
                return pendingIntent;
            }
            Intent intent = new Intent(this,typeof(GeofenceBroadcastReceiver));
            pendingIntent = PendingIntent.GetBroadcast(this,2607,intent,PendingIntentFlags.UpdateCurrent);  
            return pendingIntent;
        }
    }
}
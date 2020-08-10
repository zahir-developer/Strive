using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Uri = Android.Net.Uri;
using Android.Support.V4.App;
using Android.Graphics;
using StriveCustomer.Android.Fragments;

namespace StriveCustomer.Android.Services
{
    [BroadcastReceiver]
    public class GeofenceBroadcastReceiver : BroadcastReceiver
    {
        private IList<IGeofence> geofencingTriggers;
        private Location triggeringLocation;
        NotificationManager notificationManager;
        private GeofencingEvent geofencingEvent;
        private Intent mapIntent;
        private PendingIntent openGoogleMapIntent;
        private int TransitionType;
        Uri googleMapUri;
        private static string CHANNEL_NAME = "geofence_Notification_Channel";
        private string CHANNEL_ID = "" + CHANNEL_NAME;
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
            geofencingEvent = GeofencingEvent.FromIntent(intent);

            if (geofencingEvent.HasError)
            {

            }
            geofencingTriggers = geofencingEvent.TriggeringGeofences;
            TransitionType = geofencingEvent.GeofenceTransition;
            triggeringLocation = geofencingEvent.TriggeringLocation;

            switch (TransitionType)
            {
                case Geofence.GeofenceTransitionEnter:
                    Toast.MakeText(context, "Entered", ToastLength.Short).Show();
                    break;

                case Geofence.GeofenceTransitionExit:
                    Toast.MakeText(context, "Exited", ToastLength.Short).Show();
                    break;
            }
            setUpNotification(context);
            buildSendNotification(mapIntent,context);
        }

         void setUpNotification(Context context)
        {
            googleMapUri = Uri.Parse("geo:" + triggeringLocation.Latitude + "," + triggeringLocation.Longitude + "?q=" + triggeringLocation.Latitude + "," + triggeringLocation.Longitude);
            mapIntent = new Intent(Intent.ActionView, googleMapUri);
            mapIntent.SetPackage("com.google.android.apps.maps");
            mapIntent.SetFlags(ActivityFlags.NewTask);
            
            NotificationChannel notificationChannel = new NotificationChannel(CHANNEL_ID,CHANNEL_NAME,NotificationImportance.High);
            notificationChannel.EnableLights(true);
            notificationChannel.EnableVibration(true);
            notificationChannel.Description = "channel to trigger geofence notification";
            notificationChannel.LockscreenVisibility = NotificationVisibility.Public;
            notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);
        }      
        void buildSendNotification(Intent mapIntent,Context context) 
        {
            Intent intent = new Intent(context,typeof(MapsFragment));
            PendingIntent pendingIntent = PendingIntent.GetActivity(context,267,intent,PendingIntentFlags.UpdateCurrent);
            NotificationCompat.Builder builder = (NotificationCompat.Builder)new NotificationCompat.Builder(context)
                                          .SetSmallIcon(Resource.Drawable.world_location)
                                          .SetPriority(NotificationCompat.PriorityHigh)
                                          .SetContentTitle("Car wash near by")
                                          .SetAutoCancel(true)
                                          .SetContentIntent(pendingIntent)
                                          .SetChannelId(CHANNEL_ID);
            notificationManager.Notify(267,builder.Build());
        }
    }
}
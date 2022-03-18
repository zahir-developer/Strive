using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using Strive.Core.Utils.Employee;
using StriveEmployee.Android.NotificationConstants;
using StriveEmployee.Android.Views;

namespace StriveEmployee.Android.FCMService
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;
        public AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;
        private Intent intent;
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            if(message.GetNotification() != null || message.Data.Count > 0)
            {
                var body = message.GetNotification().Body;
                SendNotification(body, message.Data);
            }
            //
            //else if(message.Data.Count > 0)
            //{
            //    Log.Debug(TAG, "Notification Message Body: " + message.Data);
            //    var body = message.Data["priority"];
            //    SendNotification(body, message.Data);
            //}
        }
        public override void OnNewToken(string s)
        {
            base.OnNewToken(s);
            SaveRegistrationToken(s);
        }

        private void SaveRegistrationToken(string token)
        {
            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            preferenceEditor = sharedPreferences.Edit();
            preferenceEditor.PutString("RefreshToken", token);
            preferenceEditor.Apply();
        }
      
        private void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            EmployeeTempData.EmployeeRole = int.Parse(data["RoleId"]);
            if (EmployeeTempData.EmployeeID == 0)
            {

              intent = new Intent(this, typeof(LoginView));
            }
            else
            {
              intent = new Intent(this, typeof(DashboardView));
            }
            intent.PutExtra("IsFromNotification", true);
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this,
                                                          Constants.NOTIFICATION_ID,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, Constants.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.ic_successstatus)
                                      .SetContentTitle("FCM Message")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(Constants.NOTIFICATION_ID, notificationBuilder.Build());
        }
       
    }
}
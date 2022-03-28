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
        private Intent intent;
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            if( message.Data.Count > 0)
            {
                //var body = message.GetNotification().Body;
                Log.Info(TAG, "this is an fcm message");
                SendNotification(message.Data);
            }
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
      
        private void SendNotification(IDictionary<string, string> data)
        {
            string messageBody = data["body"];
            string title = data["title"];
            EmployeeTempData.EmployeeRole = int.Parse(data["RoleId"]);
            if (Constants.NOTIFICATION_EMPID == 0)
            {
                Log.Info(TAG, "calling LoginView");
                intent = new Intent(this, typeof(SplashScreen));
            }
            else
            {
                Log.Info(TAG, "calling DashboardView");
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
                                      .SetContentTitle(title)
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);
            var notification = notificationBuilder.Build();
            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(Constants.NOTIFICATION_ID, notification);
        }
       
    }
}
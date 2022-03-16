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
            if(message.GetNotification() != null)
            {
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                var body = message.GetNotification().Body;
                SendNotification(body, message.Data);
            }else if(message.Data.Count > 0)
            {
                Log.Debug(TAG, "Notification Message Body: " + message.Data);
                var body = message.Data["priority"];
                SendNotification(body, message.Data);
            }
        }
        public override void OnNewToken(string s)
        {
            base.OnNewToken(s);
            sendRegistrationToServer(s);
        }

        private void sendRegistrationToServer(string token)
        {
            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            preferenceEditor = sharedPreferences.Edit();
            preferenceEditor.PutString("RefreshToken", token);
            preferenceEditor.Apply();
        }

        private void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            
            //sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            var keys = data.Keys;
            foreach (var key in keys)
            {
                Console.WriteLine(key);
                Console.WriteLine(data[key]);

            }
            EmployeeTempData.EmployeeRole = int.Parse(data["RoleId"]);
            EmployeeTempData.FromNotification = true;
          
            //foreach (var key in data.Keys)
            //{
            //    intent.PutExtra(key, data[key]);
            //}

            Builder = new AlertDialog.Builder(ApplicationContext);
            Builder.SetMessage("Checklist Reminder");
            Builder.SetTitle(data["Name"]);

            okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {
                intent.PutExtra("IsFromNotification", EmployeeTempData.FromNotification);
                intent.AddFlags(ActivityFlags.ClearTop);
                if (messageBody.Contains("false"))
                {
                 
                    intent = new Intent(this, typeof(LoginView));
                }
                else
                {
                    intent = new Intent(this, typeof(DashboardView));
                }

            });
            Builder.SetPositiveButton("Ok", okHandler);
            Builder.Create();

            Builder.Show();
            
            
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
        private void OpenCheckListReminder()
        {
           
        }
    }
}
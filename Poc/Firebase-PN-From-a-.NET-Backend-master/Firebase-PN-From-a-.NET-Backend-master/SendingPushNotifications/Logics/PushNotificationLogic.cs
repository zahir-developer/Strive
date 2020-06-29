using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SendingPushNotifications.Logics
{
    public static class PushNotificationLogic
    {
        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private static string ServerKey = "AAAAZ3eK2wA:APA91bH2lyPLbiMCholcWd6uvvk-pLZtXhPudCYuaseVC_7xx3_v0Ed6VFf5IjgfbG2Vlp8UUHN6ma8mKVXqsL4A2p_fq1BM7_oFncuE6LO9XX5gRvzD6iUvEH58hALsbQqEItxpOQxE";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceTokens">List of all devices assigned to a user</param>
        /// <param name="title">Title of notification</param>
        /// <param name="body">Description of notification</param>
        /// <param name="data">Object with all extra information you want to send hidden in the notification</param>
        /// <returns></returns>
        public static async Task SendPushNotification(string[] deviceTokens, string title, string body, object data)
        {
    
            WebRequest tRequest = WebRequest.Create(FireBasePushNotificationsURL);
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ServerKey));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", "444387220224"));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "d-kgOi3xLkGIhKbPUf0QE2:APA91bFP2J3tLNyxgm6UiVG2VFjsFQVNa8fvvfT0NlhN0Of-Cf7cwHBrI3a6by0eUtsKFg-AzXy_WWcD6WwE07XT8JVkccTnhj12t9UDwNuRayG-aZjVbg-rzHNPSSJ-BL8-w9C8L1aE",
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = "Your Car is ready to be picked by you",
                    title = "your car is Ready",
                    badge = 0
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }

            };

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }

    }
}

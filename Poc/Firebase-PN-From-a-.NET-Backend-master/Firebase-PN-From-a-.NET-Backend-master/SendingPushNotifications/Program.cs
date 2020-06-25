using SendingPushNotifications.Logics;
using System;

namespace SendingPushNotifications
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = "";
            string body = "";
            var data = new { action = "Play", userId = 5 };

            Console.WriteLine("Hello Everyone!");
            Console.WriteLine("Let's send push notifications!!!");

            Console.Write("Title of Notification: ");
            title = "Notification";

            Console.WriteLine();

            Console.WriteLine("Ok, so now I have the title, I'll need a description");
            body = "This is a notification from the Backend";

            Console.WriteLine();

            Console.Write("How many devices are going to receive this notification? ");
            int devicesCount = 1;
            var tokens = new string[devicesCount];

            Console.WriteLine();

            for (int i = 0; i < devicesCount; i++)
            {
                Console.Write($"Token for device number {i + 1}: ");
                tokens[i] = "d-kgOi3xLkGIhKbPUf0QE2:APA91bFP2J3tLNyxgm6UiVG2VFjsFQVNa8fvvfT0NlhN0Of-Cf7cwHBrI3a6by0eUtsKFg-AzXy_WWcD6WwE07XT8JVkccTnhj12t9UDwNuRayG-aZjVbg-rzHNPSSJ-BL8-w9C8L1aE";
                Console.WriteLine();
            }

          
                var pushSent = PushNotificationLogic.SendPushNotification(tokens, title, body, data);
                Console.WriteLine($"Notification sent");
        }
    }
}

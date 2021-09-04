using System;
using Xamarin.Essentials;

namespace Greeter.Common
{
    public class Logic
    {
        public static string FullName(string firstName, string lastName) => $"{firstName} {lastName}";

        public static void Vibrate(double seconds)
        {
            var duration = TimeSpan.FromSeconds(seconds);
            Vibration.Vibrate(duration);
        }
    }
}

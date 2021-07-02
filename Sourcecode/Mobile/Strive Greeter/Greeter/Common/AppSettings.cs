using Xamarin.Essentials;

namespace Greeter.Common
{
    // App Level Storage (Local Storage)
    public static class AppSettings
    {
        public static bool IsLogin => !string.IsNullOrEmpty(Token);

        public static string BearereToken => "Bearer " + Token;

        public static string Token
        {
            get => Preferences.Get(nameof(Token), string.Empty);
            set => Preferences.Set(nameof(Token), value);
        }

        public static int LocationID
        {
            get => Preferences.Get(nameof(LocationID), 0);
            set => Preferences.Set(nameof(LocationID), value);
        }


        public static void Clear() => Preferences.Clear();
    }
}
using Greeter.Services.Api;
using Xamarin.Essentials;

namespace Greeter.Common
{
    // App Level Storage 
    public static class AppSettings
    {
        public static bool IsLogin => !string.IsNullOrEmpty(Token);

        public static bool IsHavingLocation => LocationID != 0;

        public static string BearereToken => "Bearer " + Token;

        // App Local Storage Details
        public static string Token
        {
            get => Preferences.Get(nameof(Token), string.Empty);
            set => Preferences.Set(nameof(Token), value);
        }

        public static string RefreshToken
        {
            get => Preferences.Get(nameof(RefreshToken), string.Empty);
            set => Preferences.Set(nameof(RefreshToken), value);
        }

        public static int LocationID
        {
            get => Preferences.Get(nameof(LocationID), 0);
            set => Preferences.Set(nameof(LocationID), value);
        }

        public static int WashTime
        {
            get => Preferences.Get(nameof(WashTime), 0);
            set => Preferences.Set(nameof(WashTime), value);
        }

        public static long UserID
        {
            get => Preferences.Get(nameof(UserID), 0);
            set => Preferences.Set(nameof(UserID), value);
        }

        public static void Clear() => Preferences.Clear();

        // App Level Instanaces
        // App Level Injection not dependent on UIViewController
        private static IApiService iApiService = null;
        public static IApiService ApiService
        {
            get => iApiService == null ? new ApiService() : iApiService;
        }
    }
}
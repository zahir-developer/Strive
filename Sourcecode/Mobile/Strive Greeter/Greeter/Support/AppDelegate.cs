using Foundation;
using Greeter.MvvmCross;
using MvvmCross.Platforms.Ios.Core;
using UIKit;

namespace Greeter
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {

        [Export("window")]
        public UIWindow Window { get; set; }

        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            SetApperance();

            //if (!UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            //{
            //    Window = new UIWindow(UIScreen.MainScreen.Bounds);
            //    INetworkService networkService = new NetworkService();
            //    IAuthenticationService authenticationService = new AuthenticationService(networkService);

            //    var viewController = new LoginViewController(authenticationService);
            //    Window.RootViewController = viewController;
            //    Window.MakeKeyAndVisible();
            //}

            return true;
        }

        // UISceneSession Lifecycle

        [Export("application:configurationForConnectingSceneSession:options:")]
        public UISceneConfiguration GetConfiguration(UIApplication application, UISceneSession connectingSceneSession, UISceneConnectionOptions options)
        {
            // Called when a new scene session is being created.
            // Use this method to select a configuration to create the new scene with.
            return UISceneConfiguration.Create("Default Configuration", connectingSceneSession.Role);
        }

        [Export("application:didDiscardSceneSessions:")]
        public void DidDiscardSceneSessions(UIApplication application, NSSet<UISceneSession> sceneSessions)
        {
            // Called when the user discards a scene session.
            // If any sessions were discarded while the application was not running, this will be called shortly after `FinishedLaunching`.
            // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
        }

        void SetApperance()
        {
            var titleAttribute = new UITextAttributes
            {
                Font = UIFont.SystemFontOfSize(20, UIFontWeight.Bold),
                TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f)
            };
            UINavigationBar.Appearance.SetTitleTextAttributes(titleAttribute);

            //var barButtonTitleTextAttribute = new UITextAttributes
            //{
            //    Font = UIFont.SystemFontOfSize(17),
            //    TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f)
            //};

            //UIBarButtonItem.Appearance.SetTitleTextAttributes(barButtonTitleTextAttribute, UIControlState.Normal);
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(barButtonTitleTextAttribute, UIControlState.Highlighted);
            //UIBarButtonItem.Appearance.SetTitleTextAttributes(barButtonTitleTextAttribute, UIControlState.Focused);

            //var barButtonUnselectedTitleTextAttribute = new UITextAttributes
            //{
            //    Font = UIFont.SystemFontOfSize(10),
            //    TextColor = UIColor.White
            //};
            //UITabBarItem.Appearance.SetTitleTextAttributes(barButtonUnselectedTitleTextAttribute, UIControlState.Normal);

            //var barButtonSelectedTitleTextAttribute = new UITextAttributes
            //{
            //    Font = UIFont.SystemFontOfSize(10),
            //    TextColor = UIColor.Green
            //};
            //UITabBarItem.Appearance.SetTitleTextAttributes(barButtonSelectedTitleTextAttribute, UIControlState.Selected);

            UITabBar.Appearance.UnselectedItemTintColor = UIColor.White;
            UITabBar.Appearance.TintColor = UIColor.FromRGB(12, 78, 71);

            //UITabBar.Appearance.TintColor = UIColor.FromName(ColorNames.APP_COLOR);
        }
    }
}


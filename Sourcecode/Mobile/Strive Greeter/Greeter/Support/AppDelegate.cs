using Foundation;
using Greeter.Common;
using Greeter.Storyboards;
using InfineaSDK.iOS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using UIKit;
using Xamarin.Essentials;

namespace Greeter
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIResponder, IUIApplicationDelegate
    {
        [Export("window")]
        public UIWindow Window { get; set; }

        // App Level Services
        //public IAuthenticationService AuthenticationService = new AuthenticationService();


        [Export("application:didFinishLaunchingWithOptions:")]
        public bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method

            AppCenter.Start(Constants.APP_CENTER_SECTRET_KEY, typeof(Crashes));
            IPCIQ.Instance.SetDeveloperKey(Constants.INFEA_DEVELOPER_KEY);

            SetApperance();

            if (!UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                Window = new UIWindow(UIScreen.MainScreen.Bounds);
                //INetworkService networkService = new NetworkService();
                //IAuthenticationService authenticationService = new AuthenticationService(networkService);
                //var viewController = new LoginViewController(authenticationService);

                var sb = UIStoryboard.FromName(StoryBoardNames.USER, null);
                UIViewController vc;

                if (!AppSettings.IsLogin)
                    vc = sb.InstantiateViewController(nameof(LoginViewController));
                else if (AppSettings.LocationID == 0)
                    vc = sb.InstantiateViewController(nameof(LocationViewController));
                else
                {
                    sb = UIStoryboard.FromName(StoryBoardNames.HOME, null);
                    vc = sb.InstantiateViewController(nameof(TabViewController));
                }

                var nc = new UINavigationController();
                var vcs = new UIViewController[] { vc };
                nc.ViewControllers = vcs;
                Window.RootViewController = nc;
                Window.TintColor = Common.Colors.APP_BASE_COLOR.ToPlatformColor();
                Window.MakeKeyAndVisible();
            }

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
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);

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

            if(UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                var tabBarAppearance = new UITabBarAppearance();
                tabBarAppearance.ConfigureWithDefaultBackground();
                tabBarAppearance.BackgroundColor = UIColor.White;
                UITabBar.Appearance.StandardAppearance = tabBarAppearance;
                //UITabBar.Appearance.ScrollEdgeAppearance = tabBarAppearance;

                var navigationBarAppearance = new UINavigationBarAppearance();
                navigationBarAppearance.ConfigureWithDefaultBackground();
                navigationBarAppearance.BackgroundColor = UIColor.White;
                UINavigationBar.Appearance.StandardAppearance = navigationBarAppearance;
                UINavigationBar.Appearance.ScrollEdgeAppearance = navigationBarAppearance;
            }

            //UITabBar.Appearance.TintColor = UIColor.FromName(ColorNames.APP_COLOR);
        }
    }
}


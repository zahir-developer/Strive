using System;
using Strive.Core.ViewModels.Customer;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using CoreGraphics;
using CoreLocation;
using MapKit;
using System.Text.RegularExpressions;
using StriveCustomer.iOS.UIUtils;
using WebKit;
using System.Collections.Generic;
using Strive.Core.Models.TimInventory;
using CarWashLocation = Strive.Core.Models.Customer.Locations;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Employee.Documents;
using System.Linq;
using Xamarin.Essentials;
using GoogleMaps.LocationServices;
using System.Threading.Tasks;
using PdfKit;

namespace StriveCustomer.iOS.Views
{
    public partial class HomeView : MvxViewController<MapViewModel>, IMKMapViewDelegate
    {
        CLLocationManager locationManager = new CLLocationManager();
        //public CarWashLocation carWashLocations = new CarWashLocation();
        public washLocations carWashLocations = new washLocations();
        //public static CarWashLocation washlocations;
        public static washLocations washlocations;
        public List<Double> distanceList = new List<double>();
        Dictionary<int, double> dict = new Dictionary<int, double>();
        NSString urlString;
        



        public HomeView() : base("HomeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //SetTerm();
            InitialSetup();
            ScheduleSetMap();
           
            CustomerInfo.setMapInfo();
            WashTimeWebView.MapType = MKMapType.Hybrid;
            WashTimeWebView.WeakDelegate = this;
            WashTimeWebView.ZoomEnabled = true;
            WashTimeWebView.ScrollEnabled = true;
            

            locationManager.RequestWhenInUseAuthorization();            

            WashTimeWebView.Register(typeof(WashStationAnnotationView), MKMapViewDefault.AnnotationViewReuseIdentifier);

            //this.mapDelegate = new MapDelegate();
            //this.WashTimeWebView.Delegate = this.mapDelegate;

            //var geofenceRegioncenter = new CLLocationCoordinate2D(8.185458, 77.401112);
            //var geofenceRegion = new CLCircularRegion(geofenceRegioncenter, 100, "notifymeonExit");
            //geofenceRegion.NotifyOnEntry = true;
            //geofenceRegion.NotifyOnExit = true;
            //locationManager.StartMonitoring(geofenceRegion);
            //locationManager.Delegate = new MyLocationDelegate(WashTimeWebView);
        }
        async void SetTerm()
        {
            TermsDocument term =await  ViewModel.Terms();
            var FilePath = term.Document.Document.FilePath;
            //string PdfPath = "https://strivestorageaccount.blob.core.windows.net/strivedev" + FilePath ;
            string PdfPath = "http://developer.apple.com/iphone/library/documentation/UIKit/Reference/UIWebView_Class/UIWebView_Class.pdf";
            //var base64String = term.Document.Document.Base64;
            //var data = new NSData(base64String, options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
            //TermsDocuments.LoadData(data, mimeType: "application/pdf","", baseUrl: NSUrl.FromString("https://www.google.com"));

            //NSUrl url = new NSUrl(new Uri(DefaultPath).AbsoluteUri);
            //NSUrlRequest nSUrlRequest = new NSUrlRequest(url);
            //TermsDocuments.LoadRequest(nSUrlRequest);
            //WKWebView _wkWebView = new WKWebView(TermsDocuments.Frame, new WKWebViewConfiguration());

            //_wkWebView.LoadFileUrl(url, url);
            //_wkWebView.ContentMode = UIViewContentMode.ScaleToFill;
            //_wkWebView.BackgroundColor = UIColor.Clear;
            //_wkWebView.Opaque = false;
            //_wkWebView.ScrollView.BackgroundColor = UIColor.Clear;

            ////_wkWebView.DrawViewHierarchy(_wkWebView.Bounds, true);
            //TermsDocuments.AddSubview(_wkWebView);
            PdfView pdfView = new PdfView();
            pdfView.TranslatesAutoresizingMaskIntoConstraints = false;
            TermsDocuments.AddSubview(pdfView);
            PdfDocument document;
            using (urlString = new NSString(PdfPath))

            using (var tempUrl = NSUrl.CreateFileUrl(new string[] { urlString}))
            document = new PdfDocument(tempUrl);


            //if var document = PdfDocument(url: path) {
            pdfView.Document = document;

        }
       
        public async void ScheduleSetMap()
        {
            int seconds = 60000;
            while (seconds != 0)
            {
                await Task.Delay(seconds);
                SetMaps();
                //Console.WriteLine("refreshing home");

            }
        }
        public override void ViewDidAppear(bool animated)
        {            
            SetMaps();
        }
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
        private void InitialSetup()
        {
            
            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
                var plist = NSUserDefaults.StandardUserDefaults;
                var First = plist.BoolForKey("first");
                plist.SetBool(true, "first");
            };
        }
        private async void SetMaps()
        {
            //var locations = await ViewModel.GetAllLocationsCommand();
            var locations = await ViewModel.GetAllLocationStatus();            

            if (locations.Washes.Count == 0)
            {
                carWashLocations = null;
                washlocations = null;
            }
            else
            {
                carWashLocations = locations;
                washlocations = locations;
            }
            isLocationEnabled();            
        }
        //private void SetMapAnnotations()
        //{
        //    double LatCenter = 0.0;
        //    double LongCenter = 0.0;
        //    int AddressCount = 0;
            
        //    MKPointAnnotation[] annotations = new MKPointAnnotation[carWashLocations.Location.Count];
            
        //    for (int i = 0; i < carWashLocations.Location.Count; i++)           
        //    {
        //        var subtitle = "";                
        //        LatCenter += (double)carWashLocations.Location[i].Latitude;
        //        LongCenter += (double)carWashLocations.Location[i].Longitude;
        //        ++AddressCount;
        //        var WashTime = carWashLocations.Location[i].WashTimeMinutes;
        //        var OpenTime = carWashLocations.Location[i].StartTime;
        //        var CloseTime = carWashLocations.Location[i].EndTime;
        //        subtitle = WashTime.ToString();                                             
                    
        //        annotations[i] = new MKPointAnnotation()
        //        {
        //            Title = carWashLocations.Location[i].LocationName,                    
        //            //Subtitle = subtitle,
        //            Coordinate = new CLLocationCoordinate2D((double)carWashLocations.Location[i].Latitude, (double)carWashLocations.Location[i].Longitude)                    
        //        };
        //        WashTimeWebView.AddAnnotations(annotations[i]);
        //    }
            //LatCenter = LatCenter / AddressCount;
            //LongCenter = LongCenter / AddressCount;
            //CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(LatCenter, LongCenter);
            //MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 10000, 10000);
            //WashTimeWebView.CenterCoordinate = mapCenter;
            //WashTimeWebView.Region = mapRegion;

            //CenterMap((double)carWashLocations.Location[0].Latitude, (double)carWashLocations.Location[0].Longitude);
    //}

        private void isLocationEnabled()
        {          
            bool status = CLLocationManager.LocationServicesEnabled;

            if(!status)
            {
                var alertView1 = UIAlertController.Create("Alert", "To use this functionality, enable location services from settings in your device", UIAlertControllerStyle.Alert);
                PresentViewController(alertView1, true, null);
                alertView1.AddAction(UIAlertAction.Create("Enable", UIAlertActionStyle.Default, alert => NavToSettings()));
            }
            WashTimeWebView.ShowsUserLocation = false;                             
           
            //SetMapAnnotations();
            PlaceLocationDetailsToMap(carWashLocations.Washes);
        }

        void CenterMap(double lat, double lon)
        {
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1600, 1600); //1 mile approx
            WashTimeWebView.CenterCoordinate = mapCenter;
            WashTimeWebView.Region = mapRegion;
        }
        private void NavToSettings()
        {
            var url = new NSUrl("App-Prefs:root=LOCATION_SERVICES");
            if (UIApplication.SharedApplication.CanOpenUrl(url))
            {
                UIApplication.SharedApplication.OpenUrl(url);
            }            
        }

        void PlaceLocationDetailsToMap(List<LocationStatus> locations)
        {
            if (locations == null) return;

            locations = locations.FindAll(location => location.Latitude != 0 && location.Longitude != 0);

            var annotations = locations.ConvertAll(location => new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D((double)location.Latitude, (double)location.Longitude)
            }).ToArray();

            WashTimeWebView.AddAnnotations(annotations);

            setCenter();
        }

        void setCenter()
        {                                                       
            dict.Clear();

            foreach (var item in carWashLocations.Washes)
            {
                if(item.Latitude != 0 && item.Longitude != 0) // This check to avoid showing locations that dont have lat,long
                    getDistance((double)item.Latitude, (double)item.Longitude, item.LocationId);
            }
            distanceList.Sort();
            foreach(var item in dict)
            {
                if(item.Value == distanceList[0])
                {

                    var shortLoc = carWashLocations.Washes.Find(location => location.LocationId == item.Key);
                    CenterMap((double)shortLoc.Latitude, (double)shortLoc.Longitude);
                }
            }
        }

        async void getDistance(double lat, double lon, int id)
        {
            double latEnd = lat;
            double lngEnd = lon;

            var currentLocation = await Geolocation.GetLastKnownLocationAsync();

            double dist = currentLocation.CalculateDistance(latEnd, lngEnd, DistanceUnits.Miles);
            if (!dict.ContainsKey(id))
            {
                dict.Add(id, dist);
                distanceList.Add(dist);
            }
            
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashStationAnnotationView;
            if(carWashLocations.Washes != null)
            {
                var washlocation = carWashLocations.Washes.FirstOrDefault(location => (double)location.Latitude == annotation.Coordinate.Latitude && (double)location.Longitude == annotation.Coordinate.Longitude);
                annotationView.SetupData(washlocation);
                annotationView.CenterOffset = CGPoint.Empty;

            }
            return annotationView;
        }

        [Export("mapView:didSelectAnnotationView:")]
        public virtual void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            var coordinate = view.Annotation.Coordinate;
            var mapItem = new MKMapItem(new MKPlacemark(coordinate));
            mapItem.Name = view.Annotation.GetTitle();
            mapItem.OpenInMaps();
        }
        //public class MapDelegate : MKMapViewDelegate
        //{
        //    //private UIView CustomMapView;
        //    private bool CustomMapLoaded = false; 
        //    private bool isOpen = true;
        //    static string pId = "Annotation";
        //    string Title = "";
        //    public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        //    { 
        //        if (annotation is MKUserLocation)
        //            return null;

        //        // create pin annotation view
        //        MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

        //        if(pinView == null)
        //            pinView = new MKPinAnnotationView(annotation, pId);
        //        if(pinView.Annotation != null)
        //        {
        //            Title = pinView.Annotation.GetTitle();
        //        }
        //        //var Subtitle = pinView.Annotation.GetSubtitle();

        //        //if (Regex.Matches(Subtitle, @"[a-zA-Z]").Count > 0)
        //        //{
        //        //    isOpen = false;
        //        //}
        //        //else
        //        //{
        //        //    isOpen = true;
        //        //}

        //        var ButtonBackgroundView = new UIButton(new CGRect(x: 0, y: 0, width: 105, height: 40));
        //        ButtonBackgroundView.Layer.CornerRadius = 5;
        //        ButtonBackgroundView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);

        //        if (washlocations != null)
        //        {
        //            if (washlocations.Location != null)
        //            {
        //                foreach (var item in washlocations.Location)
        //                {
        //                    if (Title == item.LocationName)
        //                    {
        //                        var WashTime = item.WashTimeMinutes;
        //                        ButtonBackgroundView.SetTitle(WashTime.ToString() + "mins", UIControlState.Normal);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ButtonBackgroundView.SetTitle("", UIControlState.Normal);
        //            }
        //            pinView.RightCalloutAccessoryView = ButtonBackgroundView;

        //            UIButton carButton = new UIButton(new CGRect(x: 30, y: 0, width: 20, height: 40));
        //            carButton.Layer.CornerRadius = 5;
        //            carButton.SetBackgroundImage(UIImage.FromBundle("icon-car"), UIControlState.Normal);
        //            pinView.LeftCalloutAccessoryView = carButton;

        //            //pinView.LeftCalloutAccessoryView = ButtonBackgroundView;
        //            //pinView.RightCalloutAccessoryView = new UIImageView(UIImage.FromFile("icon-car"));

        //            //CreateCustomView(Title, Subtitle, isOpen);
        //        }
        //        ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Red;
        //        pinView.CanShowCallout = true;

        //        return pinView;
        //    }

        //    public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
        //    {
        //        var coordinate = view.Annotation.Coordinate;
        //        var mapItem = new MKMapItem(new MKPlacemark(coordinate));
        //        mapItem.Name = view.Annotation.GetTitle();
        //        mapItem.OpenInMaps();
        //    }
        //}

        //public class MyLocationDelegate : CLLocationManagerDelegate
        //{
        //    private MKMapView mapView;
        //    public MyLocationDelegate(MKMapView mapView)
        //    {
        //        this.mapView = mapView;
        //    }

        //    public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        //    {

        //    }

        //    public override void AuthorizationChanged(CLLocationManager manager, CLAuthorizationStatus status)
        //    {
        //        mapView.ShowsUserLocation = status == CLAuthorizationStatus.AuthorizedAlways;
        //    }

        //    public override void RegionEntered(CLLocationManager manager, CLRegion region)
        //    {

        //    }

        //    public override void RegionLeft(CLLocationManager manager, CLRegion region)
        //    {

        //    }

        //    public override void DidStartMonitoringForRegion(CLLocationManager manager, CLRegion region)
        //     {

        //    }
        //}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


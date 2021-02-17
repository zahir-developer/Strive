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

namespace StriveCustomer.iOS.Views
{
    public partial class HomeView : MvxViewController<MapViewModel>
    {
        CLLocationManager locationManager = new CLLocationManager();
        private CarWashLocation carWashLocations = new CarWashLocation();
        private MapDelegate mapDelegate;

        public HomeView() : base("HomeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            SetMaps();

            CustomerInfo.setMapInfo();
            WashTimeWebView.MapType = MKMapType.Standard;
            WashTimeWebView.ZoomEnabled = true;
            WashTimeWebView.ScrollEnabled = true;

            locationManager.RequestWhenInUseAuthorization();

            this.mapDelegate = new MapDelegate();
            this.WashTimeWebView.Delegate = this.mapDelegate;

            WashTimeWebView.AddAnnotation(new MKPointAnnotation
            {
                Title = "MyAnnotation",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            });                                                    

            var geofenceRegioncenter = new CLLocationCoordinate2D(8.185458, 77.401112);
            var geofenceRegion = new CLCircularRegion(geofenceRegioncenter, 100, "notifymeonExit");
            geofenceRegion.NotifyOnEntry = true;
            geofenceRegion.NotifyOnExit = true;
            locationManager.StartMonitoring(geofenceRegion);
            locationManager.Delegate = new MyLocationDelegate(WashTimeWebView);
        }
        private async void SetMaps()
        {            
            var locations = await ViewModel.GetAllLocationsCommand();
            if(locations.Location.Count == 0)
            {
                carWashLocations = null;
            }
            else
            {
                carWashLocations = locations;
            }
            isLocationEnabled();            
        }
        private void SetMapAnnotations()
        {
            double LatCenter = 0.0;
            double LongCenter = 0.0;
            int AddressCount = 0;
            
            MKPointAnnotation[] annotations = new MKPointAnnotation[carWashLocations.Location.Count];
            
            for (int i = 0; i < carWashLocations.Location.Count; i++)           
            {
                var subtitle = "";                
                LatCenter += (double)carWashLocations.Location[i].Latitude;
                LongCenter += (double)carWashLocations.Location[i].Longitude;
                ++AddressCount;
                var WashTime = carWashLocations.Location[i].WashTimeMinutes;
                var OpenTime = carWashLocations.Location[i].StartTime;
                var CloseTime = carWashLocations.Location[i].EndTime;
                subtitle = WashTime.ToString();                                             
                    
                annotations[i] = new MKPointAnnotation()
                {
                    Title = carWashLocations.Location[i].LocationName,                    
                    Subtitle = subtitle,
                    Coordinate = new CLLocationCoordinate2D((double)carWashLocations.Location[i].Latitude, (double)carWashLocations.Location[i].Longitude)                    
                };
                WashTimeWebView.AddAnnotations(annotations[i]);
            }
            LatCenter = LatCenter / AddressCount;
            LongCenter = LongCenter / AddressCount;
            //CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(LatCenter, LongCenter);
            //MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 10000, 10000);
            //WashTimeWebView.CenterCoordinate = mapCenter;
            //WashTimeWebView.Region = mapRegion;            
        }

        private void isLocationEnabled()
        {          
            bool status = CLLocationManager.LocationServicesEnabled;

            if(!status)
            {
                var alertView1 = UIAlertController.Create("Alert", "To use this functionality, enable location services from settings in your device", UIAlertControllerStyle.Alert);
                PresentViewController(alertView1, true, null);
                alertView1.AddAction(UIAlertAction.Create("Enable", UIAlertActionStyle.Default, alert => NavToSettings()));
            }
            SetMapAnnotations();
        }
        
        private void NavToSettings()
        {
            var url = new NSUrl("App-Prefs:root=LOCATION_SERVICES");
            if (UIApplication.SharedApplication.CanOpenUrl(url))
            {
                UIApplication.SharedApplication.OpenUrl(url);
            }            
        }
       
        public class MapDelegate : MKMapViewDelegate
        {
            //private UIView CustomMapView;
            private bool CustomMapLoaded = false; 
            private bool isOpen = true;
            static string pId = "Annotation";
                       
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            { 
                if (annotation is MKUserLocation)
                    return null;

                // create pin annotation view
                MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

                if(pinView == null)
                    pinView = new MKPinAnnotationView(annotation, pId);

                //var Title = pinView.Annotation.GetTitle();
                //var Subtitle = pinView.Annotation.GetSubtitle();

                //if (Regex.Matches(Subtitle, @"[a-zA-Z]").Count > 0)
                //{
                //    isOpen = false;
                //}
                //else
                //{
                //    isOpen = true;
                //}
                //var ButtonBackgroundView = new UIButton(new CGRect(x: 0, y: 0, width: 105, height: 40));
                //ButtonBackgroundView.Layer.CornerRadius = 5;
                //ButtonBackgroundView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);

                //pinView.RightCalloutAccessoryView = ButtonBackgroundView;
                //pinView.LeftCalloutAccessoryView = ButtonBackgroundView;

                //CreateCustomView(Title, Subtitle, isOpen);

                ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Red;
                pinView.CanShowCallout = true;

                return pinView;
            }

            public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
            {
                var coordinate = view.Annotation.Coordinate;
                var mapItem = new MKMapItem(new MKPlacemark(coordinate));
                mapItem.Name = view.Annotation.GetTitle();
                mapItem.OpenInMaps();
            }
        }

        public class MyLocationDelegate : CLLocationManagerDelegate
        {
            private MKMapView mapView;
            public MyLocationDelegate(MKMapView mapView)
            {
                this.mapView = mapView;
            }

            public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
            {
               
            }

            public override void AuthorizationChanged(CLLocationManager manager, CLAuthorizationStatus status)
            {
                mapView.ShowsUserLocation = status == CLAuthorizationStatus.AuthorizedAlways;
            }

            public override void RegionEntered(CLLocationManager manager, CLRegion region)
            {
               
            }

            public override void RegionLeft(CLLocationManager manager, CLRegion region)
            {
                
            }

            public override void DidStartMonitoringForRegion(CLLocationManager manager, CLRegion region)
             {
                
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


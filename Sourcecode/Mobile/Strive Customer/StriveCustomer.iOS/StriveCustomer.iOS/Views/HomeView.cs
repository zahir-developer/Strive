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

namespace StriveCustomer.iOS.Views
{
    public partial class HomeView : MvxViewController<MapViewModel>
    {
        CLLocationManager locationManager = new CLLocationManager();

        public HomeView() : base("HomeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            WashTimeWebView.MapType = MKMapType.MutedStandard;
            WashTimeWebView.Delegate = new MapViewDelegate();
            SetMapAnnotations();
            locationManager.RequestAlwaysAuthorization();
            var geofenceRegioncenter = new CLLocationCoordinate2D(8.185458,77.401112);
            var geofenceRegion = new CLCircularRegion(geofenceRegioncenter, 100, "notifymeonExit");
            geofenceRegion.NotifyOnEntry = true;
            geofenceRegion.NotifyOnExit = true;
            locationManager.StartMonitoring(geofenceRegion);
            locationManager.Delegate = new MyLocationDelegate(WashTimeWebView);
        }

        void SetMapAnnotations()
        {
            double LatCenter = 0.0;
            double LongCenter = 0.0;
            int AddressCount = 0;
            var locationAddress = ViewModel.Locations.LocationAddress;
            MKPointAnnotation[] annotations = new MKPointAnnotation[locationAddress.Count];
            for (int i = 0; i < locationAddress.Count; i++)
            {
                var subtitle = "";
                if (locationAddress[i].Latitude > 0 || locationAddress[i].Longitude > 0)
                {
                    LatCenter += locationAddress[i].Latitude;
                    LongCenter += locationAddress[i].Longitude;
                    ++AddressCount;
                    var WashTime = locationAddress[i].WashTiming;
                    var OpenTime = locationAddress[i].OpenTime;
                    var CloseTime = locationAddress[i].CloseTime;
                    subtitle = WashTime.ToString();
                }
                annotations[i] = new MKPointAnnotation()
                {
                    Title = locationAddress[i].Address1,
                    Subtitle = subtitle,
                    Coordinate = new CLLocationCoordinate2D(locationAddress[i].Latitude, locationAddress[i].Longitude)
                };
            }
            LatCenter = LatCenter / AddressCount;
            LongCenter = LongCenter / AddressCount;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(LatCenter, LongCenter);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 10000, 10000);
            WashTimeWebView.CenterCoordinate = mapCenter;
            WashTimeWebView.Region = mapRegion;
            WashTimeWebView.AddAnnotations(annotations);
        }

        public class MapViewDelegate : MKMapViewDelegate
        {
            private UIView CustomMapView;
            private bool CustomMapLoaded = false;
            private bool isOpen = true;
            static string pId = "Annotation";

            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                if (annotation is MKUserLocation)
                    return null;

                // create pin annotation view
                MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);


                pinView = new MKPinAnnotationView(annotation, pId);

                var Title = pinView.Annotation.GetTitle();
                var Subtitle = pinView.Annotation.GetSubtitle();

                if (Regex.Matches(Subtitle, @"[a-zA-Z]").Count > 0)
                {
                    isOpen = false;
                }
                else
                {
                    isOpen = true;
                }
                var ButtonBackgroundView = new UIButton(new CGRect(x: 0, y: 0, width: 105, height: 40));
                ButtonBackgroundView.Layer.CornerRadius = 5;
                ButtonBackgroundView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);

                pinView.CanShowCallout = true;

                pinView.RightCalloutAccessoryView = ButtonBackgroundView;
                pinView.LeftCalloutAccessoryView = ButtonBackgroundView;

                //CreateCustomView(Title, Subtitle, isOpen);

                ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Red;

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


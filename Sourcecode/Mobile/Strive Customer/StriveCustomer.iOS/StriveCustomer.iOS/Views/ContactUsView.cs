﻿using System;
using CoreLocation;
using MapKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class ContactUsView : MvxViewController<ContactUsViewModel>
    { 
        private Locations locations;
        CLLocationManager locationManager = new CLLocationManager();
        private MapDelegate mapDelegate;
        public ContactUsView() : base("ContactUsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetUp();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetUp()
        {
            ContactUsParentView.Layer.CornerRadius = 5;
            ContactUsChildView.Layer.CornerRadius = 5;

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "ContactUs";            

            ContactUsMap.MapType = MKMapType.Standard;
            ContactUsMap.ZoomEnabled = true;
            ContactUsMap.ScrollEnabled = true;

            setMaps();

            locationManager.RequestWhenInUseAuthorization();

            this.mapDelegate = new MapDelegate();
            this.ContactUsMap.Delegate = this.mapDelegate;

            ContactUsMap.AddAnnotation(new MKPointAnnotation
            {
                Title = "MyAnnotation",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            });

            var geofenceRegioncenter = new CLLocationCoordinate2D(8.185458, 77.401112);
            var geofenceRegion = new CLCircularRegion(geofenceRegioncenter, 100, "notifymeonExit");
            geofenceRegion.NotifyOnEntry = true;
            geofenceRegion.NotifyOnExit = true;
            locationManager.StartMonitoring(geofenceRegion);
            locationManager.Delegate = new MyLocationDelegate(ContactUsMap);
        }

        private async void setMaps()
        {
            var allLocations = await this.ViewModel.GetAllLocationsCommand();
            if (allLocations.Location.Count == 0)
            {
                locations = null;
            }
            else
            {
                locations = allLocations;
            }
            SetMapAnnotations();
        }

        private void setData()
        {
            LocationNameLbl.Text = locations.Location[0].LocationName;
            locationValue_Lbl.Text = locations.Location[0].Address1;
            phoneValue_Lbl.Text = locations.Location[0].PhoneNumber;
            mailValue_Lbl.Text = locations.Location[0].Email;
            timeValue_Lbl.Text = locations.Location[0].StartTime + "to" + locations.Location[0].EndTime;
        }

        private void SetMapAnnotations()
        {
            double LatCenter = 0.0;
            double LongCenter = 0.0;
            int AddressCount = 0;

            MKPointAnnotation[] annotations = new MKPointAnnotation[locations.Location.Count];

            for (int i = 0; i < locations.Location.Count; i++)
            {
                var subtitle = "";
                LatCenter += (double)locations.Location[i].Latitude;
                LongCenter += (double)locations.Location[i].Longitude;
                ++AddressCount;
                var WashTime = locations.Location[i].WashTimeMinutes;
                var OpenTime = locations.Location[i].StartTime;
                var CloseTime = locations.Location[i].EndTime;
                subtitle = WashTime.ToString();

                annotations[i] = new MKPointAnnotation()
                {
                    Title = locations.Location[i].Address1,
                    Subtitle = subtitle,
                    Coordinate = new CLLocationCoordinate2D((double)locations.Location[i].Latitude, (double)locations.Location[i].Longitude)
                };
                ContactUsMap.AddAnnotations(annotations[i]);
            }
            LatCenter = LatCenter / AddressCount;
            LongCenter = LongCenter / AddressCount;
            setData();
        }

        public class MapDelegate : MKMapViewDelegate
        {           
            private bool CustomMapLoaded = false;
            private bool isOpen = true;
            static string pId = "Annotation";

            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                if (annotation is MKUserLocation)
                    return null;
               
                MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

                if (pinView == null)
                    pinView = new MKPinAnnotationView(annotation, pId);            
                                
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
    }
}
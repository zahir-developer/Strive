using UIKit;
using MapKit;
using CoreGraphics;
using Foundation;
using CoreLocation;
using System;
using Greeter.Common;
using Greeter.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Greeter.Modules.Home
{
    public partial class WashTimeViewController : BaseViewController, IMKMapViewDelegate
    {
        MKMapView mapView;

        //private readonly CLLocationManager locationManager = new CLLocationManager();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //locationManager.RequestWhenInUseAuthorization();

            SetupView();
            SetupNavigationItem();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            GetData().ConfigureAwait(false);
        }

        void SetupView()
        {
            mapView = new MKMapView(CGRect.Empty)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                WeakDelegate = this,
                MapType = MKMapType.Hybrid
            };

            View.Add(mapView);

            mapView.Register(typeof(WashStationAnnotationView), MKMapViewDefault.AnnotationViewReuseIdentifier);

            mapView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            mapView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            mapView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            mapView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void CenterMap(double lat, double lon)
        {
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            mapView.CenterCoordinate = mapCenter;
            mapView.Region = mapRegion;
        }

        void PlaceLocationDetailsToMap(List<Location> locations)
        {
            if (locations == null) return;

            locations = locations.FindAll(location => location.Latitude != 0 && location.Longitude != 0);

            var annotations = locations.ConvertAll(location => new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D(location.Latitude, location.Longitude)
            }).ToArray();

            mapView.AddAnnotations(annotations);

            var location = locations.Single(x => x.ID == AppSettings.LocationID);
            CenterMap(location.Latitude, location.Longitude);
        }

        void SetupNavigationItem()
        {
            Title = "Home";
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                //AppSettings.Clear();

                //UIViewController loginViewController = UIStoryboard.FromName(StoryBoardNames.USER, null)
                //                      .InstantiateViewController(nameof(LoginViewController));

                //TabBarController.NavigationController.SetViewControllers(new UIViewController[] { loginViewController }, true);
                Logout();
            });
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashStationAnnotationView;
            annotationView.CenterOffset = CGPoint.Empty;
            var location = locations.First(location => location.Latitude == annotation.Coordinate.Latitude && location.Longitude == annotation.Coordinate.Longitude);
            annotationView.SetupData(location);
            return annotationView;
        }
    }
}
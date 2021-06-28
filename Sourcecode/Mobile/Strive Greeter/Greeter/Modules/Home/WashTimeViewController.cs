using UIKit;
using MapKit;
using CoreGraphics;
using Foundation;
using CoreLocation;
using System;

namespace Greeter.Modules.Home
{
    public partial class WashTimeViewController : UIViewController, IMKMapViewDelegate
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

            const double lat = 11.6612012;
            const double lon = 78.1602498;

            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            mapView.CenterCoordinate = mapCenter;
            mapView.Region = mapRegion;

            mapView.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D(lat, lon)
            });

            mapView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            mapView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            mapView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            mapView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            Title = "Wash Time";
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                //TODO logout action here
            });
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            return mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier);
        }
    }
}
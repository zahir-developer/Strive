using UIKit;
using MapKit;
using CoreGraphics;
using Foundation;
using CoreLocation;
using System;
using Greeter.Common;
using System.Threading.Tasks;
using Greeter.Services.Network;
using Greeter.Extensions;
using Greeter.DTOs;
using System.Collections.Generic;

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
            _ = GetData();
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

        void CenterMap(double lat, double lon)
        {
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            mapView.CenterCoordinate = mapCenter;
            mapView.Region = mapRegion;
        }

        void PlaceLocationDetailsToMap(List<Location> locs)
        {
            int len = locs.Count;
            for (int i = 0; i < len; i++)
            {
                mapView.AddAnnotation(new MKPointAnnotation
                {
                    Coordinate = new CLLocationCoordinate2D(locs[i].Latitude, locs[i].Longitude)
                });

                if (i == 0)
                {
                    CenterMap(locs[i].Latitude, locs[i].Longitude);
                }
            }
        }

        void SetupNavigationItem()
        {
            Title = "Home";
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                AppSettings.Clear();

                UIViewController loginViewController = UIStoryboard.FromName(StoryBoardNames.USER, null)
                                      .InstantiateViewController(nameof(LoginViewController));

                TabBarController.NavigationController.SetViewControllers(new UIViewController[] { loginViewController }, true);
            });
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var response = await new ApiService(new NetworkService()).GetLocations();
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                //PlaceLocationDetailsToMap(response?.Locations);
            }
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            return mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier);
        }
    }
}
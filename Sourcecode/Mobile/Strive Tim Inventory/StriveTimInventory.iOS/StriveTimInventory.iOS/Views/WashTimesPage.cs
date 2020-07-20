using System;
using CoreLocation;
using Foundation;
using MapKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class WashTimesPage : MvxViewController<WashTimesViewModel>
    {
        public WashTimesPage() : base("WashTimesPage", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MapView.MapType = MKMapType.Standard;


            double lat = 42.364260;
            double lon = -71.120824;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(lat, lon);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 100, 100);
            MapView.CenterCoordinate = mapCenter;
            MapView.Region = mapRegion;
            MapView.Delegate = new MapViewDelegate();
            MapView.AddAnnotations(new MKPointAnnotation()
            {
                Title = "MyAnnotation",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            });
        }


        public class MapViewDelegate:MKMapViewDelegate
        {
            static string pId = "Annotation";
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                if (annotation is MKUserLocation)
                    return null;

                // create pin annotation view
                MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

                if (pinView == null)
                    pinView = new MKPinAnnotationView(annotation, pId);

                ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Green;
                pinView.CanShowCallout = true;

    
                   
                var button = UIButton.FromType(UIButtonType.Custom);
                button.Frame = new CoreGraphics.CGRect(x: 0, y: 0, width: 50, height: 50);
                button.SetTitle("35 mins", UIControlState.Normal);
                button.BackgroundColor = UIColor.Brown;

                pinView.RightCalloutAccessoryView = button;

                return pinView;
            }

            public override void DidAddAnnotationViews(MKMapView mapView, MKAnnotationView[] views)
            {
                foreach(var view in views)
                {
                    if(view.ReuseIdentifier == pId)
                    {
                        mapView.SelectAnnotation(view.Annotation, true);
                    }
                }
            }
        }

        

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


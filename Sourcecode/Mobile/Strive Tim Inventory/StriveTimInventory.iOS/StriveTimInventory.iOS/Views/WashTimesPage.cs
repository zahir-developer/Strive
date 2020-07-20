using System;
using System.Collections.Generic;
using CoreGraphics;
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
            MapView.MapType = MKMapType.MutedStandard;

            double lat = 42.364260;
            double lon = -71.120824;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(lat, lon);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 100000, 100000);
            MapView.CenterCoordinate = mapCenter;
            MapView.Region = mapRegion;
            MapView.Delegate = new MapViewDelegate();
            MKPointAnnotation[] annotations = new MKPointAnnotation[2];
            annotations[0] = new MKPointAnnotation()
            {
                Title = "Location 1",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            };
            annotations[1] = new MKPointAnnotation()
            {
                Title = "Location 2",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.420824)
            };
            MapView.AddAnnotations(annotations);
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
                pinView.Enabled = true;
                var customView = new UIView(new CGRect(x: 0, y: 0, width: 100, height: 50));
                customView.Layer.CornerRadius = 10;
                var button = UIButton.FromType(UIButtonType.Custom);
                button.Frame = new CGRect(x: 0, y: 0, width: 30, height: 50);
                button.SetImage(UIImage.FromBundle("icon-ios-time"), UIControlState.Normal);
                var label = new UILabel(new CGRect(x: 30, y: 0, width: 100, height: 50));
                label.Text = "35 mins";
                label.TextColor = UIColor.White;
                customView.BackgroundColor = UIColor.Brown;
                customView.AddSubview(button);
                customView.AddSubview(label);
                pinView.RightCalloutAccessoryView = customView;

                return pinView;
            }

            public override void DidAddAnnotationViews(MKMapView mapView, MKAnnotationView[] views)
            {
                IMKAnnotation[] annotations = new IMKAnnotation[views.Length];
                for(int i=0; i<views.Length; i++)
                {
                    if(views[i].ReuseIdentifier == pId)
                    {
                        //mapView.SelectAnnotation(view.Annotation, true);
                        annotations[i] = views[i].Annotation;
                    }
                }
                mapView.SelectedAnnotations = annotations;
            }

        }

        

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


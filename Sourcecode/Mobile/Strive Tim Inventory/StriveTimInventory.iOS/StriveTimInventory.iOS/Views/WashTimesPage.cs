using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
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
            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Dark;
            MapView.MapType = MKMapType.MutedStandard;
            double lat = 42.364260;
            double lon = -71.120824;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(lat, lon);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            MapView.CenterCoordinate = mapCenter;
            MapView.Region = mapRegion;
            MapView.Delegate = new MapViewDelegate();

            MKPointAnnotation[] annotations = new MKPointAnnotation[2];
            annotations[0] = new MKPointAnnotation()
            {
                Title = "Location1",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            };
            annotations[1] = new MKPointAnnotation()
            {
                Title = " ",
                Coordinate = new CLLocationCoordinate2D(42.364260, -71.12824)
            };

            MapView.AddAnnotations(annotations);
            //MapView.GetViewForAnnotation = myViewForAnnotation;
            //MapView.AddAnnotations(new MKPointAnnotation()
            //{
            //    Title = "Location",
            //    Coordinate = new CLLocationCoordinate2D(42.364260, -71.120824)
            //});
        }

        public MyAnnotationView myViewForAnnotation(MKMapView mapView, IMKAnnotation id)
        {
            if (id is MKPointAnnotation)
            {
                MyAnnotationView view = (MyAnnotationView)mapView.DequeueReusableAnnotation("myCustomView");
                if (view == null)
                {
                    view = new MyAnnotationView(id, "myCustomView", UIFont.FromName("OpenSans-Regular", 16f));
                }
                else
                {
                    view.Annotation = id;
                }
                view.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.InfoDark);
                view.Selected = true;
                return view;
            }
            return null;
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
                var parentView = new UIView(new CGRect(x: 0, y: 0, width: 200, height: 50));
                var customView = new UIView(new CGRect(x: 100, y: 5, width: 97, height: 40));
                customView.Layer.CornerRadius = 10;
                var button = UIButton.FromType(UIButtonType.Custom);
                button.Frame = new CGRect(x: 0, y: -3, width: 30, height: 50);
                button.SetImage(UIImage.FromBundle("icon-time-clock"), UIControlState.Normal);
                var label = new UILabel(new CGRect(x: 30, y: -3, width: 100, height: 50));
                label.Text = "35 mins";
                label.TextColor = UIColor.White;
                var Titlelabel = new UILabel(new CGRect(x: 5, y: 0, width: 100, height: 50));
                Titlelabel.Text = "Location #";
                Titlelabel.TextColor = UIColor.Clear.FromHex(0x03FCD3);
                parentView.AddSubview(Titlelabel);
                customView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);
                customView.AddSubview(button);
                customView.AddSubview(label);
                parentView.AddSubview(customView);
                parentView.BackgroundColor = UIColor.DarkGray;
                parentView.Layer.MasksToBounds = false;
                parentView.Layer.ShadowColor = UIColor.Black.CGColor;
                parentView.Layer.ShadowOpacity = 1f;
                parentView.Layer.ShadowOffset = new CGSize(3,3);
                parentView.Layer.ShadowRadius = 5;

                parentView.Layer.ShadowPath = new UIBezierPath().CGPath;
                parentView.Layer.ShouldRasterize = true;
                parentView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
                parentView.Layer.CornerRadius = 5;
                //pinView.RightCalloutAccessoryView = parentView;
                pinView.AddSubview(parentView);

                return pinView;
            }

            public override void DidAddAnnotationViews(MKMapView mapView, MKAnnotationView[] views)
            {
                IMKAnnotation[] annotations = new IMKAnnotation[views.Length];
                var parentView = new UIView(new CGRect(x: 0, y: 0, width: 200, height: 50));
                var customView = new UIView(new CGRect(x: 100, y: 5, width: 97, height: 40));
                customView.Layer.CornerRadius = 10;
                var button = UIButton.FromType(UIButtonType.Custom);
                button.Frame = new CGRect(x: 0, y: -3, width: 30, height: 50);
                button.SetImage(UIImage.FromBundle("icon-time-clock"), UIControlState.Normal);
                var label = new UILabel(new CGRect(x: 30, y: -3, width: 100, height: 50));
                label.Text = "35 mins";
                label.TextColor = UIColor.White;
                var Titlelabel = new UILabel(new CGRect(x: 0, y: 0, width: 100, height: 50));
                Titlelabel.Text = "Location #";
                Titlelabel.TextColor = UIColor.Clear.FromHex(0x03FCD3);
                parentView.AddSubview(Titlelabel);
                customView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);
                customView.AddSubview(button);
                customView.AddSubview(label);
                parentView.BackgroundColor = UIColor.Red;
                parentView.AddSubview(customView);
                for (int i=0; i<views.Length; i++)
                {
                    if(views[i].ReuseIdentifier == pId)
                    {
                        //mapView.SelectAnnotation(view.Annotation, true);
                        //views[i].AddSubview(parentView);
                        annotations[i] = views[i].Annotation;
                    }
                }
                mapView.SelectedAnnotations = annotations;
            }

        }

        public class MyAnnotationView : MKAnnotationView // or MKPointAnnotation
        {
            UIFont _font;
            public MyAnnotationView(IMKAnnotation annotation, string reuseIdentifier, UIFont font) : base(annotation, reuseIdentifier)
            {
                _font = font;
                CanShowCallout = true;
                Image = UIImage.FromBundle("icon-wash-time");
            }

            void searchViewHierarchy(UIView currentView)
            {
                // short-circuit
                if (currentView.Subviews == null || currentView.Subviews.Length == 0)
                {
                    return;
                }
                foreach (UIView subView in currentView.Subviews)
                {
                    if (subView is UILabel)
                    {
                        (subView as UILabel).Font = _font;
                    }
                    else
                    {
                        searchViewHierarchy(subView);
                    }
                }
            }

            public override void LayoutSubviews()
            {
                if (!Selected)
                    return;
                base.LayoutSubviews();
                foreach (UIView view in Subviews)
                {
                    Console.WriteLine(view);
                    searchViewHierarchy(view);
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


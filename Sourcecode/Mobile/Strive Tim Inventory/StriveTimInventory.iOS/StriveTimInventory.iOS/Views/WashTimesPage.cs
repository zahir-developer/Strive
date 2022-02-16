using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class WashTimesPage : MvxViewController<WashTimesViewModel>, IMKMapViewDelegate
    {
        public washLocations carWashLocations = new washLocations();
        public static washLocations washlocations;

        public WashTimesPage() : base("WashTimesPage", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            SetMapCenter();
            ScheduleSetMap();
            var set = this.CreateBindingSet<WashTimesPage, WashTimesViewModel>();
            set.Bind(LogOutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();
            MapView.MapType = MKMapType.Hybrid;
            MapView.WeakDelegate = this;
            
            MapView.Register(typeof(WashAnnotation), MKMapViewDefault.AnnotationViewReuseIdentifier);
        }
        public async void ScheduleSetMap()
        {
            int seconds = 60000;
            while (seconds != 0)
            {
                await Task.Delay(seconds);
                SetMapCenter();
                //Console.WriteLine("refreshing home");
            }
        }
        async void SetMapCenter()
        {
            //var location = await ViewModel.GetAllLocationAddress();
            //if(location != null)
            //{
            //    SetMapAnnotations();
            //}
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
            PlaceLocationDetailsToMap(carWashLocations.Washes);
        }

        void PlaceLocationDetailsToMap(List<LocationStatus> locations)
        {
            if (locations == null) return;

            locations = locations.FindAll(location => location.Latitude != 0 && location.Longitude != 0);

            var annotations = locations.ConvertAll(location => new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D((double)location.Latitude, (double)location.Longitude)
            }).ToArray();

            MapView.AddAnnotations(annotations);

            setCenter();
        }

        void setCenter()
        {
            var shortLoc = carWashLocations.Washes.Find(location => location.LocationId == EmployeeData.selectedLocationId);
            CenterMap((double)shortLoc.Latitude, (double)shortLoc.Longitude);
        }

        void CenterMap(double lat, double lon)
        {
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1600, 1600); //approx 1 mile
            MapView.CenterCoordinate = mapCenter;
            MapView.Region = mapRegion;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        private void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                OverrideUserInterfaceStyle = UIUserInterfaceStyle.Dark;
            }
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashAnnotation;
            if (carWashLocations.Washes != null)
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

        //void SetMapAnnotations()
        //{
        //    double LatCenter = 0.0;
        //    double LongCenter = 0.0;
        //    int AddressCount = 0;
        //    var locationAddress = ViewModel.Location.Location;
        //    MKPointAnnotation[] annotations = new MKPointAnnotation[locationAddress.Count];
        //    for (int i = 0 ; i< locationAddress.Count;i++)
        //    {
        //        var subtitle = "";

        //        LatCenter += (double)locationAddress[i].Latitude;
        //        LongCenter += (double)locationAddress[i].Longitude;
        //        ++AddressCount;

        //        var WashTime = locationAddress[i].WashTimeMinutes;
        //        var OpenTime = locationAddress[i].StartTime;
        //        var CloseTime = locationAddress[i].EndTime;

        //        subtitle = WashTime.ToString();
        //        annotations[i] = new MKPointAnnotation()
        //        {
        //            Title = locationAddress[i].LocationName,
        //            Subtitle = subtitle,
        //            Coordinate = new CLLocationCoordinate2D((double)locationAddress[i].Latitude,(double)locationAddress[i].Longitude)
        //        };
        //    }
        //    LatCenter = LatCenter / AddressCount;
        //    LongCenter = LongCenter / AddressCount;
        //    CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D(LatCenter, LongCenter);
        //    MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 10000, 10000);
        //    MapView.CenterCoordinate = mapCenter;
        //    MapView.Region = mapRegion;
        //    MapView.AddAnnotations(annotations);
        //}

        //public class MapViewDelegate:MKMapViewDelegate
        //{
        //    public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        //    {
        //        var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashAnnotation;
        //        if (washlocations.Washes != null)
        //        {
        //            var washlocation = washlocations.Washes.FirstOrDefault(location => (double)location.Latitude == annotation.Coordinate.Latitude && (double)location.Longitude == annotation.Coordinate.Longitude);
        //            annotationView.SetupData(washlocation);
        //        }
        //        return annotationView;
        //    }

        //    public virtual void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        //    {
        //        var coordinate = view.Annotation.Coordinate;
        //        var mapItem = new MKMapItem(new MKPlacemark(coordinate));
        //        mapItem.Name = view.Annotation.GetTitle();
        //        mapItem.OpenInMaps();
        //    }
        //private UIView CustomMapView;
        //private bool CustomMapLoaded = false;
        //private bool isOpen = true;
        //static string pId = "Annotation";

        //public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        //{
        //    if (annotation is MKUserLocation)
        //        return null;

        //    // create pin annotation view
        //    MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);


        //    pinView = new MKPinAnnotationView(annotation, pId);

        //    var Title = pinView.Annotation.GetTitle();
        //    var Subtitle = pinView.Annotation.GetSubtitle();

        //    if (Regex.Matches(Subtitle, @"[a-zA-Z]").Count > 0)
        //    {
        //        isOpen = false;
        //    }
        //    else
        //    {
        //        isOpen = true;
        //    }

        //    CreateCustomView(Title,Subtitle,isOpen);

        //    ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Green;

        //    pinView.AddSubview(CustomMapView);

        //    return pinView;


        //}

        //private void CreateCustomView(string Title,string time, bool isOpen)
        //{
        //    var BackgroundView = UIButton.FromType(UIButtonType.Custom);
        //    BackgroundView.Frame = new CGRect(x: -140, y: 4, width: 320, height: 100);
        //    BackgroundView.SetImage(UIImage.FromBundle("MapBackground"), UIControlState.Normal);

        //    var Titlelabel = new UILabel(new CGRect(x: 45, y: 30, width: 130, height: 50));
        //    Titlelabel.Text = Title;
        //    Titlelabel.Font = DesignUtils.OpenSansBoldEighteen();
        //    Titlelabel.TextColor = UIColor.Clear.FromHex(0x2AC1B1);
        //    Titlelabel.Lines = 2;

        //    BackgroundView.AddSubview(Titlelabel);

        //    var PointerView = UIButton.FromType(UIButtonType.Custom);
        //    PointerView.Frame = new CGRect(x: 45, y: 45, width: 30, height: 30);
        //    PointerView.SetImage(UIImage.FromBundle("MapPointer"), UIControlState.Normal);
        //    PointerView.AddSubview(BackgroundView);

        //    var ButtonBackgroundView = new UIView(new CGRect(x: 180, y: 33, width: 105, height: 40));
        //    ButtonBackgroundView.Layer.CornerRadius = 5;
        //    ButtonBackgroundView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);

        //    var IconImage = UIButton.FromType(UIButtonType.Custom);
        //    IconImage.Frame = new CGRect(x: 0, y: -5, width: 30, height: 50);
        //    IconImage.SetImage(UIImage.FromBundle("map-car-icon"), UIControlState.Normal);

        //    var SubLabel = new UILabel(new CGRect(x: 30, y: -15, width: 100, height: 50));
        //    SubLabel.Text = "Opens at";
        //    SubLabel.Font = DesignUtils.OpenSansRegularTwelve();
        //    SubLabel.TextColor = UIColor.Clear.FromHex(0x00584F);

        //    var TimeLabel = new UILabel();

        //    if (isOpen)
        //    {
        //        TimeLabel.Font = DesignUtils.OpenSansBoldEighteen();
        //        TimeLabel.Frame = new CGRect(x: 28, y: -3, width: 100, height: 50);
        //        TimeLabel.Text = time + " mins";
        //        SubLabel.Hidden = true;
        //    } else
        //    {
        //        TimeLabel.Font = DesignUtils.OpenSansBoldSixteen();
        //        TimeLabel.Frame = new CGRect(x: 28, y: 3, width: 100, height: 50);
        //        TimeLabel.Text = time;
        //        SubLabel.Hidden = false;
        //    }


        //    TimeLabel.TextColor = UIColor.Clear.FromHex(0x002E29);     

        //    ButtonBackgroundView.AddSubview(SubLabel);
        //    ButtonBackgroundView.AddSubview(IconImage);
        //    ButtonBackgroundView.AddSubview(TimeLabel);
        //    BackgroundView.AddSubview(ButtonBackgroundView);

        //    var OuterEllipse = new UIImageView(new CGRect(x: -53, y: -55, width: 120, height: 120));
        //    OuterEllipse.Image = UIImage.FromBundle("white-ellipse");

        //    var CenterEllipse = new UIImageView(new CGRect(x: 30, y: 30, width: 60, height: 60));
        //    CenterEllipse.Image = UIImage.FromBundle("white-ellipse");

        //    var InnerEllipse = new UIImageView(new CGRect(x: 40, y: 40, width: 40, height: 40));
        //    InnerEllipse.Image = UIImage.FromBundle("white-ellipse");

        //    OuterEllipse.AddSubview(CenterEllipse);
        //    OuterEllipse.AddSubview(InnerEllipse);
        //    OuterEllipse.AddSubview(PointerView);

        //    CustomMapView = OuterEllipse;
        //}
        //}

        //public override void DidReceiveMemoryWarning()
        //{
        //    base.DidReceiveMemoryWarning();
        //    // Release any cached data, images, etc that aren't in use.
        //}


        //private void DoInitialSetup()
        //{
        //    NavigationController.NavigationBarHidden = true;
        //    if(UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
        //    {
        //        OverrideUserInterfaceStyle = UIUserInterfaceStyle.Dark;
        //    }
        //}
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class ContactUsView : MvxViewController<ContactUsViewModel>, IMKMapViewDelegate
    { 
        public Locations locations;
        public static Locations washlocations;
        CLLocationManager locationManager = new CLLocationManager();
        public ContactUsView() : base("ContactUsView", null)
        {
        }             

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetUp();
            MaintainData.SetMapData(LocationNameLbl, locationValue_Lbl, phoneValue_Lbl, mailValue_Lbl, timeValue_Lbl);
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

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            ContactUsMap.MapType = MKMapType.Hybrid;
            ContactUsMap.WeakDelegate = this;
            ContactUsMap.ZoomEnabled = true;
            ContactUsMap.ScrollEnabled = true;

            setMaps();
            locationManager.RequestWhenInUseAuthorization();
            ContactUsMap.Register(typeof(WashStationAnnotationView), MKMapViewDefault.AnnotationViewReuseIdentifier);                        
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
                washlocations = allLocations;
            }
            //SetMapAnnotations();
            PlaceLocationDetailsToMap(locations.Location);
        }

        public void setData(int index)
        {
            LocationNameLbl.Text = washlocations.Location[index].LocationName;
            locationValue_Lbl.Text = washlocations.Location[index].Address1;
            phoneValue_Lbl.Text = washlocations.Location[index].PhoneNumber;
            mailValue_Lbl.Text = washlocations.Location[index].Email;
            if(washlocations.Location[index].StartTime != null)
            {
                timeValue_Lbl.Text = washlocations.Location[index].StartTime + "to" + washlocations.Location[index].EndTime;
            }
        } 
        public void setAnnotationData(UILabel locationName, UILabel locationValue_Lbls, UILabel phoneValue_Lbls, UILabel mailValue_Lbls, UILabel timeValue_Lbls, int index)
        {
            LocationNameLbl = locationName;
            locationValue_Lbl = locationValue_Lbls;
            phoneValue_Lbl = phoneValue_Lbls;
            mailValue_Lbl = mailValue_Lbls;
            LocationNameLbl.Text = washlocations.Location[index].LocationName;
            locationValue_Lbl.Text = washlocations.Location[index].Address1;
            phoneValue_Lbl.Text = washlocations.Location[index].PhoneNumber;
            mailValue_Lbl.Text = washlocations.Location[index].Email;
            if (washlocations.Location[index].StartTime != null)
            {
                timeValue_Lbl.Text = washlocations.Location[index].StartTime + "to" + washlocations.Location[index].EndTime;
            }
        }

        void PlaceLocationDetailsToMap(List<Location> locations)
        {
            if (locations == null) return;

            locations = locations.FindAll(location => location.Latitude != 0 && location.Longitude != 0);

            var annotations = locations.ConvertAll(location => new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D((double)location.Latitude, (double)location.Longitude)
            }).ToArray();

            ContactUsMap.AddAnnotations(annotations);            
            CenterMap((double)locations[0].Latitude, (double)locations[0].Longitude);
            setData(0);
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashStationAnnotationView;
            if (locations.Location != null)
            {
                var washlocation = locations.Location.First(location => (double)location.Latitude == annotation.Coordinate.Latitude && (double)location.Longitude == annotation.Coordinate.Longitude);
                annotationView.SetupData(washlocation);
            }
            return annotationView;
        }        

        [Export("mapView:didSelectAnnotationView:")]
        public virtual void DidSelectAnnotationView(MKMapView mapView, MKAnnotationView view)
        {
            var coordinate = view.Annotation.Coordinate;
            var mapItem = new MKMapItem(new MKPlacemark(coordinate));
            mapItem.Name = view.Annotation.GetTitle();
            
            var index = 0;
            foreach (var item in washlocations.Location)
            {
                if (coordinate.Latitude == (double)item.Latitude)
                {
                    if(coordinate.Longitude == (double)item.Longitude)
                    {
                        setAnnotationData(MaintainData.LocationNameLbl, MaintainData.locationValue_Lbl, MaintainData.phoneValue_Lbl, MaintainData.mailValue_Lbl, MaintainData.timeValue_Lbl, index);
                    }
                }
                index++;
            }
        }
        //private void SetMapAnnotations()
        //{
        //    double LatCenter = 0.0;
        //    double LongCenter = 0.0;
        //    int AddressCount = 0;

        //    MKPointAnnotation[] annotations = new MKPointAnnotation[locations.Location.Count];

        //    for (int i = 0; i < locations.Location.Count; i++)
        //    {
        //        var subtitle = "";
        //        LatCenter += (double)locations.Location[i].Latitude;
        //        LongCenter += (double)locations.Location[i].Longitude;
        //        ++AddressCount;
        //        var WashTime = locations.Location[i].WashTimeMinutes;
        //        var OpenTime = locations.Location[i].StartTime;
        //        var CloseTime = locations.Location[i].EndTime;
        //        subtitle = WashTime.ToString();

        //        annotations[i] = new MKPointAnnotation()
        //        {
        //            Title = locations.Location[i].LocationName,
        //            //Subtitle = subtitle,
        //            Coordinate = new CLLocationCoordinate2D((double)locations.Location[i].Latitude, (double)locations.Location[i].Longitude)
        //        };
        //        ContactUsMap.AddAnnotations(annotations[i]);
        //    }
        //    LatCenter = LatCenter / AddressCount;
        //    LongCenter = LongCenter / AddressCount;
        //    CenterMap((double)locations.Location[0].Latitude, (double)locations.Location[0].Longitude);
        //    setData(0);
        //}
        void CenterMap(double lat, double lon)
        {
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000);
            ContactUsMap.CenterCoordinate = mapCenter;
            ContactUsMap.Region = mapRegion;
        }
        public class MaintainData
        {
            public static UILabel LocationNameLbl { get; set; }
            public static UILabel locationValue_Lbl { get; set; }
            public static UILabel phoneValue_Lbl { get; set; }
            public static UILabel mailValue_Lbl { get; set; }
            public static UILabel timeValue_Lbl { get; set; }

            public static void SetMapData(UILabel locationName, UILabel locationValue_Lbls, UILabel phoneValue_Lbls, UILabel mailValue_Lbls, UILabel timeValue_Lbls)
            {
                LocationNameLbl = locationName;
                locationValue_Lbl = locationValue_Lbls;
                phoneValue_Lbl = phoneValue_Lbls;
                mailValue_Lbl = mailValue_Lbls;
                timeValue_Lbl = timeValue_Lbls;
            }
        }                 
    }
}
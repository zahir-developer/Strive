using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLocation;
using Foundation;
using MapKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;
using Xamarin.Essentials;

namespace StriveCustomer.iOS.Views
{
    public partial class ContactUsView : MvxViewController<ContactUsViewModel>, IMKMapViewDelegate
    {        
        public washLocations locations;
        public static washLocations washlocations;
        public Locations Locations;
        public List<Double> distanceList = new List<double>();
        Dictionary<int, double> dict = new Dictionary<int, double>();

        CLLocationManager locationManager = new CLLocationManager();
        public ContactUsView() : base("ContactUsView", null)
        {
        }

        //public override void ViewDidLoad()
        //{
        //    base.ViewDidLoad();
        //    InitialSetUp();
        //    MaintainData.SetMapData(LocationNameLbl, locationValue_Lbl, phoneValue_Lbl, mailValue_Lbl, timeValue_Lbl);
        //    // Perform any additional setup after loading the view, typically from a nib.
        //}
        

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        public override void ViewDidAppear(bool animated)
        {
            InitialSetUp();
            //setMaps();
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
            //var allLocations = await this.ViewModel.GetAllLocationsCommand();
            var allLocations = await ViewModel.GetAllLocationStatus();
            if (allLocations.Washes.Count == 0)
            {
                locations = null;
            }
            else
            {
                locations = allLocations;
                washlocations = allLocations;
            }
            
            PlaceLocationDetailsToMap(locations.Washes);
        }

        public void setData(int index)
        {
            LocationNameLbl.Text = washlocations.Washes[index].LocationName;
            locationValue_Lbl.Text = washlocations.Washes[index].Address1;
            phoneValue_Lbl.Text = washlocations.Washes[index].PhoneNumber;
            mailValue_Lbl.Text = washlocations.Washes[index].Email;
            if (washlocations.Washes[index].StoreTimeIn != null)
            {   
                DateTime StartTime = DateTime.Parse(washlocations.Washes[index].StoreTimeIn);
                DateTime EndTime = DateTime.Parse(washlocations.Washes[index].StoreTimeOut);
                timeValue_Lbl.Text = StartTime.TimeOfDay.ToString() + " to " + EndTime.TimeOfDay.ToString();
            }
        }

        async partial   void FacebookRedirect(UIButton sender)
        {
            try
            {
                 await Browser.OpenAsync(new Uri("https://www.facebook.com/MammothDetailSalon"), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to open ");
            }
        }
        async partial void InstagramRedirect(UIButton sender)
        {
            try
            {
                await Browser.OpenAsync(new Uri("https://www.instagram.com/mammothdetailsalons/"), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to open ");
            }
            
        }
        async partial void TwitterRedirect(UIButton sender)
        {
            try
            {
                await Browser.OpenAsync(new Uri("https://twitter.com/mammoth_detail"), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to open ");
            }
        }
        public void setAnnotationData(UILabel locationName, UILabel locationValue_Lbls, UILabel phoneValue_Lbls, UILabel mailValue_Lbls, UILabel timeValue_Lbls, int index)
        {
            LocationNameLbl = locationName;
            locationValue_Lbl = locationValue_Lbls;
            phoneValue_Lbl = phoneValue_Lbls;
            mailValue_Lbl = mailValue_Lbls;
            LocationNameLbl.Text = washlocations.Washes[index].LocationName;
            locationValue_Lbl.Text = washlocations.Washes[index].Address1;
            phoneValue_Lbl.Text = washlocations.Washes[index].PhoneNumber;
            mailValue_Lbl.Text = washlocations.Washes[index].Email;
            //if (washlocations.Washes[index].StartTime != null)
            //{
            //    timeValue_Lbl.Text = washlocations.Location[index].StartTime + "to" + washlocations.Location[index].EndTime;
            //}
        }

        void PlaceLocationDetailsToMap(List<LocationStatus> locations)
        {
            if (locations == null) return;

            locations = locations.FindAll(location => location.Latitude != 0 && location.Longitude != 0);

            var annotations = locations.ConvertAll(location => new MKPointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D((double)location.Latitude, (double)location.Longitude)
            }).ToArray();

            ContactUsMap.AddAnnotations(annotations);
            setData(0);
            setCenter();
        }

        void setCenter()
        {
            dict.Clear();

            foreach (var item in locations.Washes)
            {
                getDistance((double)item.Latitude, (double)item.Longitude, item.LocationId);
            }
            distanceList.Sort();
            foreach (var item in dict)
            {
                if (item.Value == distanceList[0])
                {

                    var shortLoc = locations.Washes.Find(location => location.LocationId == item.Key);
                    CenterMap((double)shortLoc.Latitude, (double)shortLoc.Longitude);
                }
            }
        }

        async void getDistance(double lat, double lon, int id)
        {
            double latEnd = lat;
            double lngEnd = lon;

            var currentLocation = await Geolocation.GetLastKnownLocationAsync();
            double dist = currentLocation.CalculateDistance(latEnd, lngEnd, DistanceUnits.Miles);

            dict.Add(id, dist);
            distanceList.Add(dist);
        }

        [Export("mapView:viewForAnnotation:")]
        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var annotationView = mapView.DequeueReusableAnnotation(MKMapViewDefault.AnnotationViewReuseIdentifier) as WashStationAnnotationView;
            if (locations.Washes != null)
            {
                var washlocation = locations.Washes.First(location => (double)location.Latitude == annotation.Coordinate.Latitude && (double)location.Longitude == annotation.Coordinate.Longitude);
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
            foreach (var item in washlocations.Washes)
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
using System;
using CoreGraphics;
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
        public Locations locations;
        public static Locations washlocations;
        CLLocationManager locationManager = new CLLocationManager();
        private MapDelegate mapDelegate;
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

            ContactUsMap.MapType = MKMapType.Standard;
            ContactUsMap.ZoomEnabled = true;
            ContactUsMap.ScrollEnabled = true;

            setMaps();

            locationManager.RequestWhenInUseAuthorization();

            this.mapDelegate = new MapDelegate();
            this.ContactUsMap.Delegate = this.mapDelegate;
                       
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
                washlocations = allLocations;
            }
            SetMapAnnotations();
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
                    Title = locations.Location[i].LocationName,
                    //Subtitle = subtitle,
                    Coordinate = new CLLocationCoordinate2D((double)locations.Location[i].Latitude, (double)locations.Location[i].Longitude)
                };
                ContactUsMap.AddAnnotations(annotations[i]);
            }
            LatCenter = LatCenter / AddressCount;
            LongCenter = LongCenter / AddressCount;
            setData(0);
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

        public class MapDelegate : MKMapViewDelegate
        {           
            static string pId = "Annotation";
            string Title = "";
            ContactUsView objContactUsView = new ContactUsView();
            public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
            {
                if (annotation is MKUserLocation)
                    return null;
               
                MKAnnotationView pinView = (MKPinAnnotationView)mapView.DequeueReusableAnnotation(pId);

                if (pinView == null)
                    pinView = new MKPinAnnotationView(annotation, pId);

                if (pinView.Annotation != null)
                {
                    Title = pinView.Annotation.GetTitle();
                }

                var ButtonBackgroundView = new UIButton(new CGRect(x: 0, y: 0, width: 105, height: 40));
                ButtonBackgroundView.Layer.CornerRadius = 5;
                ButtonBackgroundView.BackgroundColor = UIColor.Clear.FromHex(0xFCC201);

                if (washlocations.Location != null)
                {
                    foreach (var item in washlocations.Location)
                    {
                        if (Title == item.LocationName)
                        {
                            var WashTime = item.WashTimeMinutes;
                            ButtonBackgroundView.SetTitle(WashTime.ToString() + "mins", UIControlState.Normal);                            
                        }
                    }                                      
                }
                else
                {
                    ButtonBackgroundView.SetTitle("Wash Time", UIControlState.Normal);
                }
                pinView.RightCalloutAccessoryView = ButtonBackgroundView;

                UIButton carButton = new UIButton(new CGRect(x: 30, y: 0, width: 20, height: 40));
                carButton.Layer.CornerRadius = 5;
                carButton.SetBackgroundImage(UIImage.FromBundle("icon-car"), UIControlState.Normal);
                pinView.LeftCalloutAccessoryView = carButton;

                ((MKPinAnnotationView)pinView).PinColor = MKPinAnnotationColor.Red;
                pinView.CanShowCallout = true;

                return pinView;
            }

            public override void CalloutAccessoryControlTapped(MKMapView mapView, MKAnnotationView view, UIControl control)
            {
                var coordinate = view.Annotation.Coordinate;
                var mapItem = new MKMapItem(new MKPlacemark(coordinate));
                mapItem.Name = view.Annotation.GetTitle();
                //mapItem.OpenInMaps();
                var index = 0;
                foreach(var item in washlocations.Location)
                {
                    if(mapItem.Name == item.LocationName)
                    {
                        objContactUsView.setAnnotationData(MaintainData.LocationNameLbl, MaintainData.locationValue_Lbl, MaintainData.phoneValue_Lbl, MaintainData.mailValue_Lbl, MaintainData.timeValue_Lbl, index);
                    }
                    index++;
                }
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
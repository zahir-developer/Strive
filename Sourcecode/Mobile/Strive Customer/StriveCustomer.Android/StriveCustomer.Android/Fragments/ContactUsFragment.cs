using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Services;
using Xamarin.Essentials;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Double = System.Double;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class ContactUsFragment : MvxFragment<ContactUsViewModel>, IOnMapReadyCallback, IOnSuccessListener, IConnectionCallbacks, IOnConnectionFailedListener
    {
        private GoogleMap Googlemap;
        private Locations Locations;
       // public washLocations locations;
        //public static washLocations washlocations;

        private Locations locations;
        public washLocations washlocations = new washLocations();
        private GoogleApiClient googleAPI;
        private SupportMapFragment gmaps;
        private int carWashLocationsCount, latLngCount, geofencesCount, geofenceCirclesCount;
        private static View rootView;
        private LatLng[] carWashLatLng;
        private MarkerOptions[] carWashMarkerOptions;
        private TextView locationDetails;
        private TextView phoneDetails;
        private TextView mailDetails;
        private TextView clockDetails;
        private TextView locationName;
        private ImageButton facebookIcon;
        private ImageButton instagramIcon;
        private ImageButton twitterIcon;


        public List<Double> distanceList;
        Dictionary<int, double> dict = new Dictionary<int, double>();
        Dictionary<string, object> markerDic;
        IList<IGeofence> carWashGeofences;
        private GeofenceHelper geofenceHelper;
        string GeofenceID = "LatLng";
        private GeofencingRequest geofencingRequests;
        private PendingIntent geoPendingIntent;
        private GeofencingClient geofencingClient;
        private CircleOptions[] geofenceCircles;
        private Marker[] marker;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.ViewModel = new ContactUsViewModel();
            Locations = new Locations();

            locations = new Locations();
            geofenceHelper = new GeofenceHelper(this.Context);
            geofencingClient = LocationServices.GetGeofencingClient(this.Context);
            if (rootView != null)
            {
                ViewGroup parent = (ViewGroup)rootView.Parent;
                if (parent != null)
                    parent.RemoveView(rootView);
            }
            try
            {
                var ignore = base.OnCreateView(inflater, container, savedInstanceState);
                rootView = null;
                if (rootView == null)
                {
                    rootView = this.BindingInflate(Resource.Layout.ContactUsFragment, null);
                    locationName = rootView.FindViewById<TextView>(Resource.Id.locationName);
                    locationDetails = rootView.FindViewById<TextView>(Resource.Id.locationDetails);
                    phoneDetails = rootView.FindViewById<TextView>(Resource.Id.phoneDetails);
                    mailDetails = rootView.FindViewById<TextView>(Resource.Id.mailDetails);
                    clockDetails = rootView.FindViewById<TextView>(Resource.Id.storeClockDetails);
                    facebookIcon = rootView.FindViewById<ImageButton>(Resource.Id.facebookIcon);
                    instagramIcon = rootView.FindViewById<ImageButton>(Resource.Id.instagramIcon);
                    twitterIcon = rootView.FindViewById<ImageButton>(Resource.Id.twitterIcon);
                    setupMaps();
                   
                }
                else
                {
                    return rootView;
                }
                facebookIcon.Click += FacebookIcon_Click;
                twitterIcon.Click += TwitterIcon_Click;
                instagramIcon.Click += InstagramIcon_Click;

            }
            catch(InflateException e)
            {
                return rootView;
            }           
            return rootView;
        }

        private void InstagramIcon_Click(object sender, EventArgs e)
        {
            try
            {
                Browser.OpenAsync(new Uri("https://www.instagram.com/mammothdetailsalons/"), BrowserLaunchMode.SystemPreferred);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("unable to open ");
                //  An unexpected error occured. No browser may be installed on the device.
            }
        }

        private void TwitterIcon_Click(object sender, EventArgs e)
        {
            try
            {
                Browser.OpenAsync(new Uri("https://twitter.com/mammoth_detail"), BrowserLaunchMode.SystemPreferred);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("unable to open ");
              //  An unexpected error occured. No browser may be installed on the device.
            }
        }

        private void FacebookIcon_Click(object sender, EventArgs e)
        {
            try
            {
                Browser.OpenAsync(new Uri("https://www.facebook.com/MammothDetailSalon"), BrowserLaunchMode.SystemPreferred);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("unable to open ");
                //  An unexpected error occured. No browser may be installed on the device.
            }
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            Googlemap = googleMap;
            Googlemap.MapType = GoogleMap.MapTypeHybrid;
            Googlemap.UiSettings.CompassEnabled = false;
            await AndroidPermissions.checkLocationPermission(this);
            Googlemap.UiSettings.ZoomGesturesEnabled = true;
            setUpMarkers();
            loadFirstMarkerData();
            Googlemap.MarkerClick += Googlemap_MarkerClick; 
        }


        private void setUpMarkers()
        {
            carWashLocationsCount = 0;

            distanceList = new List<double>();
            dict.Clear();
            foreach (var carWashLocation in washlocations.Washes)
            {
                if (carWashLocation.Latitude != 0 && carWashLocation.Longitude != 0)
                {
                    getDistance((double)carWashLocation.Latitude, (double)carWashLocation.Longitude, carWashLocation.LocationId);

                }
                carWashLocationsCount++;
            }
            carWashLatLng = new LatLng[distanceList.Count];
            markerDic = new Dictionary<string, object>();
            marker = new Marker[distanceList.Count];
            distanceList.Sort();
            Googlemap.Clear();
            latLngCount = 0;
            foreach (var item in dict)
            {
                var shortLoc = washlocations.Washes.Find(location => location.LocationId == item.Key);
                carWashLatLng[latLngCount] = new LatLng((double)shortLoc.Latitude, (double)shortLoc.Longitude);
                marker[latLngCount] = Googlemap.AddMarker(new MarkerOptions().SetPosition(carWashLatLng[latLngCount]).InvokeZIndex(dict.Count - 1 - latLngCount)
                         .SetIcon(BitmapDescriptorFactory.FromBitmap(getMarkerBitmapFromView(shortLoc, Resource.Drawable.bubblePopWindow))));
                marker[latLngCount].Tag = carWashLatLng[latLngCount];
                Googlemap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(carWashLatLng[latLngCount], 17));
                if (!markerDic.ContainsKey(marker[latLngCount].Id))
                {
                    markerDic.Add(marker[latLngCount].Id, marker[latLngCount].Tag);
                }
                latLngCount++;

            }
            addCarwashGeoFence(carWashLatLng, 100f);
        }
        async void getDistance(double lat, double lon, int id)
        {
            double latEnd = lat;
            double lngEnd = lon;
            try
            {
                var currentLocation = await Geolocation.GetLastKnownLocationAsync();
                double dist = (double)(currentLocation?.CalculateDistance(latEnd, lngEnd, DistanceUnits.Miles));
                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, dist);
                    distanceList.Add(dist);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (System.Exception ex)
            {
                // Unable to get location
            }
        }
     
        public async void setupMaps()
        {
            try
            {
                var allLocations = await this.ViewModel.GetAllLocationStatus();
                if (allLocations.Washes.Count == 0)
                {
                    Locations = null;
                }
                else
                {

                    washlocations = allLocations;
                }
                if (!IsAdded)
                {
                    return;
                }
                else
                {
                    gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.contactUsMaps);
                }
                if (gmaps != null)
                {
                    gmaps.GetMapAsync(this);
                }
            }
            catch (System.Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }
        private void Googlemap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            e.Marker.ZIndex = e.Marker.ZIndex + 2;
            var data = washlocations.Washes.Find(x => (double)x.Latitude == e.Marker.Position.Latitude);
            locationName.Text = data.LocationName;
            locationDetails.Text = data.Address1;
            phoneDetails.Text = data.PhoneNumber;
            mailDetails.Text = data.Email;
            if (washlocations.Washes[0].StoreTimeIn != null)
            {
                DateTime StartTime = DateTime.Parse(washlocations.Washes[0].StoreTimeIn);
                DateTime EndTime = DateTime.Parse(washlocations.Washes[0].StoreTimeOut);
                clockDetails.Text = StartTime.TimeOfDay.ToString() + " to " + EndTime.TimeOfDay.ToString();
            }
            //clockDetails.Text = "11 am to 8 pm"; //data.StartTime +" "+"to"+ " " +data.EndTime;
        }
        private Bitmap getMarkerBitmapFromView(LocationStatus locationStatus, int resId)
        {

            View customMarkerView = LayoutInflater.Inflate(Resource.Layout.MarkerInfoWindow, null, false);//((LayoutInflater)getSystemService(Context.LayoutInflaterService)).inflate(Resource.Layout.MarkerInfoWindow, null);
            LinearLayout markerView = (LinearLayout)customMarkerView.FindViewById(Resource.Id.markerView);
            TextView markerWashTimes = (TextView)customMarkerView.FindViewById(Resource.Id.markerWashTimes);
            TextView openTitle = (TextView)customMarkerView.FindViewById(Resource.Id.openTitle);
            ImageView markerWindowIcon = (ImageView)customMarkerView.FindViewById(Resource.Id.markerWindowIcon);
            TextView washTiming = (TextView)customMarkerView.FindViewById(Resource.Id.washTiming);

            markerWashTimes.Text = locationStatus.LocationName.ToString();
            if (locationStatus.StoreStatus == "Open")
            {
                openTitle.Text = locationStatus.StoreStatus.ToString();
                washTiming.Text = locationStatus.WashtimeMinutes.ToString() + " " + "Mins";
            }
            else if (locationStatus.StoreStatus == null)
            {
                washTiming.Text = "0 Mins";
                openTitle.Text = "closed";

            }
            else
            {
                washTiming.Text = "0 Mins";
                openTitle.Text = locationStatus.StoreStatus.ToString();
            }

            markerView.SetBackgroundResource(resId);
            markerWindowIcon.SetBackgroundResource(Resource.Drawable.Icon_car_wash);
            customMarkerView.Measure((int)View.MeasureSpec.GetMode(0), (int)View.MeasureSpec.GetMode(0));
            customMarkerView.Layout(0, 0, customMarkerView.MeasuredWidth, customMarkerView.MeasuredHeight);
            customMarkerView.BuildDrawingCache();
            Bitmap returnedBitmap = Bitmap.CreateBitmap(customMarkerView.MeasuredWidth, customMarkerView.MeasuredHeight,
                    Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(returnedBitmap);
            canvas.DrawColor(Color.Red, PorterDuff.Mode.SrcIn);
            Drawable drawable = customMarkerView.Background;
            if (drawable != null)
                drawable.Draw(canvas);
            customMarkerView.Draw(canvas);

            return returnedBitmap;
        }
        private void addCarwashGeoFence(LatLng[] latlngs, float Radius)
        {
            geofencesCount = 0;
            carWashGeofences = new List<IGeofence>();
            foreach (var latlng in latlngs)
            {
                if (latlng != null)
                {
                    carWashGeofences.Add(geofenceHelper.getGeofence(GeofenceID + geofencesCount, latlng, Radius, Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionDwell | Geofence.GeofenceTransitionExit));
                }
                geofencesCount++;
            }
            if (carWashGeofences.Count > 0)
            {
                geofencingRequests = geofenceHelper.GetGeofencingRequests(carWashGeofences);

            }
            geoPendingIntent = geofenceHelper.getPendingIntent();
            geofencingClient.AddGeofences(geofencingRequests, geoPendingIntent);
            addGeoCircles(latlngs, Radius);
        }
        private void addGeoCircles(LatLng[] latLngs, float Radius)
        {
            geofenceCirclesCount = 0;
            geofenceCircles = new CircleOptions[latLngs.Length];
            foreach (var latlng in latLngs)
            {
                if (latlng != null)
                {
                    geofenceCircles[geofenceCirclesCount] = new CircleOptions();
                    geofenceCircles[geofenceCirclesCount].InvokeCenter(latlng);
                    geofenceCircles[geofenceCirclesCount].InvokeRadius(Radius);
                    geofenceCircles[geofenceCirclesCount].InvokeStrokeColor(Color.Argb(255, 144, 224, 221));
                    geofenceCircles[geofenceCirclesCount].InvokeFillColor(Color.Argb(100, 198, 223, 221));
                    geofenceCircles[geofenceCirclesCount].InvokeStrokeWidth(4);
                    Googlemap.AddCircle(geofenceCircles[geofenceCirclesCount]);
                }
            }



        }
        private void loadFirstMarkerData()
        {
            locationName.Text = washlocations.Washes[0].LocationName;
            locationDetails.Text = washlocations.Washes[0].Address1;
            phoneDetails.Text = washlocations.Washes[0].PhoneNumber;
            mailDetails.Text = washlocations.Washes[0].Email;
            if (washlocations.Washes[0].StoreTimeIn != null)
            {
                DateTime StartTime = DateTime.Parse(washlocations.Washes[0].StoreTimeIn);
                DateTime EndTime = DateTime.Parse(washlocations.Washes[0].StoreTimeOut);
                clockDetails.Text = StartTime.TimeOfDay.ToString() + " to " + EndTime.TimeOfDay.ToString();
            }
        }


        public void OnConnected(Bundle connectionHint)
        {
            
        }

        public void OnConnectionSuspended(int cause)
        {
            
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            googleAPI.Reconnect();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            
        }
    }
}
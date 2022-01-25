using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Services;
using Android.Locations;
using Android.Graphics;
using Android.Gms.Tasks;
using Xamarin.Essentials;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Location = Android.Locations.Location;
using ILocationListener = Android.Gms.Location.ILocationListener;
using System.Timers;
using Task = System.Threading.Tasks.Task;
using Provider = Android.Provider;
using static Android.Gms.Maps.GoogleMap;
using Strive.Core.Models.Customer;
using Double = System.Double;
using Android.Graphics.Drawables;

namespace StriveCustomer.Android.Fragments
{
    public class MapsFragment : MvxFragment<MapViewModel>, IOnMapReadyCallback, IOnSuccessListener, IConnectionCallbacks, IOnConnectionFailedListener, ILocationListener, IInfoWindowAdapter
    {
        int markerClickCount = 0;
        string GeofenceID = "LatLng";
        bool locationEnabled, networkEnabled;
        string[] GeofenceIDS;
        private int carWashLocationsCount, geofencesCount, geofenceCirclesCount, latLngCount;
        IList<IGeofence> carWashGeofences;
        private GoogleMap Googlemap;
        private GeofencingClient geofencingClient;
        private GoogleApiClient googleAPI;
        private Location lastLocation;
        private LocationManager locationManager;
        private LatLng userLatLng, prevSelectedMarker;
        private LatLng[] carWashLatLng;
        private MarkerOptions userMarkerOption;
        private MarkerOptions[] carWashMarkerOptions;
        private CircleOptions[] geofenceCircles;
        private LocationRequest userLocationRequest;
        private GeofencingRequest geofencingRequests;
        private GeofenceHelper geofenceHelper;
        private SupportMapFragment gmaps;
        //private CarWashLocation carWashLocations;
        private PendingIntent geoPendingIntent;
        private Timer refreshWashTimer;
        private static View rootView, markerInfoWindow;
        private washLocations carWashLocations = new washLocations();
        private List<Double> distanceList;
        private Dictionary<int, double> dict = new Dictionary<int, double>();
        private Dictionary<string, object> markerDic;
        private Marker[] marker;
        private float preZIndex;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (googleAPI == null)
            {
                googleAPI = new GoogleApiClient.Builder(this.Context)
                        .AddApi(LocationServices.API)
                        .AddConnectionCallbacks(this)
                        .AddOnConnectionFailedListener(this)
                        .Build();
                googleAPI.Connect();
            }

            locationManager = (LocationManager)Context.GetSystemService(Context.LocationService);
            checkLocationEnabled();

            CustomerInfo.setMapInfo();
            geofencingClient = LocationServices.GetGeofencingClient(this.Context);
            geofenceHelper = new GeofenceHelper(this.Context);

        }

        public override void OnStart()
        {
            base.OnStart();
            googleAPI.Reconnect();
        }
        public override void OnStop()
        {
            base.OnStop();
            googleAPI.Disconnect();
        }
        private void checkLocationEnabled()
        {
            locationEnabled = locationManager.IsProviderEnabled(LocationManager.GpsProvider);
            networkEnabled = locationManager.IsProviderEnabled(LocationManager.NetworkProvider);
            if (!locationEnabled || !networkEnabled)
            {
                new AlertDialog.Builder(Context)
                    .SetMessage("To use this functionality, enable location services from settings in your device")
                    .SetPositiveButton("Enable", (a, ev) => { Context.StartActivity(new Intent(Provider.Settings.ActionLocationSourceSettings)); })
                    .Show();
            }

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.ViewModel = new MapViewModel();
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
                    rootView = inflater.Inflate(Resource.Layout.MapScreenFragment, container, false);
                }
                else
                {
                    return rootView;
                }
            }
            catch (InflateException e)
            {
                return rootView;
            }
            return rootView;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }
        public override void OnResume()
        {
            base.OnResume();
            if (gmaps == null)
            {

            }
            else
                return;
        }
        public async void OnMapReady(GoogleMap googleMap)
        {
            Googlemap = googleMap;
            Googlemap.UiSettings.CompassEnabled = false;
            await AndroidPermissions.checkLocationPermission(this);
            Googlemap.SetInfoWindowAdapter(this);
            if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.AccessFineLocation) == Permission.Granted)// && googleAPI.IsConnected)
            {
                enableUserLocation();
                lastUserLocation();
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 10001);
            }

            if (carWashLocations != null)
            {
                Googlemap.MyLocationButtonClick += Googlemap_MyLocationButtonClick;
                setUpMarkers(); 
                _ = setLocationsOnMapAsync();
                //RefreshWashTimes();
            }
        }
        private async Task setLocationsOnMapAsync()
        {
            int seconds = 60000;
            while (seconds != 0)
            {
                await Task.Delay(seconds);
                var locations = await ViewModel.GetAllLocationStatus();
                if (locations != null)
                {
                    if (locations?.Washes.Count == 0)
                    {
                        carWashLocations = null;
                    }
                    else
                    {
                        carWashLocations = locations;
                    }
                }
                setUpMarkers();
                

            }
        }
        private void Googlemap_MyLocationButtonClick(object sender, GoogleMap.MyLocationButtonClickEventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                lastUserLocation();
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 10001);
            }
        }
        private async void setUpMaps()
        {
            //var locations = await ViewModel.GetAllLocationsCommand();
            var locations = await ViewModel.GetAllLocationStatus();
            if (locations != null)
            {
                if (locations?.Washes.Count == 0)
                {
                    carWashLocations = null;
                }
                else
                {
                    carWashLocations = locations;
                }
            }
            if (!IsAdded)
            {
                return;
            }
            else
            {
                gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.gmaps);

            }
            if (gmaps != null)
            {
                gmaps.GetMapAsync(this);
            }
        }
        private void enableUserLocation()
        {
            if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                Googlemap.MyLocationEnabled = true;
                Googlemap.UiSettings.MyLocationButtonEnabled = true;
                Googlemap.MarkerClick += Googlemap_MarkerClick;

            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 10001);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode ==  10001 || requestCode ==99)
            {
                // Check if the only required permission has been granted
                if ((grantResults.Length == 1) && (grantResults[0] == Permission.Granted))
                {
                    setUpMarkers();
                    Console.WriteLine("Map location");
                } 
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }
        private void setUpMarkers()
        {
            carWashLocationsCount = 0;

            distanceList = new List<double>();
            dict.Clear();
            foreach (var carWashLocation in carWashLocations.Washes)
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
            preZIndex = 2;
            foreach (var item in dict)
            {
                var shortLoc = carWashLocations.Washes.Find(location => location.LocationId == item.Key);
                carWashLatLng[latLngCount] = new LatLng((double)shortLoc.Latitude, (double)shortLoc.Longitude);
                marker[latLngCount] = Googlemap.AddMarker(new MarkerOptions().SetPosition(carWashLatLng[latLngCount]).InvokeZIndex(dict.Count-1-latLngCount)
                         .SetIcon(BitmapDescriptorFactory.FromBitmap(getMarkerBitmapFromView(shortLoc, Resource.Drawable.bubblePopWindow))));
                marker[latLngCount].Tag = carWashLatLng[latLngCount];
                Googlemap.AnimateCamera(CameraUpdateFactory.ZoomTo(10.0f));
                Googlemap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(carWashLatLng[latLngCount], 16));
                //Googlemap.MoveCamera(CameraUpdateFactory.NewLatLng(carWashLatLng[latLngCount]));
                if (!markerDic.ContainsKey(marker[latLngCount].Id))
                {
                    markerDic.Add(marker[latLngCount].Id, marker[latLngCount].Tag);
                }
                latLngCount++;

            }
            addCarwashGeoFence(carWashLatLng,280f);
        }

       async void getDistance(double lat, double lon, int id)
        {
            double latEnd = lat;
            double lngEnd = lon;

            try
            {
                var currentLocation = await Geolocation.GetLastKnownLocationAsync();
                if (currentLocation == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    var location = await Geolocation.GetLocationAsync(request);
                    double dist = (double)(location?.CalculateDistance(latEnd, lngEnd, DistanceUnits.Miles));
                    if (!dict.ContainsKey(id))
                    {
                        dict.Add(id, dist);
                        distanceList.Add(dist);
                    }
                }
                else
                {
                    double dist = (double)(currentLocation?.CalculateDistance(latEnd, lngEnd, DistanceUnits.Miles));
                    if (!dict.ContainsKey(id))
                    {
                        dict.Add(id, dist);
                        distanceList.Add(dist);
                    }
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
        async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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
            catch (Exception ex)
            {
                // Unable to get location
            }
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

        

        private async void Googlemap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            try
            {
                var latLngValues = markerDic[(e.Marker.Id)] ?? "default";
                e.Marker.ZIndex = e.Marker.ZIndex + preZIndex;
                preZIndex++;
                LatLng markerlatlng = (LatLng)latLngValues;
                await Map.OpenAsync(markerlatlng.Latitude, markerlatlng.Longitude);
            }
            catch(KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

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
                    geofenceCircles[geofenceCirclesCount].InvokeFillColor(Color.Argb(255, 198, 223, 221));
                    geofenceCircles[geofenceCirclesCount].InvokeStrokeWidth(4);
                    Googlemap.AddCircle(geofenceCircles[geofenceCirclesCount]);
                }
            }

      

        }


        private void lastUserLocation()
        {
            lastLocation = LocationServices.FusedLocationApi.GetLastLocation(googleAPI);

            if (lastLocation != null)
            {
                userLatLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
                userMarkerOption = new MarkerOptions();
                userMarkerOption.SetPosition(userLatLng);
                //userMarker = Googlemap.AddMarker(userMarkerOption);
                Googlemap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(userLatLng, 15));

            }
            userLocationRequest = new LocationRequest();
            userLocationRequest.SetInterval(5000);
            userLocationRequest.SetFastestInterval(3000);
            userLocationRequest.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);
            if(googleAPI.IsConnected)
            LocationServices.FusedLocationApi.RequestLocationUpdates(googleAPI, userLocationRequest, this);
        }
        public void OnLocationChanged(Location location)
        {
            if (lastLocation != null)
            {
                userLatLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
                userMarkerOption = new MarkerOptions();
                userMarkerOption.SetPosition(userLatLng);
            }
        }
       
        public View GetInfoContents(Marker marker)
        {
            return null;
        }
        public View GetInfoWindow(Marker marker)
        {
            
            return null;
        }
        public void OnConnected(Bundle connectionHint)
        {
            if(googleAPI.IsConnected)
            setUpMaps();
        }
        public void OnConnectionSuspended(int cause)
        {
            //
        }
        public void OnConnectionFailed(ConnectionResult result)
        {
            googleAPI.Reconnect();
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            //
        }

        
    }
}
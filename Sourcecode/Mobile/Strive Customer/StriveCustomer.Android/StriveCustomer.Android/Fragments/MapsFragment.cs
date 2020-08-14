using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Android.Runtime;
using Android.Support.V13.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Locations = Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Services;
using Android.Locations;
using Android.Graphics;
using Android.Gms.Tasks;
using Xamarin.Essentials;
using static Android.Gms.Common.Apis.GoogleApiClient;
using Location = Android.Locations.Location;
using ILocationListener = Android.Gms.Location.ILocationListener;
using CarWashLocation = Strive.Core.Models.TimInventory.Location;
using System.Timers;
using Task = System.Threading.Tasks.Task;

namespace StriveCustomer.Android.Fragments
{
    public class MapsFragment : MvxFragment<MapViewModel>,IOnMapReadyCallback,IOnSuccessListener,IConnectionCallbacks, IOnConnectionFailedListener, ILocationListener
    {
        float Radius = 1500;
        string GeofenceID = "LatLng";
        string[] GeofenceIDS;
        private int carWashLocationsCount , geofencesCount, geofenceCirclesCount;
        IList<IGeofence> carWashGeofences;
        private GoogleMap Googlemap;
        private GeofencingClient geofencingClient;
        private GoogleApiClient googleAPI;
        private Location lastLocation;
        private LatLng userLatLng;
        private LatLng[] carWashLatLng;
        private MarkerOptions userMarkerOption;
        private MarkerOptions[] carWashMarkerOptions;
        private CircleOptions[] geofenceCircles;
        private LocationRequest userLocationRequest;
        private GeofencingRequest geofencingRequests;
        private GeofenceHelper geofenceHelper;
        private SupportMapFragment gmaps;
        private CarWashLocation carWashLocations;
        private PendingIntent geoPendingIntent;
        private Timer refreshWashTimer;
        private static View rootView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            googleAPI = new GoogleApiClient.Builder(this.Context)
                        .AddApi(LocationServices.API)
                        .AddConnectionCallbacks(this)
                        .AddOnConnectionFailedListener(this)
                        .Build();
                        
            googleAPI.Connect();

            geofencingClient = LocationServices.GetGeofencingClient(this.Context);
            geofenceHelper = new GeofenceHelper(this.Context);
           
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
                    setUpMaps();
                }
                else
                {      
                    return rootView;
                }
            }
            catch(InflateException e)
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
        public void OnMapReady(GoogleMap googleMap)
        {
            Googlemap = googleMap;
            Googlemap.MyLocationButtonClick += Googlemap_MyLocationButtonClick;
            enableUserLocation( );
            RefreshWashTimes();
            setUpMarkers();
            addCarwashGeoFence(carWashLatLng,Radius);
        }
        private void Googlemap_MyLocationButtonClick(object sender, GoogleMap.MyLocationButtonClickEventArgs e)
        {
            lastUserLocation();
        }
        private async void setUpMaps()
        {
            carWashLocations = await ViewModel.GetAllLocationsCommand();
            gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.gmaps);
            if (gmaps != null)
            {
               gmaps.GetMapAsync(this);
            }
        }
        private void enableUserLocation()
        {
            if(ContextCompat.CheckSelfPermission(Context,Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                Googlemap.MyLocationEnabled = true;
                Googlemap.UiSettings.MyLocationButtonEnabled = true;
                //Googlemap.MarkerClick += Googlemap_MarkerClick;
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 10001);
            }
        }
        private void setUpMarkers()
        {
            carWashLocationsCount = 0;
            carWashLatLng = new LatLng[carWashLocations.LocationAddress.Count];
            carWashMarkerOptions = new MarkerOptions[carWashLocations.LocationAddress.Count];
            foreach (var carWashLocation in carWashLocations.LocationAddress)
            {
                carWashLatLng[carWashLocationsCount] = new LatLng(carWashLocation.Latitude, carWashLocation.Longitude);
                carWashMarkerOptions[carWashLocationsCount] = new MarkerOptions().SetPosition(carWashLatLng[carWashLocationsCount]).SetTitle(carWashLocation.WashTiming);
                if (carWashLocationsCount == 3)
                {
                    carWashLatLng[3] = new LatLng(Convert.ToDouble(13.123282872991561), Convert.ToDouble(80.20491600036623));
                    carWashMarkerOptions[3] = new MarkerOptions().SetPosition(carWashLatLng[3]).SetTitle(carWashLocation.WashTiming);
                }
                Googlemap.AddMarker(carWashMarkerOptions[carWashLocationsCount]).ShowInfoWindow();
                carWashLocationsCount++;
            }
        }
        //private async void Googlemap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        //{
        //    LatLng markerlatlng = e.Marker.Position;
        //    await Map.OpenAsync(markerlatlng.Latitude,markerlatlng.Longitude);
        //}
        private void addCarwashGeoFence(LatLng[] latlngs, float Radius)
        {
            geofencesCount = 0;
            carWashGeofences = new List<IGeofence>();
            foreach(var latlng in latlngs)
            {
                carWashGeofences.Add(geofenceHelper.getGeofence(GeofenceID+geofencesCount,latlng,Radius, Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionDwell | Geofence.GeofenceTransitionExit));
                geofencesCount++; 
            }
            geofencingRequests = geofenceHelper.GetGeofencingRequests(carWashGeofences);
            geoPendingIntent = geofenceHelper.getPendingIntent();
            geofencingClient.AddGeofences(geofencingRequests,geoPendingIntent);
            addGeoCircles(latlngs,Radius);
        }
        private void addGeoCircles(LatLng[] latLngs, float Radius)
        {
            geofenceCirclesCount = 0;
            geofenceCircles = new CircleOptions[latLngs.Length];
            foreach (var latlng in latLngs )
            {
                geofenceCircles[geofenceCirclesCount] = new CircleOptions();
                geofenceCircles[geofenceCirclesCount].InvokeCenter(latlng);
                geofenceCircles[geofenceCirclesCount].InvokeRadius(Radius);
                geofenceCircles[geofenceCirclesCount].InvokeStrokeColor(Color.Argb(255, 255, 0, 0));
                geofenceCircles[geofenceCirclesCount].InvokeFillColor(Color.Argb(64, 255, 0, 0));
                geofenceCircles[geofenceCirclesCount].InvokeStrokeWidth(4);
                Googlemap.AddCircle(geofenceCircles[geofenceCirclesCount]);

            }
        }
        private void lastUserLocation()
        {
             lastLocation = LocationServices.FusedLocationApi.GetLastLocation(googleAPI);
           
            if(lastLocation != null)
            {
                userLatLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
                userMarkerOption = new MarkerOptions();
                userMarkerOption.SetPosition(userLatLng);   
                //userMarker = Googlemap.AddMarker(userMarkerOption);
                Googlemap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(userLatLng,15));
                
            }
            userLocationRequest = new LocationRequest();
            userLocationRequest.SetInterval(5000);
            userLocationRequest.SetFastestInterval(3000);
            userLocationRequest.SetPriority(LocationRequest.PriorityBalancedPowerAccuracy);

            LocationServices.FusedLocationApi.RequestLocationUpdates(googleAPI,userLocationRequest,this);
        }
        public void OnLocationChanged(Location location)
        {
            userLatLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
            userMarkerOption = new MarkerOptions();
            userMarkerOption.SetPosition(userLatLng);
        }
        private void RefreshWashTimes()
        {
            refreshWashTimer = new Timer(10000);
            refreshWashTimer.AutoReset = true;
            refreshWashTimer.Elapsed += RefreshWashTimer_Elapsed;
            refreshWashTimer.Start();
        }
        private async void RefreshWashTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            carWashLocations = await ViewModel.GetAllLocationsCommand();

            carWashLocationsCount = 0;
            carWashLatLng = new LatLng[carWashLocations.LocationAddress.Count];
            carWashMarkerOptions = new MarkerOptions[carWashLocations.LocationAddress.Count];
            foreach (var carWashLocation in carWashLocations.LocationAddress)
            {
                carWashLatLng[carWashLocationsCount] = new LatLng(carWashLocation.Latitude, carWashLocation.Longitude);
                carWashMarkerOptions[carWashLocationsCount] = new MarkerOptions().SetPosition(carWashLatLng[carWashLocationsCount]).SetTitle(carWashLocation.WashTiming);
                if (carWashLocationsCount == 3)
                {
                    carWashLatLng[3] = new LatLng(Convert.ToDouble(13.123282872991561), Convert.ToDouble(80.20491600036623));
                    carWashMarkerOptions[3] = new MarkerOptions().SetPosition(carWashLatLng[3]).SetTitle(carWashLocation.WashTiming);
                }
                carWashLocationsCount++;
            }

        }

        public void OnConnected(Bundle connectionHint)
        {
            //
        }
        public void OnConnectionSuspended(int cause)
        {
            //
        }
        public void OnConnectionFailed(ConnectionResult result)
        {
            //
        }
        public void OnSuccess(Java.Lang.Object result)
        {
            //
        }
    }
}
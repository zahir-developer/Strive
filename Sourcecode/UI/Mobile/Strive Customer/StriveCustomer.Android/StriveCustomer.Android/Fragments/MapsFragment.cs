using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Services;
using static Android.Gms.Common.Apis.GoogleApiClient;

namespace StriveCustomer.Android.Fragments
{
    public class MapsFragment : MvxFragment<MapViewModel>,IOnMapReadyCallback,IResultCallback, IConnectionCallbacks, IOnConnectionFailedListener
    {
        private GoogleMap Googlemap;
        private GoogleApiClient googleAPI;
        private Circle geofenceCircle;
        private CircleOptions[] circleOptions;
        private IList<IGeofence> geofencesall;
        private GeofencingRequest geofencingRequest;
        private Geofence geofence;
        private GeofenceBuilder geofenceBuilder;
        private Location locations;
        private PendingIntent geofencingPendingIntent;
        private SupportMapFragment gmaps;
        private static View rootView;
        private LatLng[] latlngs;
        private MarkerOptions[] markers;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            googleAPI = new GoogleApiClient.Builder(this.Context)
                        .AddApi(LocationServices.API)
                        .AddConnectionCallbacks(this)
                        .AddOnConnectionFailedListener(this)
                        .Build();
            googleAPI.Connect();
            // Create your fragment here
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
            var count = 0;
            latlngs = new LatLng[locations.LocationAddress.Count];
            markers = new MarkerOptions[locations.LocationAddress.Count];
            
            foreach(var location in locations.LocationAddress)
            {
                latlngs[count] = new LatLng(location.Latitude,location.Longitude);
                markers[count] = new MarkerOptions().SetPosition(latlngs[count]).SetTitle(location.WashTiming);
                googleMap.AddMarker(markers[count]);
               
                count++;
            }
            startGeoFence(latlngs);
        }
        private void startGeoFence(LatLng[] latlngs)
        {
            if(markers != null)
            {
                geofencesall = createGeoFence(latlngs , 400f);
                geofencingRequest = createGeoFenceRequest(geofencesall);
                addGeoFences(geofencesall);

            }
        }
        public void OnResult(Java.Lang.Object result)
        {
            drawGeoFences();
        }
        private void drawGeoFences()
        {
            if(geofenceCircle != null)
            {
                geofenceCircle.Remove();
            }
            circleOptions = new CircleOptions[locations.LocationAddress.Count];
            var circlesOptionCount = 0;
            foreach(var latlng in latlngs)
            {
                circleOptions[circlesOptionCount] = new CircleOptions();
                circleOptions[circlesOptionCount].InvokeCenter(latlng).InvokeRadius(400).InvokeFillColor(0X66FF0000);
                geofenceCircle = Googlemap.AddCircle(circleOptions[circlesOptionCount]);
            }
            
        }

        private void addGeoFences(IList<IGeofence> geofencesall)
        {
            if (googleAPI.IsConnected)
                LocationServices.GeofencingApi.AddGeofences(googleAPI, geofencesall, createGeoFencePendingIntent()).SetResultCallback(this);
            else
                googleAPI.Reconnect();
        }
        private PendingIntent createGeoFencePendingIntent()
        {
            if(geofencingPendingIntent != null)
            {
                return geofencingPendingIntent;
            }
            Intent geoIntent = new Intent(this.Context, typeof(GeoFencingTransitionService));

            return PendingIntent.GetService(this.Context,0,geoIntent,PendingIntentFlags.UpdateCurrent);
        }
        private GeofencingRequest createGeoFenceRequest(IList<IGeofence> geofences)
        {
            return new GeofencingRequest.Builder()
                .SetInitialTrigger(GeofencingRequest.InitialTriggerEnter)
                .AddGeofences(geofences)
                .Build();
        }

        private IList<IGeofence> createGeoFence(LatLng[] latlngs, float radius)
        {
            IList<IGeofence> createdGeoFences = new List<IGeofence>();
            int geofencecount = 0;
            foreach(var latlng in latlngs)
            {
                var geofences = new GeofenceBuilder().SetTransitionTypes(Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionExit)
                                           .SetRequestId("Geofence"+geofencecount)
                                           .SetCircularRegion(latlng.Latitude, latlng.Longitude, radius)
                                           .SetExpirationDuration(Geofence.NeverExpire)
                                           .Build();
                createdGeoFences.Add(geofences);
                geofencecount++;
            }
            return createdGeoFences;
        }

        private async void setUpMaps()
        {
            locations = await ViewModel.GetAllLocationsCommand();
            
            gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.gmaps);
            if (gmaps != null)
            {
               gmaps.GetMapAsync(this);
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
    }
}
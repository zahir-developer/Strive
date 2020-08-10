﻿using System;
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
using static Android.Gms.Common.Apis.GoogleApiClient;
using Android.Locations;
using ILocationListener = Android.Gms.Location.ILocationListener;
using Android.Graphics;
using Android.Gms.Tasks;

namespace StriveCustomer.Android.Fragments
{
    public class MapsFragment : MvxFragment<MapViewModel>,IOnMapReadyCallback,IOnSuccessListener,IConnectionCallbacks, IOnConnectionFailedListener, ILocationListener
    {
        float Radius = 1500;
        string GeofenceID = "SOMEID1";
        private GoogleMap Googlemap;
        private GeofencingClient geofencingClient;
        private GoogleApiClient googleAPI;
        private Location lastLocation;
        private LatLng userLatLng;
        private MarkerOptions userMarkerOption;
        private MarkerOptions geofenceMarkerOptions;
        private CircleOptions geofenceCircleOptions;
        private LocationRequest userLocationRequest;
        private GeofenceHelper geofenceHelper;
        private SupportMapFragment gmaps;
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
        }

        private void Googlemap_MyLocationButtonClick(object sender, GoogleMap.MyLocationButtonClickEventArgs e)
        {
            lastUserLocation();
        }

        private async void setUpMaps()
        {
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
                Googlemap.MapLongClick += Googlemap_MapLongClick;
            }
            else
            {
                RequestPermissions(new[] { Manifest.Permission.AccessFineLocation }, 10001);
            }
        }

        private void Googlemap_MapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            Googlemap.Clear();
            addMarker(new LatLng(e.Point.Latitude,e.Point.Longitude));
            addGeoCircle(new LatLng(e.Point.Latitude, e.Point.Longitude),Radius);
            addGeofence(new LatLng(e.Point.Latitude, e.Point.Longitude), Radius);
        }
        private void addGeofence(LatLng latlng, float Radius)
        {
            IGeofence geofence = geofenceHelper.getGeofence(GeofenceID,latlng,Radius,Geofence.GeofenceTransitionEnter|Geofence.GeofenceTransitionDwell|Geofence.GeofenceTransitionExit);
            GeofencingRequest geofencingRequest = geofenceHelper.GetGeofencingRequest(geofence);
            PendingIntent pendingIntent = geofenceHelper.getPendingIntent();
            geofencingClient.AddGeofences(geofencingRequest,pendingIntent).AddOnSuccessListener(this);
        }
        private void addMarker(LatLng latLng)
        {
            geofenceMarkerOptions = new MarkerOptions().SetPosition(latLng);
            Googlemap.AddMarker(geofenceMarkerOptions);
        }
        private void addGeoCircle(LatLng latLng, float Radius)
        {
            geofenceCircleOptions = new CircleOptions();
            geofenceCircleOptions.InvokeCenter(latLng);
            geofenceCircleOptions.InvokeRadius(Radius);
            geofenceCircleOptions.InvokeStrokeColor(Color.Argb(255,255,0,0));
            geofenceCircleOptions.InvokeFillColor(Color.Argb(64,255,0,0));
            geofenceCircleOptions.InvokeStrokeWidth(4);
            Googlemap.AddCircle(geofenceCircleOptions);
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

        public void OnLocationChanged(Location location)
        {
            userLatLng = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
            userMarkerOption = new MarkerOptions();
            userMarkerOption.SetPosition(userLatLng);       
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Tasks;
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
using static Android.Gms.Common.Apis.GoogleApiClient;

namespace StriveCustomer.Android.Fragments
{
    public class ContactUsFragment : MvxFragment<ContactUsViewModel>, IOnMapReadyCallback, IOnSuccessListener, IConnectionCallbacks, IOnConnectionFailedListener
    {
        private GoogleMap Googlemap;
        private Locations locations;
        private GoogleApiClient googleAPI;
        private SupportMapFragment gmaps;
        private int carWashLocationsCount;
        private static View rootView;
        private LatLng[] carWashLatLng;
        private MarkerOptions[] carWashMarkerOptions;
        private TextView locationDetails;
        private TextView phoneDetails;
        private TextView mailDetails;
        private TextView clockDetails;
        private TextView locationName;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.ViewModel = new ContactUsViewModel();
            locations = new Locations();
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
                    setupMaps();
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

        public async void OnMapReady(GoogleMap googleMap)
        {
            Googlemap = googleMap;
            await AndroidPermissions.checkLocationPermission(this);
            setUpMarkers();
            loadFirstMarkerData();
            Googlemap.MarkerClick += Googlemap_MarkerClick; 
        }

        private void setUpMarkers()
        {
            carWashLocationsCount = 0;
            carWashLatLng = new LatLng[locations.Location.Count];
            carWashMarkerOptions = new MarkerOptions[locations.Location.Count];
            foreach (var carWashLocation in locations.Location)
            {
                carWashLatLng[carWashLocationsCount] = new LatLng((double)carWashLocation.Latitude, (double)carWashLocation.Longitude);
                carWashMarkerOptions[carWashLocationsCount] = new MarkerOptions().SetPosition(carWashLatLng[carWashLocationsCount]).SetTitle(carWashLocation.WashTimeMinutes.ToString());
            }
            Googlemap.AddMarker(carWashMarkerOptions[carWashLocationsCount]);
            carWashLocationsCount++;
        }
        public async void setupMaps()
        {
            var allLocations = await this.ViewModel.GetAllLocationsCommand();
            if (allLocations.Location.Count == 0)
            {
                locations = null;
            }
            else
            {
                locations = allLocations;
            }
            gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.contactUsMaps);
            if (gmaps != null)
            {
                gmaps.GetMapAsync(this);
            }
            
        }
        private async void Googlemap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
           var data = locations.Location.Find(x => (double)x.Latitude == e.Marker.Position.Latitude);
            locationName.Text = data.LocationName;
            locationDetails.Text = data.Address1;
            phoneDetails.Text = data.PhoneNumber;
            mailDetails.Text = data.Email;
            clockDetails.Text = data.StartTime +"to"+data.EndTime;
        }
        private void loadFirstMarkerData()
        {
            locationName.Text = locations.Location[0].LocationName;
            locationDetails.Text = locations.Location[0].Address1;
            phoneDetails.Text = locations.Location[0].PhoneNumber;
            mailDetails.Text = locations.Location[0].Email;
            clockDetails.Text = locations.Location[0].StartTime + "to" + locations.Location[0].EndTime;
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
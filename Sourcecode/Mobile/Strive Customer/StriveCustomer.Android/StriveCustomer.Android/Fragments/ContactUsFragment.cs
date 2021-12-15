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
using Xamarin.Essentials;
using static Android.Gms.Common.Apis.GoogleApiClient;

namespace StriveCustomer.Android.Fragments
{
    public class ContactUsFragment : MvxFragment<ContactUsViewModel>, IOnMapReadyCallback, IOnSuccessListener, IConnectionCallbacks, IOnConnectionFailedListener
    {
        private GoogleMap Googlemap;
        private Locations Locations;
       // public washLocations locations;
        //public static washLocations washlocations;
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
        private ImageButton facebookIcon;
        private ImageButton instagramIcon;
        private ImageButton twitterIcon;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.ViewModel = new ContactUsViewModel();
            Locations = new Locations();
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
            await AndroidPermissions.checkLocationPermission(this);
            setUpMarkers();
            loadFirstMarkerData();
            Googlemap.MarkerClick += Googlemap_MarkerClick; 
        }

        private void setUpMarkers()
        {
            carWashLocationsCount = 0;
            carWashLatLng = new LatLng[Locations.Location.Count];
            carWashMarkerOptions = new MarkerOptions[Locations.Location.Count];
            foreach (var carWashLocation in Locations.Location)
            {
                carWashLatLng[carWashLocationsCount] = new LatLng((double)carWashLocation.Latitude, (double)carWashLocation.Longitude);
                carWashMarkerOptions[carWashLocationsCount] = new MarkerOptions().SetPosition(carWashLatLng[carWashLocationsCount]).SetTitle(carWashLocation.WashTimeMinutes.ToString());
                Googlemap.AddMarker(carWashMarkerOptions[carWashLocationsCount]);
                carWashLocationsCount++;
            }  
        }
        public async void setupMaps()
        {
            var allLocations = await this.ViewModel.GetAllLocationsCommand();
            if (allLocations.Location.Count == 0)
            {
                Locations = null;
            }
            else
            {
                Locations = allLocations;
            }
            //var allLocations = await ViewModel.GetAllLocationStatus();
            //if (allLocations.Washes.Count == 0)
            //{
            //    locations = null;
            //}
            //else
            //{
            //    locations = allLocations;
            //    washlocations = allLocations;
            //}
            gmaps = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.contactUsMaps);
            if (gmaps != null)
            {
                gmaps.GetMapAsync(this);
            }
            
        }
        private async void Googlemap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
           var data = Locations.Location.Find(x => (double)x.Latitude == e.Marker.Position.Latitude);
            locationName.Text = data.LocationName;
            locationDetails.Text = data.Address1;
            phoneDetails.Text = data.PhoneNumber;
            mailDetails.Text = data.Email;
            clockDetails.Text = "11 am to 8 pm"; //data.StartTime +" "+"to"+ " " +data.EndTime;
        }
        private void loadFirstMarkerData()
        {
            locationName.Text = Locations.Location[0].LocationName;
            locationDetails.Text = Locations.Location[0].Address1;
            phoneDetails.Text = Locations.Location[0].PhoneNumber;
            mailDetails.Text = Locations.Location[0].Email;
            clockDetails.Text = Locations.Location[0].StartTime + "to" + Locations.Location[0].EndTime;
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
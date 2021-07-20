using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;

namespace StriveOwner.Android.Resources.Fragments
{
    public class DashboardHomeFragment : MvxFragment<DashboardHomeViewModel>
    {
        private TextView TempTextView;
        private LinearLayout locationsLayout;
        private TabLayout dashhome_TabLayout;
        private ViewPager dashhome_ViewPager;
        private ViewPagerAdapter dashhome_ViewPagerAdapter;
        private ServicesFragment servicesFragment;
        private SalesFragment salesFragment;
        private RevenueFragment revenueFragment;
        private ImageView bay1_expand;
        private TextView bay1_timein;
        private TextView bay1_client;
        private TextView bay1_phone;
        private TextView bay1_timeout;
        private TextView bay1_makemodelcolor;
        private TextView bay1_services;
        private TextView bay1_upcharges;
        private ImageView bay2_expand;
        private TextView bay2_timein;
        private TextView bay2_client;
        private TextView bay2_phone;
        private TextView bay2_timeout;
        private TextView bay2_makemodelcolor;
        private TextView bay2_services;
        private TextView bay2_upcharges;
        private ImageView bay3_expand;
        private TextView bay3_timein;
        private TextView bay3_client;
        private TextView bay3_phone;
        private TextView bay3_timeout;
        private TextView bay3_makemodelcolor;
        private TextView bay3_services;
        private TextView bay3_upcharges;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.DashboardHome_Fragment, null);
            this.ViewModel = new DashboardHomeViewModel();

            servicesFragment = new ServicesFragment();
            salesFragment = new SalesFragment();
            revenueFragment = new RevenueFragment();
            TempTextView = rootView.FindViewById<TextView>(Resource.Id.weather_Temperature);
            TempTextView.Text = "/";
            locationsLayout = rootView.FindViewById<LinearLayout>(Resource.Id.addinglocationbuttons);
            dashhome_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.dashhome_TabLayout);
            dashhome_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.dashhome_ViewPager);
            bay1_expand = rootView.FindViewById<ImageView>(Resource.Id.bay1_button);
            bay1_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_1);
            bay1_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_1);
            bay1_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_1);
            bay1_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_1);
            bay1_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_1);
            bay1_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_1);
            bay1_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_1);
            bay2_expand = rootView.FindViewById<ImageView>(Resource.Id.bay2_button);
            bay2_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_2);
            bay2_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_2);
            bay2_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_2);
            bay2_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_2);
            bay2_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_2);
            bay2_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_2);
            bay2_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_2);
            bay3_expand = rootView.FindViewById<ImageView>(Resource.Id.bay3_button);
            bay3_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_3);
            bay3_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_3);
            bay3_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_3);
            bay3_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_3);
            bay3_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_3);
            bay3_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_3);
            bay3_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_3);


            GetLocations();
            hidebay1Details();
            hidebay2Details();
            hidebay3Details();

            return rootView;
        }

        private async void GetLocations()
        {
            await ViewModel.GetAllLocationsCommand();
            if(ViewModel.Locations.Location.Count > 0 && ViewModel.Locations != null && ViewModel.Locations.Location != null)
            {
                var BtnID = 777;
                locationsLayout.Orientation = Orientation.Vertical;
                LinearLayout row = new LinearLayout(this.Context);
                var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                var btnParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                row.LayoutParameters = layoutParams;
                foreach (var location in ViewModel.Locations.Location)
                {
                    Button locationBtn = new Button(this.Context);
                    locationBtn.SetBackgroundResource(Resource.Drawable.RoundEdge_Button);
                    var nameSplits = location.LocationName.Split(" ");
                    foreach (var name in nameSplits)
                    {
                        if(!string.Equals(name.ToUpper(), "DETAIL") || !string.Equals(name.ToUpper(), "SALON") || !string.Equals(name.ToUpper(), "MAMMOTH") )
                        locationBtn.Text += name + " ";
                    }
                    
                    btnParams.SetMargins(5, 5, 5, 5);
                    locationBtn.LayoutParameters = btnParams;                  
                    BtnID += 1;
                    locationBtn.SetTextColor(Color.ParseColor("#ffffff"));
                    locationBtn.Id = BtnID;
                    row.AddView(locationBtn);
                }
                locationsLayout.AddView(row);
            }

        }
        private void hidebay1Details()
        {
            bay1_timein.Visibility = ViewStates.Gone;
            bay1_client.Visibility = ViewStates.Gone;
            bay1_phone.Visibility = ViewStates.Gone;
            bay1_timeout.Visibility = ViewStates.Gone;
            bay1_makemodelcolor.Visibility = ViewStates.Gone;
            bay1_services.Visibility = ViewStates.Gone;
            bay1_upcharges.Visibility = ViewStates.Gone;
        }
        private void hidebay2Details()
        {
            bay2_timein.Visibility = ViewStates.Gone;
            bay2_client.Visibility = ViewStates.Gone;
            bay2_phone.Visibility = ViewStates.Gone;
            bay2_timeout.Visibility = ViewStates.Gone;
            bay2_makemodelcolor.Visibility = ViewStates.Gone;
            bay2_services.Visibility = ViewStates.Gone;
            bay2_upcharges.Visibility = ViewStates.Gone;
        }
        private void hidebay3Details()
        {
            bay3_timein.Visibility = ViewStates.Gone;
            bay3_client.Visibility = ViewStates.Gone;
            bay3_phone.Visibility = ViewStates.Gone;
            bay3_timeout.Visibility = ViewStates.Gone;
            bay3_makemodelcolor.Visibility = ViewStates.Gone;
            bay3_services.Visibility = ViewStates.Gone;
            bay3_upcharges.Visibility = ViewStates.Gone;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Service");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Sales");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Revenue");
            dashhome_ViewPager.Adapter = dashhome_ViewPagerAdapter;
            dashhome_TabLayout.SetupWithViewPager(dashhome_ViewPager);
        }
        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnDetach()
        {
            base.OnDetach();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
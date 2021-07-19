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

            GetLocations();

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
                        if(!string.Equals(name, "DETAIL") || !string.Equals(name, "SALON") || !string.Equals(name, "MAMMOTH") )
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
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Recent");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Contact");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Groups");
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
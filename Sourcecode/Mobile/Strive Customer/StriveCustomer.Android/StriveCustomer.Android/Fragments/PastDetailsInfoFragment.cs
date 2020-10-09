using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PastDetailsInfoFragment : MvxFragment<PastDetailViewModel>
    {
        TabLayout slidingTabs;
        ViewPager viewPager;
        ViewPagerAdapter adapter;
        PastDetailsPageFragment dealFrag1, dealFrag2, dealFrag3;
        private PastDetailsFragment pastDetails;
        private MyProfileInfoFragment myProfile;
        private Button backButton;
        private int pastDetailDates = 1;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }   
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PastDetailsInfoFragment, null);
            slidingTabs = rootview.FindViewById<TabLayout>(Resource.Id.pastSlidingTabs);
            viewPager = rootview.FindViewById<ViewPager>(Resource.Id.pastViewPager);
            backButton = rootview.FindViewById<Button>(Resource.Id.pastServiceBack);
            pastDetails = new PastDetailsFragment();
            myProfile = new MyProfileInfoFragment();
            
            
            
            backButton.Click += BackButton_Click;
            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MyProfileInfoNeeds.selectedTab = 2;
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfile).Commit();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            adapter = new ViewPagerAdapter(ChildFragmentManager);
            var data = CustomerInfo.pastClientServices.PastClientDetails.ElementAt(CustomerInfo.SelectedVehiclePastDetails).VehicleId;
            foreach (var result in CustomerInfo.pastClientServices.PastClientDetails)
            {
                if (data == result.VehicleId)
                {
                    if(pastDetailDates == 1)
                    {
                        dealFrag1 = new PastDetailsPageFragment(result, CustomerInfo.TotalCost[CustomerInfo.SelectedVehiclePastDetails]);
                        var splits = result.DetailVisitDate.Split('T');
                        adapter.AddFragment(dealFrag1, splits[0]);
                    }
                    if (pastDetailDates == 2)
                    {
                        dealFrag2 = new PastDetailsPageFragment(result, CustomerInfo.TotalCost[CustomerInfo.SelectedVehiclePastDetails+1]);
                        var splits = result.DetailVisitDate.Split('T');
                        adapter.AddFragment(dealFrag2, splits[0]);
                    }
                    if (pastDetailDates == 3)
                    {
                        dealFrag3 = new PastDetailsPageFragment(result, CustomerInfo.TotalCost[CustomerInfo.SelectedVehiclePastDetails+2]);
                        var splits = result.DetailVisitDate.Split('T');
                        adapter.AddFragment(dealFrag3, splits[0]);
                    }
                    pastDetailDates++;
                }

            }
            viewPager.Adapter = adapter;
            slidingTabs.SetupWithViewPager(viewPager);
        }
    }
}

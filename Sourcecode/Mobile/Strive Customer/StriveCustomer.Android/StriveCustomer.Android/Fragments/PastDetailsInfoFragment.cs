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
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
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
        DealsFragment dealFrag = new DealsFragment();
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
            return rootview;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            adapter = new ViewPagerAdapter(FragmentManager);
            adapter.AddFragment(dealFrag,"Deals");
            viewPager.Adapter = adapter;
            slidingTabs.SetupWithViewPager(viewPager);
        }
    }
}
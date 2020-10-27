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
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class MessengerFragment : MvxFragment<MessengerViewModel> 
    {
        private TabLayout messenger_TabLayout;
        private ViewPager messenger_ViewPager;
        private ViewPagerAdapter messenger_ViewPagerAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.Messenger_Fragment, null);
            this.ViewModel = new MessengerViewModel();

            messenger_TabLayout = rootview.FindViewById<TabLayout>(Resource.Id.messenger_TabLayout);
            messenger_ViewPager = rootview.FindViewById<ViewPager>(Resource.Id.messenger_ViewPager);

            return rootview;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxFragmentPresentation(typeof(DashboardViewModel),Resource.Id.content_frame)]
    [Register("strivecustomer.android.fragments.MapFragment")]
    public class MapFragment : MvxFragment<MapViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.MapScreenFragment, null);
            this.ViewModel = new MapViewModel();
            ViewModel.GetLocationsCommand();
            return rootview;
        }
    }
}
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
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleAdditionalServicesFragment : MvxFragment
    {
        private ListView additionalService;
        private AdditionalServicesAdapter additionalServicesAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleAdditionalServicesFragment, null);
            additionalService = rootview.FindViewById<ListView>(Resource.Id.additionalOptions);
            additionalServicesAdapter = new AdditionalServicesAdapter(Context,CustomerInfo.filteredList.ServicesWithPrice);
            additionalService.SetAdapter(additionalServicesAdapter);
            return rootview;
        }
    }
}
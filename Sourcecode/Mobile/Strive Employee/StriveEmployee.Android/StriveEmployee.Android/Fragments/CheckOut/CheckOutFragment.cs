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
using Strive.Core.ViewModels.Employee.CheckOut;

namespace StriveEmployee.Android.Fragments.CheckOut
{
    public class CheckOutFragment : MvxFragment<CheckOutViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.CheckOut_Fragment, null);
            this.ViewModel = new CheckOutViewModel();
            GetCheckoutDetails();
            return rootView;
        }

        private async void GetCheckoutDetails()
        {
            await ViewModel.GetCheckOutDetails();
        }
    }
}
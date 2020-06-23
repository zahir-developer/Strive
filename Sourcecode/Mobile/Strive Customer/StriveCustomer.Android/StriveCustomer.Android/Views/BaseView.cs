using System;
using Acr.UserDialogs;
using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "Base View")]
    public class BaseView : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.BaseView);

            var set = this.CreateBindingSet<BaseView, LoginViewModel>();
            set.Apply();
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.DoLogin();
        }
    }
}

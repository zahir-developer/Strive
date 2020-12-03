using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "sample View")]
    public class sampleView : MvxAppCompatActivity<SampleViewModel>
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BestSample);
        }
    }
}
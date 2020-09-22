using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    public class TermsAndConditionsFragment : MvxFragment<TermsAndConditionsViewModel>
    {
        private TextView AgreeTextView;
        private TextView DisagreeTextView;
        MyProfileInfoFragment infoFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater,container,savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.TermsAndConditionsFragment,null);
            infoFragment = new MyProfileInfoFragment();
            AgreeTextView = rootview.FindViewById<TextView>(Resource.Id.textAgree);
            DisagreeTextView = rootview.FindViewById<TextView>(Resource.Id.textDisagree);
            AgreeTextView.PaintFlags = PaintFlags.UnderlineText;
            DisagreeTextView.PaintFlags = PaintFlags.UnderlineText;
            this.ViewModel = new TermsAndConditionsViewModel();
            AgreeTextView.Click += AgreeTextView_Click;
            return rootview;
        }

        private async void AgreeTextView_Click(object sender, EventArgs e)
        {
            await ViewModel.AgreeMembership();
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
        }
    }
}
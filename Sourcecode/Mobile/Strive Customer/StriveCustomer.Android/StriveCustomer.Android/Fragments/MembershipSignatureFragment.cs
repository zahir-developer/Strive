using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using Xamarin.Controls;

namespace StriveCustomer.Android.Fragments
{
    public class MembershipSignatureFragment : MvxFragment<MembershipSignatureViewModel>
    {
        private SignaturePadView signatuerPad;
        private Button nextButton;
        private TermsAndConditionsFragment termsFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var ignore = base.OnCreateView(inflater,container,savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.MembershipSignatureFragment,null);
            termsFragment = new TermsAndConditionsFragment();
            this.ViewModel = new MembershipSignatureViewModel();
            nextButton = rootview.FindViewById<Button>(Resource.Id.signatureNext);
            signatuerPad = rootview.FindViewById<SignaturePadView>(Resource.Id.signatureView);
            signatuerPad.CaptionText = "";
            signatuerPad.SignaturePromptText = "";
            nextButton.Click += NextButton_Click;
            return rootview;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, termsFragment).Commit();
        }
    }
}
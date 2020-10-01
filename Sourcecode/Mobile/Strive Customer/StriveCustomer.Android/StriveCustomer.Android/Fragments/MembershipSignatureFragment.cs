using System;
using System.Collections.Generic;
using System.Drawing;
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
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using Xamarin.Controls;

namespace StriveCustomer.Android.Fragments
{
    public class MembershipSignatureFragment : MvxFragment<MembershipSignatureViewModel>
    {
        private SignaturePadView signatuerPad;
        private Button nextButton;
        private Button backButton;
        private Button doneButton;
        private Button cancelButton;
        private TermsAndConditionsFragment termsFragment;
        private VehicleAdditionalServicesFragment additionalServicesFragment;
        private MyProfileInfoFragment myProfileInfoFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var ignore = base.OnCreateView(inflater,container,savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.MembershipSignatureFragment,null);
            termsFragment = new TermsAndConditionsFragment();
            myProfileInfoFragment = new MyProfileInfoFragment();
            additionalServicesFragment = new VehicleAdditionalServicesFragment();
            this.ViewModel = new MembershipSignatureViewModel();
            nextButton = rootview.FindViewById<Button>(Resource.Id.signatureNext);
            backButton = rootview.FindViewById<Button>(Resource.Id.signatureBack);
            doneButton = rootview.FindViewById<Button>(Resource.Id.doneSignature);
            cancelButton = rootview.FindViewById<Button>(Resource.Id.cancelSignature);
            signatuerPad = rootview.FindViewById<SignaturePadView>(Resource.Id.signatureView);
            signatuerPad.CaptionText = "";
            signatuerPad.SignaturePromptText = "";
            nextButton.Click += DoneButton_Click;
            backButton.Click += BackButton_Click;
            doneButton.Click += DoneButton_Click;
            cancelButton.Click += CancelButton_Click;
            LoadSignature();
            return rootview;
        }

        private void LoadSignature()
        {
            if(SignatureClass.signaturePoints != null)
            {
                signatuerPad.LoadPoints(SignatureClass.signaturePoints);
            }
        }

        private async void CancelButton_Click(object sender, EventArgs e)
        {
            var result =  await ViewModel.CancelMembership();
            if (result)
            {
                signatuerPad.Clear();
                SignatureClass.signaturePoints = null;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            SignatureClass.signaturePoints = signatuerPad.Points;
            if (SignatureClass.signaturePoints == null || !(SignatureClass.signaturePoints.Length > 100))
            {
                this.ViewModel.NoSignatureError();
            }
            else
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, termsFragment).Commit();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, additionalServicesFragment).Commit();
        }

        //private void NextButton_Click(object sender, EventArgs e)
        //{
        //    AppCompatActivity activity = (AppCompatActivity)Context;
        //    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, termsFragment).Commit();
        //}
    }

    public static class SignatureClass
    {
        public static PointF[] signaturePoints { get; set; }
    }
}
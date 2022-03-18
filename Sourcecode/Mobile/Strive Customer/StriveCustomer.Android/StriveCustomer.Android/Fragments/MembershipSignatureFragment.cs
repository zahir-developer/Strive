using System;
using System.IO;
using System.Linq;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using Xamarin.Controls;
using Bitmap = Android.Graphics.Bitmap;
using PointF = System.Drawing.PointF;

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
        private PaymentScreenFragment paymentScreenFragment;
        private VehicleMembershipFragment membershipFragment;
        private MyProfileInfoFragment myProfileInfoFragment;
        private ImageView signatureImage;
        private ImageView contractView;
        private ImageView termsAndConditions;
        private LinearLayout finalTermsView;
        private LinearLayout signatureLayout;
        private Bitmap finalContractBitMap;
        private bool isFromPayment;

        public MembershipSignatureFragment(bool isFromPayment)
        {
            this.isFromPayment = isFromPayment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.MembershipSignatureFragment, null);
            termsFragment = new TermsAndConditionsFragment();
            paymentScreenFragment = new PaymentScreenFragment();
            myProfileInfoFragment = new MyProfileInfoFragment();
            membershipFragment = new VehicleMembershipFragment(this.Activity);
            this.ViewModel = new MembershipSignatureViewModel();
            nextButton = rootview.FindViewById<Button>(Resource.Id.signatureNext);
            backButton = rootview.FindViewById<Button>(Resource.Id.signatureBack);
            doneButton = rootview.FindViewById<Button>(Resource.Id.doneSignature);
            cancelButton = rootview.FindViewById<Button>(Resource.Id.cancelSignature);
            signatuerPad = rootview.FindViewById<SignaturePadView>(Resource.Id.signaturePadView);
            signatureImage = rootview.FindViewById<ImageView>(Resource.Id.signatureView);
            contractView = rootview.FindViewById<ImageView>(Resource.Id.contractPriceView);
            termsAndConditions = rootview.FindViewById<ImageView>(Resource.Id.termsConditionView);
            finalTermsView = rootview.FindViewById<LinearLayout>(Resource.Id.finalTermsView);
            signatureLayout = rootview.FindViewById<LinearLayout>(Resource.Id.signatureLayout);
            signatuerPad.CaptionText = "";
            signatuerPad.SignaturePromptText = "";
            nextButton.Click += NextButton_Click;
            nextButton.Visibility = ViewStates.Gone;
            backButton.Click += BackButton_Click;
            doneButton.Click += DoneButton_Click;
            cancelButton.Click += CancelButton_Click;
            if (!isFromPayment)
            {
                signatuerPad.Clear();
                SignatureClass.signaturePoints = null;
            }
            LoadSignature();
            return rootview;
        }

        private void LoadSignature()
        {
            if (SignatureClass.signaturePoints != null)
            {
                signatuerPad.LoadPoints(SignatureClass.signaturePoints);
            }
            else
            {
                signatuerPad.Clear();
            }
        }

        private async void CancelButton_Click(object sender, EventArgs e)
        {
            var result = await ViewModel.CancelMembership();
            if (result)
            {
                string make = MembershipDetails.vehicleMakeName;
                string model = MembershipDetails.modelName;
                string color = MembershipDetails.colorName;
                MembershipDetails.clearMembershipData();
                MyProfileInfoNeeds.selectedTab = 1;
                signatuerPad.Clear();
                SignatureClass.signaturePoints = null;
                MembershipDetails.vehicleMakeName = make;
                MembershipDetails.modelName = model;
                MembershipDetails.colorName = color;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, new MyProfileInfoFragment()).Commit();
            }
        }

        private async void DoneButton_Click(object sender, EventArgs e)
        {
            SignatureClass.signaturePoints = signatuerPad.Points;
            if (SignatureClass.signaturePoints == null || !(SignatureClass.signaturePoints.Length > 100))
            {
                this.ViewModel.NoSignatureError();
            }
            else
            {
                
                contractView.SetImageBitmap(TermsAndConditionsFragment.contractImage);                
                signatureImage.SetImageBitmap(signatuerPad.GetImage());
                signatureLayout.Visibility = ViewStates.Gone;
                finalTermsView.Visibility = ViewStates.Visible;
                nextButton.Visibility = ViewStates.Visible;                
            }
        }


        private string GetBase64String(Bitmap bitMap)
        {

            MemoryStream stream = new MemoryStream();
            bitMap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            byte[] ba = stream.ToArray();
            string base64 = Base64.EncodeToString(ba, Base64Flags.Default);
            return base64;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (finalTermsView.Visibility == ViewStates.Gone)
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, termsFragment).Commit();
            }
            else
            {
                finalTermsView.Visibility = ViewStates.Gone;
                signatureLayout.Visibility = ViewStates.Visible;
            }
        }

        private async void NextButton_Click(object sender, EventArgs e)
        {
            finalContractBitMap = TermsAndConditionsFragment.GetBitmapFromView(finalTermsView);
            PaymentViewModel.Base64ContractString = GetBase64String(finalContractBitMap);
            var result = await ViewModel.AgreeMembership();
            if (result)
            {
                //signatuerPad.Clear();
                //SignatureClass.signaturePoints = null;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, paymentScreenFragment).Commit();
            }
            
        }
    }

    public static class SignatureClass
    {
        public static PointF[] signaturePoints { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    public class TermsAndConditionsFragment : MvxFragment<TermsAndConditionsViewModel>
    {
        private TextView AgreeTextView;
        private TextView DisagreeTextView;
        private ImageView termsandconditionsImage;
        private Button backButton;
        private VehicleMembershipDetailsViewModel vehicleMembershipVM;
        VehicleAdditionalServicesFragment additionalServicesFragment;
        MyProfileInfoFragment infoFragment;
        MembershipSignatureFragment signatureFragment;
        PaymentScreenFragment paymentScreenFragment;
        public double Total = 0;
        public List<string> SelectedAdditionalServices = new List<string>();
        private TextView MembershipName;
        private TextView AdditionalServicesCost;
        private TextView SwitchMembershipFee;
        private TextView UpchargesCost;
        private TextView OptionCode;
        private TextView AdvanceFee;
        private TextView Date;
        private TextView TotalCost;
        private TextView AdditionalServices;
        private TextView YearlyTotal;
        private TextView MonthlyCharges;
        private TextView StartingDate;
        private TextView EndingDate;
        private TextView MonthlyRecurString;
        private LinearLayout parentView;
        public static Bitmap contractImage;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.TermsAndConditionsFragment, null);
            infoFragment = new MyProfileInfoFragment();           
            additionalServicesFragment = new VehicleAdditionalServicesFragment();
            signatureFragment = new MembershipSignatureFragment();
            vehicleMembershipVM = new VehicleMembershipDetailsViewModel();
            termsandconditionsImage = rootview.FindViewById<ImageView>(Resource.Id.termsandconditionsDetail);
            AgreeTextView = rootview.FindViewById<TextView>(Resource.Id.textAgree);
            DisagreeTextView = rootview.FindViewById<TextView>(Resource.Id.textDisagree);
            backButton = rootview.FindViewById<Button>(Resource.Id.signatureBack);
            AgreeTextView.PaintFlags = PaintFlags.UnderlineText;
            DisagreeTextView.PaintFlags = PaintFlags.UnderlineText;
            this.ViewModel = new TermsAndConditionsViewModel();
            MembershipName = rootview.FindViewById<TextView>(Resource.Id.membershipName);
            AdditionalServicesCost = rootview.FindViewById<TextView>(Resource.Id.additionalServicesCost);
            SwitchMembershipFee = rootview.FindViewById<TextView>(Resource.Id.switchMembershipFee);
            SwitchMembershipFee.Visibility = ViewStates.Gone;
            UpchargesCost = rootview.FindViewById<TextView>(Resource.Id.upcharges);
            UpchargesCost.Visibility = ViewStates.Gone;
            OptionCode = rootview.FindViewById<TextView>(Resource.Id.optionCode);
            AdvanceFee = rootview.FindViewById<TextView>(Resource.Id.advanceFee);
            Date = rootview.FindViewById<TextView>(Resource.Id.date);
            TotalCost = rootview.FindViewById<TextView>(Resource.Id.total);
            AdditionalServices = rootview.FindViewById<TextView>(Resource.Id.additionalSevices);
            YearlyTotal = rootview.FindViewById<TextView>(Resource.Id.yearlyTotal);
            MonthlyCharges = rootview.FindViewById<TextView>(Resource.Id.monthlyCharges);
            StartingDate = rootview.FindViewById<TextView>(Resource.Id.startingDate);
            EndingDate = rootview.FindViewById<TextView>(Resource.Id.endingDate);
            MonthlyRecurString = rootview.FindViewById<TextView>(Resource.Id.monthlyRecurString);
            parentView = rootview.FindViewById<LinearLayout>(Resource.Id.parentView);
            TermsDocument();

            string Datenow = DateTime.Now.Date.ToString("yyyy-MM-dd");
            StartingDate.Text = new DateTime(DateTime.Now.Date.AddMonths(1).Year, DateTime.Now.Date.AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
            EndingDate.Text = "Open";
            Date.Text = Datenow.Substring(0, 10);
            if (MembershipDetails.selectedMembershipDetail.MembershipName.Length < 8)
            {

                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                MembershipName.Text = membershipname;
            }

            else
            {
                string membershipname = MembershipDetails.selectedMembershipDetail.MembershipName.Substring(8) + "- $" + (VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price);
                MembershipName.Text = membershipname;
            }

            GetTotal();
            AgreeTextView.Click += AgreeTextView_Click;
            DisagreeTextView.Click += DisagreeTextView_Click;
            backButton.Click += BackButton_Click;
            
            return rootview;
        }
        void TermsDocument() 
        {
            SpannableString s1 = new SpannableString("Split into monthly recurring charges of");           
            s1.SetSpan(new BackgroundColorSpan(Color.Yellow), 11, 28, SpanTypes.ExclusiveExclusive);
            MonthlyRecurString.TextFormatted = s1;           
            
        }

        public void GetTotal()
        {
            double MembershipAmount = VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != null)
                {

                    //Console.WriteLine(Service.Upcharges.Split("-")[1].Substring(3));
                    Total += (double)Service.Price;
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$" + string.Format("{0:0.00}", Service.Price));

                }
                else
                {
                    SelectedAdditionalServices.Add(Service.ServiceName.Trim() + "-$0");
                }
            }
            if (CustomerInfo.MembershipFee != 0)
            {
                MembershipAmount += CustomerInfo.MembershipFee;
                SwitchMembershipFee.Visibility = ViewStates.Visible;
            }
            if (MembershipDetails.modelUpcharge.upcharge.Count != 0)
            {
                UpchargesCost.Visibility = ViewStates.Visible;
                MembershipAmount += MembershipDetails.modelUpcharge.upcharge[0].Price;
                UpchargesCost.Text = "Wash Upcharges:   " + "$" + string.Format("{0:0.00}", MembershipDetails.modelUpcharge.upcharge[0].Price);
            }
            string ValuesToDisplay = ""+string.Join(", ", SelectedAdditionalServices);
            AdditionalServices.Text = ValuesToDisplay;
            AdditionalServicesCost.Text = "$" + string.Format("{0:0.00}", Total);
            Total += MembershipAmount;
            MonthlyCharges.Text = "$" + string.Format("{0:0.00}", Total);
            TotalCost.Text = "$" + string.Format("{0:0.00}", Total);
            YearlyTotal.Text = "$" + string.Format("{0:0.00}", Total * 12);
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, additionalServicesFragment).Commit();
        }

        private async void DisagreeTextView_Click(object sender, EventArgs e)
        {
            var result = await ViewModel.DisagreeMembership();
            if (result)
            {
                SignatureClass.signaturePoints = null;
                MyProfileInfoNeeds.selectedTab = 1;
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoFragment).Commit();
            }
        }

        private void AgreeTextView_Click(object sender, EventArgs e)
        {
            contractImage = GetBitmapFromView(parentView);                       
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, signatureFragment).Commit();         

        }
        public static Bitmap GetBitmapFromView(View view)
        {
            //Define a bitmap with the same size as the view
            Bitmap returnedBitmap = Bitmap.CreateBitmap(view.Width, view.Height, Bitmap.Config.Argb8888);
            //Bind a canvas to it
            Canvas canvas = new Canvas(returnedBitmap);
            //Get the view's background
            Drawable bgDrawable = view.Background;
            if (bgDrawable != null)
            {
                //has background drawable, then draw it on the canvas
                bgDrawable.Draw(canvas);
            }
            else
            {
                //does not have background drawable, then draw white background on the canvas
                canvas.DrawColor(Color.White);
            }
            // draw the view on the canvas
            view.Draw(canvas);
            //return the bitmap
            return returnedBitmap;
        }
    }
}
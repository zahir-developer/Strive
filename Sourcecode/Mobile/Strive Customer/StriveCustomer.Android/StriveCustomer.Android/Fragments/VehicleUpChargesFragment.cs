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
using MvvmCross.IoC;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleUpChargesFragment : MvxFragment<VehicleUpchargeViewModel>
    {
        VehicleAdditionalServicesFragment additionalServicesFragment;
        VehicleMembershipFragment membershipFragment;
        private RadioGroup upchargeOptions;
        private Button backButton;
        private Button nextButton;
        private Dictionary<int, int> upchargeRadio;
        LinearLayout.LayoutParams layoutParams;
        int someId = 12348880;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = inflater.Inflate(Resource.Layout.VehicleUpChargesFragment,null);
            this.ViewModel = new VehicleUpchargeViewModel();
            additionalServicesFragment = new VehicleAdditionalServicesFragment();
            membershipFragment = new VehicleMembershipFragment();
            upchargeRadio = new Dictionary<int, int>();
            upchargeOptions = rootview.FindViewById<RadioGroup>(Resource.Id.upchargesOptions);
            nextButton = rootview.FindViewById<Button>(Resource.Id.upchargeNext);
            backButton = rootview.FindViewById<Button>(Resource.Id.upchargeBack);
            nextButton.Click += NextButton_Click;
            backButton.Click += BackButton_Click;
            upchargeOptions.CheckedChange += UpchargeOptions_CheckedChange;
            ServiceDetails();
            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipFragment).Commit();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if(ViewModel.VehicleUpchargeCheck())
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, additionalServicesFragment).Commit();
            }
        }

        private void UpchargeOptions_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            MembershipDetails.selectedUpCharge = upchargeRadio.FirstOrDefault(x => x.Value == e.CheckedId).Key;
        }

        private async void ServiceDetails()
        {
            await this.ViewModel.getServiceList(MembershipDetails.selectedMembership);
            await this.ViewModel.getAllServiceList();
            if(MembershipDetails.filteredList != null)
            {
                foreach(var result in MembershipDetails.filteredList.ServicesWithPrice)
                {
                    if(!string.IsNullOrEmpty(result.Upcharges))
                    {
                        RadioButton upChargeRadio = new RadioButton(Context);
                        layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                        layoutParams.Gravity = GravityFlags.Left | GravityFlags.Center;
                        layoutParams.SetMargins(0, 20, 0, 20);
                        
                        if(!upchargeRadio.ContainsKey(result.ServiceId))
                        {
                            upChargeRadio.LayoutParameters = layoutParams;
                            upChargeRadio.Text = result.Upcharges;
                            upChargeRadio.SetButtonDrawable(Resource.Drawable.radioButton);
                            upChargeRadio.Id = someId;
                            upchargeRadio.Add(result.ServiceId, someId);
                            upChargeRadio.SetTypeface(null, TypefaceStyle.Bold);
                            upChargeRadio.SetTextSize(ComplexUnitType.Sp, (float)16.5);
                            upChargeRadio.TextAlignment = TextAlignment.ViewEnd;
                            if (result.ServiceId == MembershipDetails.selectedUpCharge)
                            {
                                upChargeRadio.Checked = true;
                            }
                            someId++;
                            upchargeOptions.AddView(upChargeRadio);
                        }
                       
                    }
                   
                }
            }

        }
    }
}
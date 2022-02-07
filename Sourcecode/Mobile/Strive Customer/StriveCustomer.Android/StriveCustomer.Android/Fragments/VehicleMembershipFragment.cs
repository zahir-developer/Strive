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
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using AlertDialog = Android.App.AlertDialog;
using static Android.Views.View;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class VehicleMembershipFragment : MvxFragment<VehicleMembershipViewModel>
    {
        private RadioGroup membershipGroup;
        private Dictionary<int, string> serviceList;
        private Dictionary<int, int> checkedId;
        private Button backButton;
        private Button nextButton;
        int someId = 12347770;
        VehicleUpChargesFragment upchargeFragment;
        VehicleInfoDisplayFragment infoDisplayFragment;
        LinearLayout.LayoutParams layoutParams;
        Context context;
        public VehicleMembershipFragment(Context cxt)
        {
            this.context = cxt;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleMembershipInfoFragment, null);
            upchargeFragment = new VehicleUpChargesFragment(this.Activity);
            infoDisplayFragment = new VehicleInfoDisplayFragment();
            this.ViewModel = new VehicleMembershipViewModel();
            serviceList = new Dictionary<int, string>();
            checkedId = new Dictionary<int, int>();
            membershipGroup = rootview.FindViewById<RadioGroup>(Resource.Id.membershipOptions);
            backButton = rootview.FindViewById<Button>(Resource.Id.membershipBack);
            nextButton = rootview.FindViewById<Button>(Resource.Id.membershipNext);            
            backButton.Enabled = false;
            nextButton.Enabled = false;
            getMembershipData();

            membershipGroup.CheckedChange += MembershipGroup_CheckedChange;
            backButton.Click += BackButton_Click;
            nextButton.Click += NextButton_Click;
            return rootview;
        }        

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (ViewModel.VehicleMembershipCheck())
            {
                AppCompatActivity activity = (AppCompatActivity)context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, upchargeFragment).Commit();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {

            AppCompatActivity activity = (AppCompatActivity)context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoDisplayFragment).Commit();
        }

        private void MembershipGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            MembershipDetails.selectedMembership = checkedId.FirstOrDefault(x => x.Value == e.CheckedId).Key;
            int index = membershipGroup.IndexOfChild(membershipGroup.FindViewById(membershipGroup.CheckedRadioButtonId));
            if (index != -1)
                UpdatePrice(index);
        }
        public void UpdatePrice(int index)
        {
            if (MembershipDetails.selectedMembershipDetail != null)
            {
                if (MembershipDetails.selectedMembershipDetail.Price > this.ViewModel.membershipList.Membership[index].Price)
                {
                    CustomerInfo.MembershipFee = 20;
                    MembershipDetails.selectedMembershipDetail = this.ViewModel.membershipList.Membership[index];

                }
                else
                {
                    CustomerInfo.MembershipFee = 0;
                    MembershipDetails.selectedMembershipDetail = this.ViewModel.membershipList.Membership[index];
                }
            }
            else
            {
                MembershipDetails.selectedMembershipDetail = this.ViewModel.membershipList.Membership[index];
            }

            MembershipDetails.selectedAdditionalServices = null;
        }
        public async void getMembershipData()
        {
            try 
            {
                await this.ViewModel.getMembershipDetails();
                foreach (var data in this.ViewModel.membershipList.Membership)
                {
                    RadioButton radioButton = new RadioButton(context);
                    layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                    layoutParams.Gravity = GravityFlags.Left | GravityFlags.Center;
                    layoutParams.SetMargins(0, 25, 0, 25);
                    radioButton.LayoutParameters = layoutParams;
                    radioButton.Text = data.MembershipName;
                    radioButton.SetButtonDrawable(Resource.Drawable.radioButton);
                    radioButton.Id = someId;
                    checkedId.Add(data.MembershipId, someId);
                    radioButton.SetTextSize(ComplexUnitType.Sp, (float)16.5);
                    radioButton.SetTypeface(null, TypefaceStyle.Bold);
                    radioButton.TextAlignment = TextAlignment.ViewEnd;

                    if (data.MembershipId == MembershipDetails.selectedMembership)
                    {
                        radioButton.Checked = true;

                        MembershipDetails.selectedMembershipDetail = this.ViewModel.membershipList.Membership[ViewModel.membershipList.Membership.IndexOf(data)];
                    }


                    someId++;
                    membershipGroup.AddView(radioButton);
                }
                backButton.Enabled = true;
                nextButton.Enabled = true;
            }
            catch (Exception ex) 
            {
                if (ex is OperationCanceledException) 
                {
                    return;
                }
            
            }            
        }
    }
}
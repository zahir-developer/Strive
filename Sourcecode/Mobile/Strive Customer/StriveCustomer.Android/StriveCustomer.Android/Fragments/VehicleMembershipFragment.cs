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
using static Android.Views.View;

namespace StriveCustomer.Android.Fragments
{
    public class VehicleMembershipFragment : MvxFragment<VehicleMembershipViewModel>
    {
        private RadioGroup membershipGroup;
        private Dictionary<int, string> serviceList;
        private Dictionary<int,int> checkedId;
        private Button backButton;
        private Button nextButton;
        int someId = 12347770;
        VehicleUpChargesFragment upchargeFragment;
        VehicleInfoEditFragment infoEditFragment;
        LinearLayout.LayoutParams layoutParams;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleMembershipInfoFragment, null);
            upchargeFragment = new VehicleUpChargesFragment();
            infoEditFragment = new VehicleInfoEditFragment();
            this.ViewModel = new VehicleMembershipViewModel();
            serviceList = new Dictionary<int, string>();
            checkedId = new Dictionary<int, int>();
            getMembershipData();
            membershipGroup = rootview.FindViewById<RadioGroup>(Resource.Id.membershipOptions);
            backButton = rootview.FindViewById<Button>(Resource.Id.membershipBack);
            nextButton = rootview.FindViewById<Button>(Resource.Id.membershipNext);
            membershipGroup.CheckedChange += MembershipGroup_CheckedChange;
            backButton.Click += BackButton_Click;
            nextButton.Click += NextButton_Click;
            return rootview;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, upchargeFragment).Commit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, infoEditFragment).Commit();
        }

        private async void MembershipGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            CustomerInfo.selectedMemberShip = checkedId.FirstOrDefault(x => x.Value == e.CheckedId).Key;
        }

        public async void getMembershipData()
        {
            await this.ViewModel.getMembershipDetails();
            foreach (var data in this.ViewModel.membershipList.Membership)
            {
                RadioButton radioButton = new RadioButton(Context);
                layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                layoutParams.Gravity = GravityFlags.Left | GravityFlags.Center;
                layoutParams.SetMargins(0, 20, 0, 20);
                radioButton.LayoutParameters = layoutParams;
                radioButton.Text = data.MembershipName; 
                //radioButton.SetPadding(0,20,0,20);
                radioButton.SetButtonDrawable(Resource.Drawable.radioButton);
                radioButton.Id = someId;
                checkedId.Add(data.MembershipId,someId);
                radioButton.SetTextSize(ComplexUnitType.Sp,14);
                radioButton.SetTypeface(null,TypefaceStyle.Bold);
                radioButton.TextAlignment = TextAlignment.ViewEnd;
                someId++;
                membershipGroup.AddView(radioButton);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using StriveEmployee.Android.Adapter;
using StriveEmployee.Android.Fragments.MyProfile.Collisions;
using StriveEmployee.Android.Fragments.MyProfile.Documents;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    [MvxFragmentPresentationAttribute]
    public class MyProfileFragment : MvxFragment
    {
        private TabLayout profile_TabLayout;
        private ViewPager profile_ViewPager;
        private ViewPagerAdapter profile_ViewPagerAdapter;
        private EmployeeInfoFragment employeeInfo_Fragment;
        private CollisionsFragment collisions_Fragment;
        private DocumentsFragment document_Fragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MyProfile_Fragment, null);

            profile_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.myProfile_TabLayout);
            profile_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.myProfile_ViewPager);

            employeeInfo_Fragment = new EmployeeInfoFragment();
            collisions_Fragment = new CollisionsFragment();
            document_Fragment = new DocumentsFragment();
            return rootView;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            profile_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            profile_ViewPagerAdapter.AddFragment(employeeInfo_Fragment, "Employee Info");
            profile_ViewPagerAdapter.AddFragment(collisions_Fragment, "Collision");
            profile_ViewPagerAdapter.AddFragment(document_Fragment, "Documents");

            profile_ViewPager.Adapter = profile_ViewPagerAdapter;
            profile_TabLayout.SetupWithViewPager(profile_ViewPager);
        }
    }
}
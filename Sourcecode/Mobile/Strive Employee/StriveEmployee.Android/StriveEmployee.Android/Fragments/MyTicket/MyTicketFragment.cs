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
using Strive.Core.ViewModels.Employee.MyTicket;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments.MyTicket
{
    public class MyTicketFragment : MvxFragment<MyTicketViewModel>
    {
        private TabLayout myTickets_TabLayout;
        private ViewPager myTickets_ViewPager;
        private ViewPagerAdapter myTickets_ViewPagerAdapter;
        private AllTicketsFragment allTickets_Fragment;
        private PendingTicketsFragment pendingTickets_Fragment;
        private CompletedTicketsFragment completedTickets_Fragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MyTickets_Fragment, null);
            this.ViewModel = new MyTicketViewModel();

            myTickets_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.myTickets_TabLayout);
            myTickets_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.myTickets_ViewPager);

            allTickets_Fragment = new AllTicketsFragment();
            pendingTickets_Fragment = new PendingTicketsFragment();
            completedTickets_Fragment = new CompletedTicketsFragment();

            return rootView;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            myTickets_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            myTickets_ViewPagerAdapter.AddFragment(allTickets_Fragment,"All Tickets");
            myTickets_ViewPagerAdapter.AddFragment(pendingTickets_Fragment, "Pending");
            myTickets_ViewPagerAdapter.AddFragment(completedTickets_Fragment, "Completed");
            myTickets_ViewPager.Adapter = myTickets_ViewPagerAdapter;
            myTickets_TabLayout.SetupWithViewPager(myTickets_ViewPager);

        }
    }
}
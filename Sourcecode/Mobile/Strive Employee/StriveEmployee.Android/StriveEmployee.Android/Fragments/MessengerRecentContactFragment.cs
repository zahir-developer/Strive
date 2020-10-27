using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerRecentContactFragment : MvxFragment<MessengerRecentContactsViewModel>
    {
        private RecyclerView recentContacts_RecyclerView;
        private MessengerRecentContactsAdapter messengerRecentContacts_Adapter;
        private List<RecentContactsSampleData> recentContactsSampleDatas;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerRecentContacts_Fragment, null);
            recentContacts_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recentContacts_RecyclerView);
            getCardata();
            return rootView;
        }

        public void getCardata()
        {
            recentContactsSampleDatas = new List<RecentContactsSampleData>();
            RecentContactsSampleData recent1 = new RecentContactsSampleData
            {
                ContactName = "Markus Brown",
                LastMessage = "Have you checked the car washes available ?",
                MessageTime = "8:10pm"
            };
            RecentContactsSampleData recent2 = new RecentContactsSampleData
            {
                ContactName = "Abi Black",
                LastMessage = "Last Car sent to final bay",
                MessageTime = "5:00am"
            };
            RecentContactsSampleData recent3 = new RecentContactsSampleData
            {
                ContactName = "Zian Johnson",
                LastMessage = "This ferrari needs a wheel job",
                MessageTime = "12:10pm"
            };
            recentContactsSampleDatas.Add(recent1);
            recentContactsSampleDatas.Add(recent2);
            recentContactsSampleDatas.Add(recent3);
            messengerRecentContacts_Adapter = new MessengerRecentContactsAdapter(this.Context, recentContactsSampleDatas);
            var layoutManager = new LinearLayoutManager(Context);
            recentContacts_RecyclerView.SetLayoutManager(layoutManager);
            recentContacts_RecyclerView.SetAdapter(messengerRecentContacts_Adapter);
        }
    }

    
    public class RecentContactsSampleData
    { 
    
     public string ContactName { get; set; }

     public string LastMessage { get; set; }

        public string MessageTime { get; set; }

    }

}
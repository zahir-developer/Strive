﻿using System;
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
using Strive.Core.Models.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerRecentContactFragment : MvxFragment<MessengerRecentContactsViewModel>
    {
        private RecyclerView recentContacts_RecyclerView;
        private MessengerRecentContactsAdapter messengerRecentContacts_Adapter;
        private SearchView recentContactSearchView;
        private MessengerSearchAdapter searchAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerRecentContacts_Fragment, null);
            this.ViewModel = new MessengerRecentContactsViewModel();
            recentContacts_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recentContacts_RecyclerView);
            recentContactSearchView = rootView.FindViewById<SearchView>(Resource.Id.recentContactSearchView);
            recentContactSearchView.QueryTextChange += RecentContactSearchView_QueryTextChange;
            searchAdapter = new MessengerSearchAdapter();

            getRecentContacts();
            return rootView;
        }

        private void RecentContactSearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.NewText))
            {
                var sortedResult = searchAdapter.SearchRecentContacts(ViewModel.EmployeeList.ChatEmployeeList, e.NewText);

                if (sortedResult.Count >= 0 || string.IsNullOrEmpty(e.NewText))
                {
                    messengerRecentContacts_Adapter = new MessengerRecentContactsAdapter(this.Context, sortedResult);
                    var layoutManager = new LinearLayoutManager(Context);
                    recentContacts_RecyclerView.SetLayoutManager(layoutManager);
                    recentContacts_RecyclerView.SetAdapter(messengerRecentContacts_Adapter);
                }
            }
            else
            {
                messengerRecentContacts_Adapter = new MessengerRecentContactsAdapter(this.Context, ViewModel.EmployeeList.ChatEmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                recentContacts_RecyclerView.SetLayoutManager(layoutManager);
                recentContacts_RecyclerView.SetAdapter(messengerRecentContacts_Adapter);
            }
        }

        private async void getRecentContacts()
        {
            await ViewModel.GetRecentContactsList();
            if(ViewModel.EmployeeList != null)
            {
                messengerRecentContacts_Adapter = new MessengerRecentContactsAdapter(this.Context, ViewModel.EmployeeList.ChatEmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                recentContacts_RecyclerView.SetLayoutManager(layoutManager);
                recentContacts_RecyclerView.SetAdapter(messengerRecentContacts_Adapter);
            }
        }
    }
}
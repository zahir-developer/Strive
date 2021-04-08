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
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerContactFragment : MvxFragment<MessengerContactViewModel>
    {
        private RecyclerView contacts_RecyclerView;
        private SearchView contact_SearchView;
        private MessengerContactsAdapter messengerContacts_Adapter;
        private MessengerSearchAdapter searchAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerContacts_Fragment, null);
            this.ViewModel = new MessengerContactViewModel();
            

            contacts_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.contacts_RecyclerView);
            contact_SearchView = rootView.FindViewById<SearchView>(Resource.Id.contacts_SearchView);
            contact_SearchView.QueryTextChange += Contact_SearchView_QueryTextChange; 
            searchAdapter = new MessengerSearchAdapter();
            getContacts();
            return rootView;
        }

        private async void Contact_SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //if (!string.IsNullOrEmpty(e.NewText) && ViewModel.EmployeeLists != null)
            //{
            //   // getContacts(e.NewText);
            //    var sortedResult = searchAdapter.SearchContacts(ViewModel.EmployeeLists.Employee, e.NewText);
            //    if (sortedResult.Count >= 0 || string.IsNullOrEmpty(e.NewText))
            //    {
            //        messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, sortedResult);
            //        var layoutManager = new LinearLayoutManager(Context);
            //        contacts_RecyclerView.SetLayoutManager(layoutManager);
            //        contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
            //    }
            //}
            //else
            //{
            //   // getContacts("%20");
            //    messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, ViewModel.EmployeeLists.EmployeeList);
            //    var layoutManager = new LinearLayoutManager(Context);
            //    contacts_RecyclerView.SetLayoutManager(layoutManager);
            //    contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
            //}
        }

        private async void getContacts()
        {
            if(MessengerTempData.EmployeeLists == null || MessengerTempData.ContactsCount < MessengerTempData.EmployeeLists.EmployeeList.Count)
            {
                var employeeLists = await ViewModel.GetContactsList();
                //if(MessengerTempData.employeeList_Contact != null || employeeLists != null ||employeeLists.Employee != null || employeeLists.Employee.Count != 0)
                {
                    messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, employeeLists.EmployeeList.Employee);
                    var layoutManager = new LinearLayoutManager(Context);
                    contacts_RecyclerView.SetLayoutManager(layoutManager);
                    contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
                }
            }
              
        }
    }
}
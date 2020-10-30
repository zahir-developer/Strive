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
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerContactFragment : MvxFragment<MessengerContactViewModel>
    {
        private RecyclerView contacts_RecyclerView;
        private MessengerContactsAdapter messengerContacts_Adapter;
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
            getContacts();
            return rootView;
        }

        private async void getContacts()
        {
            await ViewModel.GetContactsList();
            if(ViewModel.EmployeeLists != null || !(ViewModel.EmployeeLists.EmployeeList.Count == 0))
            {
                messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, ViewModel.EmployeeLists.EmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                contacts_RecyclerView.SetLayoutManager(layoutManager);
                contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
            }
            
        }
    }
}
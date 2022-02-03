using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using OperationCanceledException = System.OperationCanceledException;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveOwner.Android.Fragments
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

        private  void Contact_SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewText) && ViewModel.EmployeeLists != null)
            {
                // getContacts(e.NewText);
                var sortedResult = searchAdapter.SearchContacts(ViewModel.EmployeeLists.EmployeeList.Employee, e.NewText);
                if (sortedResult.Count >= 0 || string.IsNullOrEmpty(e.NewText))
                {
                    messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, sortedResult , this.ViewModel);
                    var layoutManager = new LinearLayoutManager(Context);
                    contacts_RecyclerView.SetLayoutManager(layoutManager);
                    contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
                }
            }
            else
            {
                if (ViewModel.EmployeeLists != null)
                {
                    messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, ViewModel.EmployeeLists.EmployeeList.Employee, this.ViewModel);
                    var layoutManager = new LinearLayoutManager(Context);
                    contacts_RecyclerView.SetLayoutManager(layoutManager);
                    contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
                }
            }
        }

        private async void getContacts()
        {
            if(MessengerTempData.EmployeeLists == null || MessengerTempData.ContactsCount < MessengerTempData.EmployeeLists.EmployeeList.Count)
            {
                try
                {
                    var employeeLists = await ViewModel.GetContactsList();
                    if (MessengerTempData.employeeList_Contact != null && employeeLists != null && employeeLists.EmployeeList != null && employeeLists?.EmployeeList?.Employee != null)
                    {
                        messengerContacts_Adapter = new MessengerContactsAdapter(this.Context, employeeLists.EmployeeList?.Employee, this.ViewModel);
                        var layoutManager = new LinearLayoutManager(Context);
                        contacts_RecyclerView.SetLayoutManager(layoutManager);
                        contacts_RecyclerView.SetAdapter(messengerContacts_Adapter);
                    }
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
}
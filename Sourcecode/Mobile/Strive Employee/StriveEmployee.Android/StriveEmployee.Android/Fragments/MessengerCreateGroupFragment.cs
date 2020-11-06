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
    public class MessengerCreateGroupFragment : MvxFragment<MessengerCreateGroupViewModel>
    {
        private RecyclerView createGroup_RecyclerView;
        private MessengerCreateGroupAdapter messengerCreateGroup_Adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerCreateGroup_Fragment, null);
            this.ViewModel = new MessengerCreateGroupViewModel();

            createGroup_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.createGroup_RecyclerView);
            selectGroupChatEntry();

            return rootView;
        }

        private async void selectGroupChatEntry()
        {
            await ViewModel.GetContactsList();
            if(ViewModel.EmployeeLists != null || ViewModel.EmployeeLists.EmployeeList.Count != 0)
            {
                messengerCreateGroup_Adapter = new MessengerCreateGroupAdapter(Context, ViewModel.EmployeeLists.EmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                createGroup_RecyclerView.SetLayoutManager(layoutManager);
                createGroup_RecyclerView.SetAdapter(messengerCreateGroup_Adapter);
            }
        }
    }
}
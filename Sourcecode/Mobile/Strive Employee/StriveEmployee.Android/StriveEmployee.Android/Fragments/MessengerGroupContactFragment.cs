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
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerGroupContactFragment : MvxFragment<MessengerGroupContactViewModel>
    {
        private RecyclerView groupChat_RecyclerView;
        private MessengerGroupChatAdapter messengerGroup_Adapter; 
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerGroupContacts_Fragment, null);
            this.ViewModel = new MessengerGroupContactViewModel();
            getGroups();
            groupChat_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.groupChat_RecyclerView);
            return rootView;
        }

        private async void getGroups()
        {
            await ViewModel.GetGroupsList();
            if (ViewModel.GroupList != null || !(ViewModel.GroupList.ChatEmployeeList.Count == 0))
            {
                messengerGroup_Adapter = new MessengerGroupChatAdapter(this.Context, ViewModel.GroupList.ChatEmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                groupChat_RecyclerView.SetLayoutManager(layoutManager);
                groupChat_RecyclerView.SetAdapter(messengerGroup_Adapter);

            }
        }
    }
}
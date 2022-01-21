﻿using System;

using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    [MvxFragmentPresentationAttribute]
    [MvxUnconventionalAttribute]
    public class MessengerFragment : MvxFragment<MessengerPersonalChatViewModel>
    {
        private ImageButton messenger_ImageButton;
        private TabLayout messenger_TabLayout;
        private ViewPager messenger_ViewPager;
        private ViewPagerAdapter messenger_ViewPagerAdapter;
        private PopupMenu messenger_PopupMenu;
        private MvxFragment selected_MvxFragment;
        private IMenu messenger_Menu;
        private MessengerContactFragment contactFragment;
        private MessengerRecentContactFragment recentContactFragment;
        private MessengerGroupContactFragment groupContactFragment;
        public static string ConnectionID;
        private HubConnection connection;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Messenger_Fragment, null);
            this.ViewModel = new MessengerPersonalChatViewModel();
            contactFragment = new MessengerContactFragment();
            recentContactFragment = new MessengerRecentContactFragment();
            groupContactFragment = new MessengerGroupContactFragment();

            messenger_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.menu_ImageButton);
            messenger_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.messenger_TabLayout);
            messenger_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.messenger_ViewPager);
            messenger_PopupMenu = new PopupMenu(Context, messenger_ImageButton);
            messenger_Menu = messenger_PopupMenu.Menu;
            messenger_PopupMenu.MenuInflater.Inflate(Resource.Menu.group_create_menu, messenger_Menu);
            messenger_PopupMenu.MenuItemClick += Messenger_PopupMenu_MenuItemClick;
            messenger_ImageButton.Click += Messenger_ImageButton_Click;
    
            return rootView;
        }



        private void Messenger_PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_CreateGroup:
                    MessengerTempData.resetParticipantInfo();
                    selected_MvxFragment = new MessengerCreateGroupFragment();
                    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
                    break;

                //case Resource.Id.menu_Refresh:
                //    break;
            }
           
        }

        private void Messenger_ImageButton_Click(object sender, EventArgs e)
        {
          messenger_PopupMenu.Show();
           
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            messenger_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            messenger_ViewPagerAdapter.AddFragment(recentContactFragment, "Recent");
            messenger_ViewPagerAdapter.AddFragment(contactFragment, "Contact");
            messenger_ViewPagerAdapter.AddFragment(groupContactFragment, "Groups");
            messenger_ViewPager.Adapter = messenger_ViewPagerAdapter;
            messenger_TabLayout.SetupWithViewPager(messenger_ViewPager);
        }
        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnDetach()
        {
            base.OnDetach();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveOwner.Android.Adapter;

namespace StriveOwner.Android.Fragments
{
    public class MessengerFinalizeGroupFragment : MvxFragment<MessengerFinalizeGroupViewModel>
    {
        private Button Create_Button;
        private Button BackButton;
        private EditText groupFinalName_TextView;
        private MessengerFinalizeGroupAdapter messengerFinalizeGroup_Adapter;
        private MessengerCreateGroupFragment messengerCreateGroupFragment;
        private RecyclerView finalizeGroup_recyclerView;
        private ImageButton AddParticipant;
        MessengerFragment messengerFragment = new MessengerFragment(); 
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerFinalizeGroup_Fragment, null);
            this.ViewModel = new MessengerFinalizeGroupViewModel();

            Create_Button = rootView.FindViewById<Button>(Resource.Id.createGroupSave);
            groupFinalName_TextView = rootView.FindViewById<EditText>(Resource.Id.groupFinalName_TextView);
            finalizeGroup_recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.finalizeGroup_recyclerView);
            BackButton = rootView.FindViewById<Button>(Resource.Id.finalizeGroupBack);
            BackButton.Click += BackButton_Click;
            AddParticipant = rootView.FindViewById<ImageButton>(Resource.Id.addParticipants);
            AddParticipant.Click += AddParticipant_Click;
            getParticipants();
            Create_Button.Click += Create_Button_Click;
            return rootView;
        }

        private void AddParticipant_Click(object sender, EventArgs e)
        {
            messengerCreateGroupFragment = new MessengerCreateGroupFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerCreateGroupFragment).Commit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            messengerCreateGroupFragment = new MessengerCreateGroupFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerCreateGroupFragment).Commit();
        }

        private async void Create_Button_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(groupFinalName_TextView.Text))
            {
                this.ViewModel.GroupName = this.groupFinalName_TextView.Text;
                await this.ViewModel.CreateGroup();
                MessengerTempData.resetParticipantInfo();
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
            }
            else
            {
                this.ViewModel.EmptyGroupName();
            }
            
        }


        private void getParticipants()
        {
            messengerFinalizeGroup_Adapter = new MessengerFinalizeGroupAdapter(Context, MessengerTempData.createGroup_Contact);
            var layoutManager = new LinearLayoutManager(Context);
            finalizeGroup_recyclerView.SetLayoutManager(layoutManager);
            finalizeGroup_recyclerView.SetAdapter(messengerFinalizeGroup_Adapter);
        }
    }
}
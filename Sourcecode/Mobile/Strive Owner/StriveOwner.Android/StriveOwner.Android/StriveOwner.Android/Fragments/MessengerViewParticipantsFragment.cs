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
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveOwner.Android.Adapter;

namespace StriveOwner.Android.Fragments
{
    public class MessengerViewParticipantsFragment : MvxFragment<MessengerViewParticipantsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private ImageButton addParticipant_ImageButton;
        public TextView participantGroupName_TextView;
        private MvxFragment selected_MvxFragment;
        private MessengerViewParticipantsAdapter messengerViewParticipants_Adapter;
        private RecyclerView particpants_recyclerView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerViewParticipants_Fragment, null);
            this.ViewModel = new MessengerViewParticipantsViewModel();

            back_Button = rootView.FindViewById<Button>(Resource.Id.ViewParticipantsBack_Button);
            save_Button = rootView.FindViewById<Button>(Resource.Id.ViewParticipantsSave_Button);
            addParticipant_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.addParticipant_ImageButton);
            particpants_recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.particpants_recyclerView);
            participantGroupName_TextView = rootView.FindViewById<TextView>(Resource.Id.participantGroup_Name);
            participantGroupName_TextView.Text = MessengerTempData.GroupName;
            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;
            addParticipant_ImageButton.Click += AddParticipant_ImageButton_Click;
            GetParticipants();

            return rootView;
        }

        private async void Save_Button_Click(object sender, EventArgs e)
        {
            if(MessengerTempData.SelectedParticipants != null)
            {
                await this.ViewModel.UpdateGroup();
                MessengerTempData.resetChatData();
                MessengerTempData.resetParticipantInfo();
                selected_MvxFragment = new MessengerFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
            }
            else
            {
                this.ViewModel.NoParticipants();
            }
          
        }

        private void AddParticipant_ImageButton_Click(object sender, EventArgs e)
        {
            selected_MvxFragment = new MessengerCreateGroupFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }

        private async void GetParticipants()
        {
            MessengerTempData.IsCreateGroup = false;
            await ViewModel.GetParticipants();
            if(ViewModel.EmployeeList != null)
            {
                if(MessengerTempData.SelectedParticipants != null)
                {
                    ChatEmployeeList data;
                    foreach (var result in MessengerTempData.SelectedParticipants.EmployeeList.Employee)
                    {
                        var results = ViewModel.EmployeeList.EmployeeList.ChatEmployeeList.Find(x => x.Id == result.EmployeeId);
                        if (results == null)
                        {
                            data = new ChatEmployeeList();
                            data.FirstName = result.FirstName;
                            data.LastName = result.LastName;
                            ViewModel.EmployeeList.EmployeeList.ChatEmployeeList.Add(data);
                        }
                    }
                }
                messengerViewParticipants_Adapter = new MessengerViewParticipantsAdapter(Context, ViewModel.EmployeeList);
                var layoutManager = new LinearLayoutManager(Context);
                particpants_recyclerView.SetLayoutManager(layoutManager);
                particpants_recyclerView.SetAdapter(messengerViewParticipants_Adapter);
            }
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            selected_MvxFragment = new MessengerPersonalChatFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }
    }
}
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
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    public class MessengerViewParticipantsFragment : MvxFragment<MessengerViewParticipantsViewModel>
    {
        private Button back_Button;
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
            addParticipant_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.addParticipant_ImageButton);
            particpants_recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.particpants_recyclerView);
            participantGroupName_TextView = rootView.FindViewById<TextView>(Resource.Id.participantGroup_Name);
            participantGroupName_TextView.Text = MessengerTempData.GroupName;
            back_Button.Click += Back_Button_Click;
            addParticipant_ImageButton.Click += AddParticipant_ImageButton_Click;
            GetParticipants();

            return rootView;
        }

        private void AddParticipant_ImageButton_Click(object sender, EventArgs e)
        {
            selected_MvxFragment = new MessengerCreateGroupFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }

        private async void GetParticipants()
        {
            await ViewModel.GetParticipants();
            if(ViewModel.EmployeeList != null)
            {
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
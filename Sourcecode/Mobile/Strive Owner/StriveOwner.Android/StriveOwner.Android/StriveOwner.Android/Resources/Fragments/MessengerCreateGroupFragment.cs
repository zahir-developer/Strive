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
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveOwner.Android.Adapter;

namespace StriveOwner.Android.Fragments
{
    public class MessengerCreateGroupFragment : MvxFragment<MessengerCreateGroupViewModel>
    {
        private Button next_Button;
        private Button createGroupBack;
        private RecyclerView createGroup_RecyclerView;
        private MessengerFragment messengerFragment;
        private MessengerCreateGroupAdapter messengerCreateGroup_Adapter;
        private MessengerFinalizeGroupFragment FinalizeGroup_Fragment;
        private MvxFragment selected_MvxFragment;
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

            next_Button = rootView.FindViewById<Button>(Resource.Id.createGroupNext); 
            createGroupBack = rootView.FindViewById<Button>(Resource.Id.createGroupBack);
            createGroup_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.createGroup_RecyclerView);

            next_Button.Click += Next_Button_Click;
            createGroupBack.Click += CreateGroupBack_Click;

            selectGroupChatEntry();

            return rootView;
        }

        private void CreateGroupBack_Click(object sender, EventArgs e)
        {
            messengerFragment = new MessengerFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
        }

        private void Next_Button_Click(object sender, EventArgs e)
        {
            if(!MessengerTempData.IsCreateGroup)
            {
                selected_MvxFragment = new MessengerViewParticipantsFragment();
                FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
            }

            else
            {
                if(MessengerTempData.createGroup_Contact.EmployeeList.Employee.Count > 0)
                {
                    selected_MvxFragment = new MessengerFinalizeGroupFragment();
                    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
                }
                else
                {
                    this.ViewModel.NotEnough();
                }              
            }
           
        }

        private async void selectGroupChatEntry()
        {
            await ViewModel.GetContactsList();

            if (ViewModel.EmployeeLists != null || ViewModel.EmployeeLists.EmployeeList != null || ViewModel.EmployeeLists.EmployeeList.Employee != null || ViewModel.EmployeeLists.EmployeeList.Employee.Count != 0)
            {
                if (MessengerTempData.ExistingParticipants != null)
                {
                    MessengerTempData.IsCreateGroup = false;
                    if (MessengerTempData.SelectedParticipants == null)
                    {
                        MessengerTempData.SelectedParticipants = new Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts();
                        MessengerTempData.SelectedParticipants.EmployeeList = new Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                        MessengerTempData.SelectedParticipants.EmployeeList.Employee = new List<Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                    }

                    foreach (var data in MessengerTempData.ExistingParticipants.EmployeeList.Employee)
                    {
                        var participant = ViewModel.EmployeeLists.EmployeeList.Employee.Find(x => x.EmployeeId == data.EmployeeId);
                        ViewModel.EmployeeLists.EmployeeList.Employee.Remove(participant);
                    }
                    foreach (var data in MessengerTempData.SelectedParticipants.EmployeeList.Employee)
                    {
                        var participant = ViewModel.EmployeeLists.EmployeeList.Employee.Find(x => x.EmployeeId == data.EmployeeId);
                        ViewModel.EmployeeLists.EmployeeList.Employee.Remove(participant);
                    }

                    messengerCreateGroup_Adapter = new MessengerCreateGroupAdapter(Context, ViewModel.EmployeeLists.EmployeeList.Employee);
                }
                else
                {
                    MessengerTempData.IsCreateGroup = true;
                    messengerCreateGroup_Adapter = new MessengerCreateGroupAdapter(Context, ViewModel.EmployeeLists.EmployeeList.Employee);
                }

                //var layoutManager = new LinearLayoutManager(Context);
                //createGroup_RecyclerView.SetLayoutManager(layoutManager);
                //createGroup_RecyclerView.SetAdapter(messengerCreateGroup_Adapter);
            }
            //if (MessengerTempData.EmployeeLists.EmployeeList == null)
            //{
            //    await ViewModel.GetContactsList();
            //}
            if (MessengerTempData.createGroup_Contact == null)
            {
                MessengerTempData.createGroup_Contact = new EmployeeMessengerContacts();
                MessengerTempData.createGroup_Contact.EmployeeList = new EmployeeList();
                MessengerTempData.createGroup_Contact.EmployeeList.Employee = new List<Employee>();
            }
            if (MessengerTempData.ExistingParticipants != null)
            {
                MessengerTempData.IsCreateGroup = false;
                if (MessengerTempData.SelectedParticipants == null)
                {
                    MessengerTempData.SelectedParticipants = new Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts();
                    MessengerTempData.SelectedParticipants.EmployeeList = new Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                    MessengerTempData.SelectedParticipants.EmployeeList.Employee = new List<Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                }
                foreach (var data in MessengerTempData.ExistingParticipants.EmployeeList.Employee)
                {
                    var participant = MessengerTempData.EmployeeLists.EmployeeList.Find(x => x.EmployeeId == data.EmployeeId);
                    var removalData = new Employee();
                   
                    removalData.Collisions = participant.Collisions;
                    removalData.CommunicationId = participant.CommunicationId;
                    removalData.Documents = participant.Documents;
                    removalData.EmployeeCode = participant.EmployeeCode;
                    removalData.EmployeeId = participant.EmployeeId;
                    removalData.FirstName = participant.FirstName;
                    removalData.LastName = participant.LastName;
                    removalData.MobileNo = participant.MobileNo;
                    removalData.Schedules = participant.Schedules;
                    removalData.Status = participant.Status;

                    ViewModel.EmployeeLists.EmployeeList.Employee.Remove(removalData);
                }
                foreach (var data in MessengerTempData.SelectedParticipants.EmployeeList.Employee)
                {
                     var participant = ViewModel.EmployeeLists.EmployeeList.Employee.Find(x => x.EmployeeId == data.EmployeeId);
                    var removalData = new Employee();

                    removalData.Collisions = participant.Collisions;
                    removalData.CommunicationId = participant.CommunicationId;
                    removalData.Documents = participant.Documents;
                    removalData.EmployeeCode = participant.EmployeeCode;
                    removalData.EmployeeId = participant.EmployeeId;
                    removalData.FirstName = participant.FirstName;
                    removalData.LastName = participant.LastName;
                    removalData.MobileNo = participant.MobileNo;
                    removalData.Schedules = participant.Schedules;
                    removalData.Status = participant.Status;

                    ViewModel.EmployeeLists.EmployeeList.Employee.Remove(removalData);
                }
                messengerCreateGroup_Adapter = new MessengerCreateGroupAdapter(Context, ViewModel.EmployeeLists.EmployeeList.Employee);
            }

            //if(MessengerTempData.EmployeeLists.EmployeeList != null && MessengerTempData.ExistingParticipants == null)
            //{
                MessengerTempData.IsCreateGroup = true;
                messengerCreateGroup_Adapter = new MessengerCreateGroupAdapter(Context, MessengerTempData.employeeList_Contact.EmployeeList.Employee);
            //}
            var layoutManager = new LinearLayoutManager(Context);
            createGroup_RecyclerView.SetLayoutManager(layoutManager);
            createGroup_RecyclerView.SetAdapter(messengerCreateGroup_Adapter);

        }
    }
}

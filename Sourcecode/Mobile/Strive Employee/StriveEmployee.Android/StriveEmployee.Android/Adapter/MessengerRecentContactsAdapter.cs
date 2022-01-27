using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Fragments;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerRecentContactsRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button recentContact_Button;
        public TextView recentContactName_TextView;
        public TextView recentContactLastText_TextView;
        public TextView recentContactMessageTime_TextView;
        public IItemClickListener itemClickListener;
        
        public MessengerRecentContactsRecycleHolder(View recentContact) : base(recentContact)
        {

            recentContact_Button = recentContact.FindViewById<Button>(Resource.Id.recentContact_ImageView);
            recentContactName_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactName_TextView);
            recentContactLastText_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactLastText_TextView);
            recentContactMessageTime_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactMessageTime_TextView);
            recentContact.SetOnClickListener(this);

        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            itemClickListener.OnClick(view, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    }
    public class MessengerRecentContactsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerRecentContactsViewModel messengerRecentContactsViewModel;
        private MessengerRecentContactsRecycleHolder recentContactsRecycleHolder;
        private List<ChatEmployeeList> recentContacts = new List<ChatEmployeeList>();
        private char[] firstInitial;
        private char[] secondInitial;
        public IMessengerService MessengerService = Mvx.IoCProvider.Resolve<IMessengerService>();
        public MessengerRecentContactsAdapter(Context context, List<ChatEmployeeList> recentContacts , MessengerRecentContactsViewModel viewModel)
        {
            this.context = context;
            this.recentContacts = recentContacts;
            this.messengerRecentContactsViewModel = viewModel;

        }

        public override int ItemCount
        {
            get
            {
                return recentContacts.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            recentContactsRecycleHolder = holder as MessengerRecentContactsRecycleHolder;
            if (!string.IsNullOrEmpty(recentContacts[position].FirstName))
            {
                firstInitial = recentContacts[position].FirstName.ToCharArray();
            }
            else
            {
                firstInitial =  null;
            }
            if (!string.IsNullOrEmpty(recentContacts[position].LastName))
            {
               secondInitial = recentContacts[position].LastName.ToCharArray();
            }
            else
            {
                secondInitial = null;
            }          
            if(secondInitial == null)
            {
                if (firstInitial.Length != 0)
                {
                    recentContactsRecycleHolder.recentContact_Button.Text = firstInitial.ElementAt(0).ToString() + firstInitial.ElementAt(1).ToString();
                    recentContactsRecycleHolder.recentContactName_TextView.Text = recentContacts[position].FirstName + " " + recentContacts[position].LastName;
                }
            }
            else if(firstInitial == null)
            {
                if (secondInitial.Length != 0)
                {
                    recentContactsRecycleHolder.recentContact_Button.Text = secondInitial.ElementAt(0).ToString() + secondInitial.ElementAt(1).ToString();
                    recentContactsRecycleHolder.recentContactName_TextView.Text = recentContacts[position].FirstName + " " + recentContacts[position].LastName;
                }
            }          
            else
            {
                recentContactsRecycleHolder.recentContact_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                recentContactsRecycleHolder.recentContactName_TextView.Text = recentContacts[position].FirstName + " " + recentContacts[position].LastName;
            }
            if (!String.IsNullOrEmpty(recentContacts[position].RecentChatMessage))
            {
                DateTime UTCFormat = DateTime.Parse(recentContacts[position].CreatedDate);
                DateTime TimeKindFormat = DateTime.SpecifyKind(UTCFormat, DateTimeKind.Utc);
                DateTime localFormat = TimeKindFormat.ToLocalTime();
                var lastMessage = localFormat.ToString().Split(" ");
                if(String.Equals(DateTime.Now.Date.ToString(), localFormat.Date.ToString()))
                {
                    var messageTime = lastMessage[1].Split(":");

                    var TimeofDay = int.Parse(messageTime[0]);

                    if(TimeofDay >= 12)
                    {
                        recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = messageTime[0] + ":" + messageTime[1] + " " +"PM";
                    }
                    else
                    {
                        recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = messageTime[0] + ":" + messageTime[1] + " " + "AM";
                    }
                }
                else
                {
                    recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = lastMessage[0];
                }
                recentContactsRecycleHolder.recentContactLastText_TextView.Text = recentContacts[position].RecentChatMessage;
            }
            recentContactsRecycleHolder.SetItemClickListener(this);
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            int itemPosition = messengerRecentContactsViewModel.EmployeeList.ChatEmployeeList.IndexOf(recentContacts[position]);
            MessengerTempData.resetChatData();
            if (MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).IsGroup)
            {
                MessengerTempData.GroupID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).Id;
                MessengerTempData.IsGroup = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).IsGroup;
                MessengerTempData.GroupName = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).FirstName;
                MessengerTempData.GroupUniqueID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).CommunicationId;
                MessengerTempData.ConnectionID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).CommunicationId; ;
                MessengerTempData.RecipientID = 0;
            }
            else
            {
                MessengerTempData.GroupID = 0;
                MessengerTempData.IsGroup = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).IsGroup;
                MessengerTempData.RecipientName = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).FirstName + " "+ MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).LastName;
                MessengerTempData.GroupUniqueID = null;
                MessengerTempData.RecipientID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).Id;
                MessengerTempData.ConnectionID = MessengerTempData.RecentEmployeeLists.ChatEmployeeList.ElementAt(itemPosition).CommunicationId;

                var data = await MessengerService.GetRecentContacts(EmployeeTempData.EmployeeID);
                //var selectedData = data.EmployeeList.ChatEmployeeList.Find(x => x.Id == MessengerTempData.RecipientID);
                //MessengerTempData.ConnectionID = selectedData.CommunicationId;
                if (MessengerTempData.RecipientsConnectionID == null)
                {
                    MessengerTempData.RecipientsConnectionID = new Dictionary<string, string>();
                }
                if (!MessengerTempData.RecipientsConnectionID.ContainsKey(MessengerTempData.RecipientID.ToString()))
                {
                    if (MessengerTempData.ConnectionID != null)
                        MessengerTempData.RecipientsConnectionID.Add(MessengerTempData.RecipientID.ToString(), MessengerTempData.ConnectionID);
                }
            }
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            MessengerPersonalChatFragment messengerPersonalChatFragment = new MessengerPersonalChatFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerPersonalChatFragment).Commit();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerRecentContactItemView, parent, false);
            return new MessengerRecentContactsRecycleHolder(itemView);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Fragments;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerGroupChatViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public IItemClickListener itemClickListener;
        public Button group_Button;
        public TextView groupName_TextView;
        public MessengerGroupChatViewHolder(View groups) : base(groups)
        {
            group_Button = groups.FindViewById<Button>(Resource.Id.group_ImageView);
            groupName_TextView = groups.FindViewById<TextView>(Resource.Id.groupName_TextView);
            groups.SetOnClickListener(this);
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
    public class MessengerGroupChatAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        MessengerGroupContactViewModel messengerGroupContactViewModel;
        private List<ChatEmployeeList> groups = new List<ChatEmployeeList>();
        private MessengerGroupChatViewHolder messengerGroup;
        private char[] firstInitial;
        private char[] secondInitial;
        public MessengerGroupChatAdapter(Context context, List<ChatEmployeeList> groups, MessengerGroupContactViewModel viewModel)
        {
            this.context = context;
            this.groups = groups;
            this.messengerGroupContactViewModel = viewModel;

        }

        public override int ItemCount
        {
            get
            {
                return groups.Count;
            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            messengerGroup = holder as MessengerGroupChatViewHolder;
            messengerGroup.groupName_TextView.Text = groups[position].FirstName + groups[position].LastName;
            if (!string.IsNullOrEmpty(groups[position].FirstName))
            {
                firstInitial = groups[position].FirstName.ToCharArray();
            }
            else
            {
                firstInitial = null;
            }
            if (!string.IsNullOrEmpty(groups[position].LastName))
            {
                secondInitial = groups[position].LastName.ToCharArray();
            }
            else
            {
                secondInitial = null;
            }
            if (firstInitial != null && secondInitial != null)
            {
                messengerGroup.group_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
            }
            else if (firstInitial != null)
            {
                if (firstInitial.Length > 0)
                {
                    messengerGroup.group_Button.Text = firstInitial.ElementAt(0).ToString() + firstInitial.ElementAt(1).ToString();
                }
            }
            else
            {
                if (secondInitial.Length > 0)
                {
                    messengerGroup.group_Button.Text = secondInitial.ElementAt(0).ToString() + secondInitial.ElementAt(1).ToString();
                }
            }
            messengerGroup.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            int itemPosition = messengerGroupContactViewModel.GroupList.ChatEmployeeList.IndexOf(groups[position]);
            MessengerTempData.resetChatData();
            MessengerTempData.GroupID = MessengerTempData.GroupLists.ChatEmployeeList[itemPosition].Id;
            MessengerTempData.IsGroup = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(itemPosition).IsGroup;
            MessengerTempData.GroupName = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(itemPosition).FirstName;
            var data = MessengerTempData.GroupLists.ChatEmployeeList.Find(x => x.Id == MessengerTempData.GroupID);
            MessengerTempData.GroupUniqueID = data.CommunicationId;
            MessengerTempData.ConnectionID = data.CommunicationId; 
            MessengerTempData.RecipientID = 0;
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            MessengerPersonalChatFragment messengerPersonalChatFragment = new MessengerPersonalChatFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerPersonalChatFragment).Commit();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerGroupChatItemView, parent, false);
            return new MessengerGroupChatViewHolder(itemView);
        }
    }

  
}
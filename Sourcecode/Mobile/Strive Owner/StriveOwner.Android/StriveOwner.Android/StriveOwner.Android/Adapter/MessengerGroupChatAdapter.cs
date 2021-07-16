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
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using StriveOwner.Android.Fragments;
using StriveOwner.Android.Listeners;

namespace StriveOwner.Android.Adapter
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
        private List<ChatEmployeeList> groups = new List<ChatEmployeeList>();
        private MessengerGroupChatViewHolder messengerGroup;
        public MessengerGroupChatAdapter(Context context, List<ChatEmployeeList> groups)
        {
            this.context = context;
            this.groups = groups;
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
            messengerGroup.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            MessengerTempData.resetChatData();
            MessengerTempData.GroupID = MessengerTempData.GroupLists.ChatEmployeeList[position].Id;
            MessengerTempData.IsGroup = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(position).IsGroup;
            MessengerTempData.GroupName = MessengerTempData.GroupLists.ChatEmployeeList.ElementAt(position).FirstName;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Messenger;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerGroupChatViewHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button group_Button;
        public TextView groupName_TextView;
        public MessengerGroupChatViewHolder(View groups) : base(groups)
        {
            group_Button = groups.FindViewById<Button>(Resource.Id.group_ImageView);
            groupName_TextView = groups.FindViewById<TextView>(Resource.Id.groupName_TextView);
        }

        public void OnClick(View v)
        {
            
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
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerGroupChatItemView, parent, false);
            return new MessengerGroupChatViewHolder(itemView);
        }
    }

  
}
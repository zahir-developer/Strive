using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.ViewModels;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Utils.Employee;
using StriveEmployee.Android.Enums;

namespace StriveOwner.Android.Adapter
{
    public class SenderChatViewHolder : RecyclerView.ViewHolder
    {
        public TextView chatMessage_TextView;
        public TextView chatMessageTime_TextView;
        public SenderChatViewHolder(View senderChat) : base(senderChat)
        {
            chatMessage_TextView = senderChat.FindViewById<TextView>(Resource.Id.sender_ChatBubble);
            chatMessageTime_TextView = senderChat.FindViewById<TextView>(Resource.Id.sender_ChatTime);
        }
        public void bindMessage(string Message, string MessageTime)
        {
            chatMessage_TextView.Text = Message;
            var date = MessageTime.Split("T");
            DateTime localMessageTime = DateTime.Parse(date[0]);
            chatMessageTime_TextView.Text = localMessageTime.ToString("HH:mm tt", CultureInfo.CurrentCulture) + " | " + localMessageTime.ToString("MMM");
        }
    }
    public class RecipientChatViewHolder : RecyclerView.ViewHolder
    {
        public TextView chatMessage_TextView;
        public TextView chatMessageTime_TextView;
        public TextView chatRecipientName_TextView;
        public RecipientChatViewHolder(View recipientChat) : base(recipientChat)
        {
            chatMessage_TextView = recipientChat.FindViewById<TextView>(Resource.Id.recipient_ChatBubble);
            chatMessageTime_TextView = recipientChat.FindViewById<TextView>(Resource.Id.recipient_ChatTime);
            chatRecipientName_TextView = recipientChat.FindViewById<TextView>(Resource.Id.contactName_TextView);
        }
        public void bindMessage(string Message, string MessageTime, string ContactName)
        {
            chatMessage_TextView.Text = Message;
            var date = MessageTime.Split("T");
            DateTime localMessageTime = DateTime.Parse(date[0]);
            chatMessageTime_TextView.Text = localMessageTime.ToString("HH:mm tt", CultureInfo.CurrentCulture)+ " | "+ localMessageTime.ToString("MMM");
            chatRecipientName_TextView.Text = ContactName;
        }
    }
    class MessengerChatAdapter : RecyclerView.Adapter
    {

        Context context;
        private const int TYPE_SENDER = 0;
        private const int TYPE_RECIPIENT = 1;
        private SenderChatViewHolder sender_holder;
        private RecipientChatViewHolder recipient_holder;
        private MvxObservableCollection<ChatMessageDetail> chatMessages = new MvxObservableCollection<ChatMessageDetail>();
        int adapterPosition;
        public MessengerChatAdapter(Context context, MvxObservableCollection<ChatMessageDetail> chatMessages)
        {
            this.context = context;
            this.chatMessages = chatMessages;
        }

        public override int ItemCount
        {
            get 
            {
                return chatMessages.Count;
            }
        }

        public override int GetItemViewType(int position)
        {
            if(chatMessages[position].SenderId == EmployeeTempData.EmployeeID)
            {
                adapterPosition = position;
                return TYPE_SENDER;
            }
            else
            {
                adapterPosition = position;
                return TYPE_RECIPIENT;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            string Message = chatMessages[position].MessageBody;
            string MessageTime = chatMessages[position].CreatedDate.ToString();
            switch (holder.ItemViewType)
            {
                case TYPE_SENDER:
                    sender_holder = holder as SenderChatViewHolder;
                    sender_holder.bindMessage(Message, MessageTime);
                    break;

                case TYPE_RECIPIENT:
                    var ContactName = chatMessages[position].SenderFirstName;
                    var Datetime = chatMessages[position].CreatedDate.ToString();
                    recipient_holder = holder as RecipientChatViewHolder;
                    recipient_holder.bindMessage(Message, MessageTime, ContactName);
                    break;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if(viewType == TYPE_RECIPIENT)
            {
                LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
                View itemView = layoutInflater.Inflate(Resource.Layout.Recipient_ChatBubble, parent, false);
                return new RecipientChatViewHolder(itemView);
            }
            else
            {
                LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
                View itemView = layoutInflater.Inflate(Resource.Layout.Sender_ChatBubble, parent, false);
                return new SenderChatViewHolder(itemView);
            }
            return null;
        }
    }

    
}
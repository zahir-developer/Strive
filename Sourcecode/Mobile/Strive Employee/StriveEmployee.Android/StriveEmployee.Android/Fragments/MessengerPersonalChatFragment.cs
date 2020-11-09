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
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;

namespace StriveEmployee.Android.Fragments
{
    [Activity (WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MessengerPersonalChatFragment : MvxFragment<MessengerPersonalChatViewModel>
    {
        private EditText chatMessage_EditText;
        private TextView personalContactName_TextView;
        private RecyclerView chatMessage_RecyclerView;
        private MessengerChatAdapter messengerChat_Adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerPersonalChat_Fragment, null);
            this.ViewModel = new MessengerPersonalChatViewModel();

            personalContactName_TextView = rootView.FindViewById<TextView>(Resource.Id.personalContactName_TextView);
            chatMessage_EditText = rootView.FindViewById<EditText>(Resource.Id.chatMessage_EditText);
            chatMessage_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.message_recyclerview);
            chatMessage_EditText.SetCompoundDrawablesWithIntrinsicBounds(0, 0, Resource.Drawable.sendcircle, 0);

            personalContactName_TextView.Text = MessengerTempData.RecipientName;
            getChatData();
            return rootView;
        }

        private async void getChatData()
        {
            ChatDataRequest chatData = new ChatDataRequest
            {
                SenderId = EmployeeTempData.EmployeeID,
                RecipientId = MessengerTempData.RecipientID,
                GroupId = 0
            };
            await this.ViewModel.GetAllMessages(chatData);
            if(ViewModel.chatMessages != null || ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Count != 0)
            {
                messengerChat_Adapter = new MessengerChatAdapter(Context, ViewModel.chatMessages.ChatMessage.ChatMessageDetail);
                var layoutManager = new LinearLayoutManager(Context);
                chatMessage_RecyclerView.SetLayoutManager(layoutManager);
                chatMessage_RecyclerView.SetAdapter(messengerChat_Adapter);
            }
        }
    }
}
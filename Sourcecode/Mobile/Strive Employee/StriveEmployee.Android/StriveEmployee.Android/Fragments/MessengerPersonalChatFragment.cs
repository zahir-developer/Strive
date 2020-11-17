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
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.Android.Adapter;
using static Android.Views.View;
using PopupMenu = Android.Widget.PopupMenu;

namespace StriveEmployee.Android.Fragments
{
    [Activity (WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MessengerPersonalChatFragment : MvxFragment<MessengerPersonalChatViewModel>
    {
        private Button personalChat_Button;
        private EditText chatMessage_EditText;
        private ImageButton sendChat_Button;
        private ImageButton chatMenu_ImageButton;
        private TextView personalContactName_TextView;
        private PopupMenu chat_PopupMenu;
        private IMenu chat_Menu;
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
         
            personalChat_Button = rootView.FindViewById<Button>(Resource.Id.personalChatBack_Button);
            sendChat_Button = rootView.FindViewById<ImageButton>(Resource.Id.chatSend_ImageButton);
            chatMenu_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.chatMenu_ImageButton);
            personalContactName_TextView = rootView.FindViewById<TextView>(Resource.Id.personalContactName_TextView);
            chatMessage_EditText = rootView.FindViewById<EditText>(Resource.Id.chatMessage_EditText);
            chatMessage_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.message_recyclerview);
            personalContactName_TextView.Text = MessengerTempData.RecipientName;
            personalChat_Button.Click += PersonalChat_Button_Click;
            sendChat_Button.Click += SendChat_Button_Click;
            chatMenu_ImageButton.Click += ChatMenu_ImageButton_Click;
            chat_PopupMenu = new PopupMenu(Context, chatMenu_ImageButton);
            chat_Menu = chat_PopupMenu.Menu;
            chat_PopupMenu.MenuInflater.Inflate(Resource.Menu.chat_menu, chat_Menu);
            chat_PopupMenu.MenuItemClick += Chat_PopupMenu_MenuItemClick;
            chatMenu_ImageButton.Visibility = MessengerTempData.IsGroup ? ViewStates.Visible : ViewStates.Gone;
            getChatData();
            return rootView;
        }

        private void Chat_PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_viewParticipants:
                    
                    break;


            }

        }

        private void ChatMenu_ImageButton_Click(object sender, EventArgs e)
        {
            chat_PopupMenu.Show();
        }

        private async void SendChat_Button_Click(object sender, EventArgs e)
        {
            var data = new ChatMessageDetail()
            {
                MessageBody = chatMessage_EditText.Text,
                ReceipientId = 0,
                RecipientFirstName = "",
                RecipientLastName = "",
                SenderFirstName = "",
                SenderLastName = "",
                SenderId = EmployeeTempData.EmployeeID,
                CreatedDate = DateTime.UtcNow
            };
            if(ViewModel.chatMessages == null)
            {
                ViewModel.chatMessages = new PersonalChatMessages();
                ViewModel.chatMessages.ChatMessage = new ChatMessage();
                ViewModel.chatMessages.ChatMessage.ChatMessageDetail = new List<ChatMessageDetail>();
                ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Add(data);
                messengerChat_Adapter = new MessengerChatAdapter(Context, ViewModel.chatMessages.ChatMessage.ChatMessageDetail);
                 
                var layoutManager = new LinearLayoutManager(Context);
                chatMessage_RecyclerView.SetLayoutManager(layoutManager);
                chatMessage_RecyclerView.SetAdapter(messengerChat_Adapter);
                chatMessage_RecyclerView.ScrollToPosition(ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Count);
            }
            else
            {
                ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Add(data);
            }
            messengerChat_Adapter.NotifyItemInserted(ViewModel.chatMessages.ChatMessage.ChatMessageDetail.Count);
            this.ViewModel.Message = chatMessage_EditText.Text;
            await this.ViewModel.SendMessage();
            if(this.ViewModel.SentSuccess)
            {
                chatMessage_EditText.Text = "";
            }
        }

        private void PersonalChat_Button_Click(object sender, EventArgs e)
        {
            MessengerFragment messengerFragment = new MessengerFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
        }

        private async void getChatData()
        {
            ChatDataRequest chatData = new ChatDataRequest
            {
                SenderId = EmployeeTempData.EmployeeID,
                RecipientId = MessengerTempData.RecipientID,
                GroupId = MessengerTempData.GroupID
            };
            await this.ViewModel.GetAllMessages(chatData);
            if(ViewModel.chatMessages != null )
            {
                messengerChat_Adapter = new MessengerChatAdapter(Context, ViewModel.chatMessages.ChatMessage.ChatMessageDetail);
                var layoutManager = new LinearLayoutManager(Context);
                layoutManager.StackFromEnd = true;
                chatMessage_RecyclerView.SetLayoutManager(layoutManager);
                chatMessage_RecyclerView.SetAdapter(messengerChat_Adapter);
               
            }
        }
    }
}
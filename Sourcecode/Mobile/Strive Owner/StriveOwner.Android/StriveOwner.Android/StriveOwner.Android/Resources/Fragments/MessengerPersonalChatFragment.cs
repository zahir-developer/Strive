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
using Strive.Core.Services.HubServices;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveOwner.Android.Adapter;
using static Android.Views.View;
using PopupMenu = Android.Widget.PopupMenu;
using IList = System.Collections.IList;
using System.Collections.ObjectModel;
using Strive.Core.Services.Implementations;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using MvvmCross.ViewModels;

namespace StriveOwner.Android.Fragments
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
        private MvxFragment selected_MvxFragment;
        private MessengerViewParticipantsFragment viewParticipants_Fragment;
        private static ObservableCollection<SendChatMessage> messages { get; set; }
        private static List<SendChatMessage> privateMessages { get; set; }
        private static List<SendChatMessage> groupMessages { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerPersonalChat_Fragment, null);
            this.ViewModel = new MessengerPersonalChatViewModel();

            MessengerTempData.resetParticipantInfo();
            personalChat_Button = rootView.FindViewById<Button>(Resource.Id.personalChatBack_Button);
            sendChat_Button = rootView.FindViewById<ImageButton>(Resource.Id.chatSend_ImageButton);
            chatMenu_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.chatMenu_ImageButton);
            personalContactName_TextView = rootView.FindViewById<TextView>(Resource.Id.personalContactName_TextView);
            chatMessage_EditText = rootView.FindViewById<EditText>(Resource.Id.chatMessage_EditText);
            chatMessage_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.message_recyclerview);
            personalContactName_TextView.Text = MessengerTempData.IsGroup ? MessengerTempData.GroupName : MessengerTempData.RecipientName;
            personalChat_Button.Click += PersonalChat_Button_Click;
            sendChat_Button.Click += SendChat_Button_Click;
            chatMenu_ImageButton.Click += ChatMenu_ImageButton_Click;
            chat_PopupMenu = new PopupMenu(Context, chatMenu_ImageButton);
            chat_Menu = chat_PopupMenu.Menu;
            chat_PopupMenu.MenuInflater.Inflate(Resource.Menu.chat_menu, chat_Menu);
            chat_PopupMenu.MenuItemClick += Chat_PopupMenu_MenuItemClick;
            chatMenu_ImageButton.Visibility = MessengerTempData.IsGroup ? ViewStates.Visible : ViewStates.Gone;
            ChatHubMessagingService.PrivateMessageList.CollectionChanged += PrivateMessageList_CollectionChanged;
            ChatHubMessagingService.GroupMessageList.CollectionChanged += GroupMessageList_CollectionChanged;
            getChatData();
            getCommunicationID();
            return rootView;
        }

        private void GroupMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (groupMessages == null)
                {
                    groupMessages = new List<SendChatMessage>();
                }
                foreach (var item in e.NewItems)
                {
                    var datas = (SendChatMessage)item;
                    groupMessages.Add(datas);
                }
                var lastMessage = groupMessages.Last();
                if (MessengerTempData.IsGroup && lastMessage.chatMessageRecipient.recipientGroupId == MessengerTempData.GroupID)
                {
                    var message = new ChatMessageDetail()
                    {
                        MessageBody = lastMessage.chatMessage.messagebody,
                        ReceipientId = EmployeeTempData.EmployeeID,
                        RecipientFirstName = "",
                        RecipientLastName = "",
                        SenderFirstName = lastMessage.fullName,
                        SenderLastName = "",
                        SenderId = (int)lastMessage.chatMessageRecipient.senderId,
                        CreatedDate = DateTime.UtcNow
                    };
                    if(MessengerTempData.GroupUniqueID == lastMessage.groupId && EmployeeTempData.EmployeeID != message.SenderId)
                    {
                        ViewModel.ChatMessages.Add(message);
                        messengerChat_Adapter.NotifyItemInserted(ViewModel.ChatMessages.Count);
                        chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count);
                    }
                   
                }
               
            }
        }

        private void PrivateMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if(privateMessages == null)
                {
                    privateMessages = new List<SendChatMessage>();
                }
                foreach(var item in e.NewItems)
                {
                    var datas = (SendChatMessage)item;
                    privateMessages.Add(datas);
                }

                var lastMessage = privateMessages.Last();

                var message = new ChatMessageDetail()
                {
                    MessageBody = lastMessage.chatMessage.messagebody,
                    ReceipientId = EmployeeTempData.EmployeeID,
                    RecipientFirstName = "",
                    RecipientLastName = "",
                    SenderFirstName = lastMessage.fullName ?? lastMessage.firstName,
                    SenderLastName = "",
                    SenderId = (int)lastMessage.chatMessageRecipient.senderId,
                    CreatedDate = DateTime.UtcNow
                };
                if (MessengerTempData.RecipientID == message.SenderId || MessengerTempData.GroupID == message.ReceipientId)
                {
                    ViewModel.ChatMessages.Add(message);
                    messengerChat_Adapter.NotifyItemInserted(ViewModel.ChatMessages.Count);
                    chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count + 1);
                }
                
            }
        }

        private void Chat_PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_viewParticipants:
                    selected_MvxFragment = new MessengerViewParticipantsFragment();
                    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
                    break;


            }

        }

        private void ChatMenu_ImageButton_Click(object sender, EventArgs e)
        {
            chat_PopupMenu.Show();
        }

        private async void SendChat_Button_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(chatMessage_EditText.Text))
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
                if (ViewModel.ChatMessages == null)
                {
                    ViewModel.ChatMessages = new MvxObservableCollection<ChatMessageDetail>();
                    //ViewModel.chatMessages.ChatMessage = new ChatMessage();
                    //ViewModel.chatMessages.ChatMessage.ChatMessageDetail = new List<ChatMessageDetail>();
                    ViewModel.ChatMessages.Add(data);
                    messengerChat_Adapter = new MessengerChatAdapter(Context, ViewModel.ChatMessages);

                    var layoutManager = new LinearLayoutManager(Context);
                    chatMessage_RecyclerView.SetLayoutManager(layoutManager);
                    chatMessage_RecyclerView.SetAdapter(messengerChat_Adapter);
                    chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count);
                }
                else
                {
                    ViewModel.ChatMessages.Add(data);
                }
                messengerChat_Adapter.NotifyItemInserted(ViewModel.ChatMessages.Count);
                this.ViewModel.Message = chatMessage_EditText.Text;
                await this.ViewModel.SendMessage();
                if (this.ViewModel.SentSuccess)
                {
                    chatMessage_EditText.Text = "";
                }
            }
            else
            {
                this.ViewModel.EmptyChatMessageError();
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
                SenderId = MessengerTempData.IsGroup ? 0 : EmployeeTempData.EmployeeID,
                RecipientId = MessengerTempData.RecipientID,
                GroupId = MessengerTempData.GroupID
            };
            await this.ViewModel.GetAllMessages(chatData);
            if(ViewModel.ChatMessages != null )
            {
                messengerChat_Adapter = new MessengerChatAdapter(Context, ViewModel.ChatMessages);
                var layoutManager = new LinearLayoutManager(Context);
                layoutManager.StackFromEnd = true;
                chatMessage_RecyclerView.SetLayoutManager(layoutManager);
                chatMessage_RecyclerView.SetAdapter(messengerChat_Adapter);               
            }
        }
        private async void getCommunicationID()
        {
            MessengerService messengerService = new MessengerService();
            var data = await messengerService.GetContacts(new GetAllEmployeeDetail_Request
            {
                startDate = null,
                endDate = null,
                locationId = null,
                pageNo = null,
                pageSize = null,
                query = "",
                sortOrder = null,
                sortBy = null,
                status = true,
            });
            var selectedData = data.EmployeeList.Employee.Find(x => x.EmployeeId == MessengerTempData.RecipientID);
            if (selectedData != null)
            {
                MessengerTempData.ConnectionID = selectedData.CommunicationId;
            }
            else
            {
                MessengerTempData.ConnectionID = "0";
            }
        }
    }
}
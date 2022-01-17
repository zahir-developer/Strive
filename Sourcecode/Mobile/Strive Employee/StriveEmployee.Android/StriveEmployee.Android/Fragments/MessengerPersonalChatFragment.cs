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
using StriveEmployee.Android.Adapter;
using static Android.Views.View;
using PopupMenu = Android.Widget.PopupMenu;
using IList = System.Collections.IList;
using System.Collections.ObjectModel;
using Strive.Core.Services.Implementations;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using MvvmCross.ViewModels;
using Android.Support.V4.Content;
using Android.Graphics;
using StriveEmployee.Android.Views;
using Strive.Core.Models.Employee.Messenger;

namespace StriveEmployee.Android.Fragments
{
    [Activity (WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MessengerPersonalChatFragment : MvxFragment<MessengerPersonalChatViewModel> , ViewTreeObserver.IOnGlobalLayoutListener
    {
        private Button personalChat_Button;
        private EditText chatMessage_EditText;
        private ImageButton sendChat_Button;
        private TextView chatName_TextView;
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
        private bool keyboardListenersAttached = false;
        private ViewGroup rootLayout;
        private LinearLayout listLayout;
        public static string ConnectionID;
        private bool isSendClicked;
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
            chatName_TextView = rootView.FindViewById<TextView>(Resource.Id.chatName);
            personalContactName_TextView.Text = MessengerTempData.IsGroup ? MessengerTempData.GroupName : MessengerTempData.RecipientName;
            personalChat_Button.Click += PersonalChat_Button_Click;
            sendChat_Button.Click += SendChat_Button_Click;
            chatMenu_ImageButton.Click += ChatMenu_ImageButton_Click;
            chat_PopupMenu = new PopupMenu(Context, chatMenu_ImageButton);
            chat_Menu = chat_PopupMenu.Menu;
            chat_PopupMenu.MenuInflater.Inflate(Resource.Menu.chat_menu, chat_Menu);
            chat_PopupMenu.MenuItemClick += Chat_PopupMenu_MenuItemClick;
            isSendClicked = false;
            //chatMenu_ImageButton.Visibility = MessengerTempData.IsGroup ? ViewStates.Visible : ViewStates.Gone;
            if (!MessengerTempData.IsGroup)
            {
                chatName_TextView.Text = "Personal Chat";
            }
            else
            {
                chatName_TextView.Text = "Group Chat";
            }
            //ChatHubMessagingService.PrivateMessageList.CollectionChanged += PrivateMessageList_CollectionChanged;
            //ChatHubMessagingService.GroupMessageList.CollectionChanged += GroupMessageList_CollectionChanged;

            getChatData();
            //getCommunicationID();
            EstablishHubConnection();
            rootLayout = (ViewGroup)rootView.FindViewById(Resource.Id.layout);
            listLayout = rootView.FindViewById<LinearLayout>(Resource.Id.listLayout);
            return rootView;
        }

        public override void OnResume()
        {
            attachKeyboardListeners();
            base.OnResume();
            

        }
        public override void OnPause()
        {
            
            if (Activity != null && Activity.GetType() == typeof(DashboardView))
            {
                ((DashboardView)Activity).getBottomNavView().Visibility = ViewStates.Visible;

            }
            removeKeyboardListeners();
            base.OnPause();
        }
        public override void OnDestroy()
        {
          
            if (Activity != null && Activity.GetType() == typeof(DashboardView))
            {
                ((DashboardView)Activity).getBottomNavView().Visibility = ViewStates.Visible;

            }
            removeKeyboardListeners();
            base.OnDestroy();
        }
        private async void EstablishHubConnection()
        {
            ConnectionID = await ViewModel.StartCommunication();

            await ChatHubMessagingService.SendEmployeeCommunicationId(EmployeeTempData.EmployeeID.ToString(), ConnectionID);

            MessengerTempData.ConnectionID = ConnectionID;

            //await ViewModel.SetChatCommunicationDetails(MessengerTempData.ConnectionID);
            await ChatHubMessagingService.SubscribeChatEvent();

            if (ChatHubMessagingService.RecipientsID == null)
            {
                ChatHubMessagingService.RecipientsID = new ObservableCollection<RecipientsCommunicationID>();
                ChatHubMessagingService.RecipientsID.CollectionChanged += RecipientsID_CollectionChanged;

            }

            //await ChatHubMessagingService.SubscribeChatEvent();
            ChatHubMessagingService.PrivateMessageList.CollectionChanged += PrivateMessageList_CollectionChanged;
            ChatHubMessagingService.GroupMessageList.CollectionChanged += GroupMessageList_CollectionChanged;

        }
        private void RecipientsID_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (MessengerTempData.RecipientsConnectionID == null)
                {
                    MessengerTempData.RecipientsConnectionID = new Dictionary<string, string>();
                }
                foreach (var item in e.NewItems)
                {
                    var datas = (RecipientsCommunicationID)item;
                    if (MessengerTempData.RecipientsConnectionID.ContainsKey(datas.employeeId))
                    {
                        MessengerTempData.RecipientsConnectionID.Remove(datas.employeeId);
                        MessengerTempData.RecipientsConnectionID.Add(datas.employeeId, datas.communicationId);
                    }
                    else
                    {
                        MessengerTempData.RecipientsConnectionID.Add(datas.employeeId, datas.communicationId);
                    }
                }
            }
        }
        private void GroupMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                    var newChatItem = (SendChatMessage)item;

                    ChatMessageDetail chatMessageDetail = new ChatMessageDetail();
                    chatMessageDetail.CreatedDate = DateTime.Parse(newChatItem.chatMessage.createdDate);
                    chatMessageDetail.MessageBody = newChatItem.chatMessage.messagebody;
                    chatMessageDetail.ReceipientId = (int)newChatItem.chatMessageRecipient.chatRecipientId;
                    chatMessageDetail.RecipientFirstName = newChatItem.firstName;
                    chatMessageDetail.RecipientLastName = newChatItem.lastName;

                    //Sender 1st name, last name is not coming in the outpu
                    chatMessageDetail.SenderId = (int)newChatItem.chatMessageRecipient.senderId;
                    chatMessageDetail.SenderFirstName = newChatItem.fullName;
                    chatMessageDetail.SenderLastName = newChatItem.lastName;

                    chatMessageDetail.chatMessageId = newChatItem.chatMessageRecipient.chatMessageId;
                    if (ViewModel.ChatMessages == null)
                    {
                        getChatData();
                    }
                    if (!ViewModel.ChatMessages.Any(x => x.chatMessageId == chatMessageDetail.chatMessageId))
                    {
                        if (MessengerTempData.IsGroup)
                        {
                            ViewModel.ChatMessages.Add(chatMessageDetail);
                        }

                    }
                    messengerChat_Adapter.NotifyItemInserted(ViewModel.ChatMessages.Count);
                    chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count);
                    messengerChat_Adapter.NotifyDataSetChanged();
                }
               

            }
        }

        private void PrivateMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                    var newChatItem = (SendChatMessage)item;

                    ChatMessageDetail chatMessageDetail = new ChatMessageDetail();
                    chatMessageDetail.CreatedDate = DateTime.Parse(newChatItem.chatMessage.createdDate);
                    chatMessageDetail.MessageBody = newChatItem.chatMessage.messagebody;
                    chatMessageDetail.ReceipientId = (int)newChatItem.chatMessageRecipient.recipientId;
                    chatMessageDetail.RecipientFirstName = newChatItem.firstName;
                    chatMessageDetail.RecipientLastName = newChatItem.lastName;

                    //Sender 1st name, last name is not coming in the outpu
                    chatMessageDetail.SenderId = (int)newChatItem.chatMessageRecipient.senderId;
                    chatMessageDetail.SenderFirstName = newChatItem.fullName;
                    chatMessageDetail.SenderLastName = newChatItem.lastName;

                    chatMessageDetail.chatMessageId = newChatItem.chatMessageRecipient.chatMessageId;

                    if (!ViewModel.ChatMessages.Any(x => x.chatMessageId == chatMessageDetail.chatMessageId))
                    {
                        if (!MessengerTempData.IsGroup)
                        {
                            ViewModel.ChatMessages.Add(chatMessageDetail);
                        }
                      
                    }
                    messengerChat_Adapter.NotifyItemInserted(ViewModel.ChatMessages.Count);
                    chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count + 1);
                    messengerChat_Adapter.NotifyDataSetChanged();
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
            if (!isSendClicked)
            {
                if (!String.IsNullOrEmpty(chatMessage_EditText.Text))
                {
                    isSendClicked = true;
                    var data = new ChatMessageDetail()
                    {
                        MessageBody = chatMessage_EditText.Text,
                        ReceipientId = 0,
                        RecipientFirstName = "",
                        RecipientLastName = "",
                        SenderFirstName = "",
                        SenderLastName = "",
                        SenderId = EmployeeTempData.EmployeeID,
                        CreatedDate = DateTime.UtcNow.ToLocalTime()
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
                    chatMessage_RecyclerView.ScrollToPosition(ViewModel.ChatMessages.Count + 1);
                    messengerChat_Adapter.NotifyDataSetChanged();
                    this.ViewModel.Message = chatMessage_EditText.Text;
                    await this.ViewModel.SendMessage();
                    if (this.ViewModel.SentSuccess)
                    {
                        chatMessage_EditText.Text = "";
                        isSendClicked = false;
                        getChatData();
                    }
                }
                else
                {
                    this.ViewModel.EmptyChatMessageError();
                }
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

        public void OnGlobalLayout()
        {

            int heightDiff = rootLayout.RootView.Height -
        rootLayout.Height;
            if (this.Activity != null)
            {
                int contentViewTop =
              this.Activity.Window.FindViewById(Window.IdAndroidContent).Top;
                LocalBroadcastManager broadcastManager =
                LocalBroadcastManager.GetInstance(this.Context);
                Rect r = new Rect();
                rootLayout.GetWindowVisibleDisplayFrame(r);
                int screenHeight = rootLayout.RootView.Height;

                // r.bottom is the position above soft keypad or device button.
                // if keypad is shown, the r.bottom is smaller than that before.
                int keypadHeight = screenHeight - r.Bottom;
                if (keypadHeight > screenHeight * 0.15)
                {
                    onHideKeyboard();

                    Intent intent = new Intent("KeyboardWillHide");
                    broadcastManager.SendBroadcast(intent);
                }
                else
                {
                    int keyboardHeight = heightDiff - contentViewTop;
                    onShowKeyboard(keyboardHeight);

                    Intent intent = new Intent("KeyboardWillShow");
                    intent.PutExtra("KeyboardHeight", keyboardHeight);
                    broadcastManager.SendBroadcast(intent);
                }
            }
            else
            {

            }
        }
        protected void onShowKeyboard(int keyboardHeight)
        {
            if (Activity != null && Activity.GetType() == typeof(DashboardView))
            {
                ((DashboardView)Activity).getBottomNavView().Visibility = ViewStates.Visible;

            }
            LinearLayout.LayoutParams layoutParams = (LinearLayout.LayoutParams)listLayout.LayoutParameters;
            layoutParams.BottomMargin = 60;
            listLayout.LayoutParameters = layoutParams;

        }
        protected void onHideKeyboard()
        {
            if(Activity!=null && Activity.GetType() == typeof(DashboardView))
            {
              ((DashboardView)Activity).getBottomNavView().Visibility = ViewStates.Gone;
 
            }
            LinearLayout.LayoutParams layoutParams = (LinearLayout.LayoutParams)listLayout.LayoutParameters;
            layoutParams.BottomMargin = 0;
            listLayout.LayoutParameters = layoutParams;

        }

        protected void attachKeyboardListeners()
        {
            if (keyboardListenersAttached)
            {
                return;
            }

            rootLayout.ViewTreeObserver.AddOnGlobalLayoutListener(this);
            keyboardListenersAttached = true;
        }
        protected void removeKeyboardListeners()
        {
           
            rootLayout.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
            keyboardListenersAttached = false;
        }
    }
}
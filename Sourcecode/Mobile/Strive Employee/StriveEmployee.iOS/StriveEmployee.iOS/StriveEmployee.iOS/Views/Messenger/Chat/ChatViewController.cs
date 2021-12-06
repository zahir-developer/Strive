using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using MvvmCross.Binding.BindingContext;
using UIKit;
using System.Linq;

namespace StriveEmployee.iOS.Views.Messenger.Chat
{
    public partial class ChatViewController : MvxViewController<MessengerPersonalChatViewModel>
    {
        UITableView chatTableView;
        UIView messageBoxContainer;
        UITextView messageTextView;
        UILabel chatMessagePlaceholderLabel;
        NSLayoutConstraint messageBoxContainerBottomConstraint;
        //MessengerPersonalChatViewModel ViewModel;
        

        public List<string> Chats = new List<string>();

        public ChatViewController() : base("ChatViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //ViewModel = new MessengerPersonalChatViewModel();

            getChatData();            
            RegisterKeyboardObserver();

            //chatTableView.WeakDelegate = this;
            //chatTableView.WeakDataSource = this;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        async void SetupView()
        {
            View.BackgroundColor = UIColor.White;

            chatTableView = new UITableView(CGRect.Empty);
            chatTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            chatTableView.EstimatedRowHeight = 110;
            chatTableView.RowHeight = UITableView.AutomaticDimension;
            chatTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            chatTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            View.Add(chatTableView);

            messageBoxContainer = new UIView(CGRect.Empty);
            messageBoxContainer.TranslatesAutoresizingMaskIntoConstraints = false;
            messageBoxContainer.BackgroundColor = UIColor.White;
            messageBoxContainer.Layer.BorderWidth = 2;
            messageBoxContainer.Layer.BorderColor = UIColor.LightGray.CGColor;
            View.Add(messageBoxContainer);

            messageTextView = new UITextView(CGRect.Empty);
            messageTextView.TranslatesAutoresizingMaskIntoConstraints = false;
            messageTextView.Font = UIFont.SystemFontOfSize(16);
            messageTextView.TextColor = UIColor.Black;
            messageTextView.WeakDelegate = this;
            messageBoxContainer.Add(messageTextView);

            chatMessagePlaceholderLabel = new UILabel(CGRect.Empty);
            chatMessagePlaceholderLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            chatMessagePlaceholderLabel.Text = "Type the message here";
            chatMessagePlaceholderLabel.Font = UIFont.SystemFontOfSize(16);
            chatMessagePlaceholderLabel.TextColor = UIColor.Gray;
            messageBoxContainer.Add(chatMessagePlaceholderLabel);

            var sendImageView = new UIImageView(CGRect.Empty);
            sendImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            sendImageView.Image = UIImage.FromBundle("send-msg");
            sendImageView.UserInteractionEnabled = true;
            sendImageView.AddGestureRecognizer(new UITapGestureRecognizer(OnSend));
            messageBoxContainer.Add(sendImageView);

            chatTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            chatTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            chatTableView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            chatTableView.BottomAnchor.ConstraintEqualTo(messageBoxContainer.TopAnchor).Active = true;

            messageBoxContainer.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 20).Active = true;
            messageBoxContainer.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, constant: -20).Active = true;
            messageBoxContainer.HeightAnchor.ConstraintEqualTo(60).Active = true;
            messageBoxContainerBottomConstraint = messageBoxContainer.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, constant: -10);
            messageBoxContainerBottomConstraint.Priority = 249;
            messageBoxContainerBottomConstraint.Active = true;

            messageTextView.LeadingAnchor.ConstraintEqualTo(messageBoxContainer.LeadingAnchor, constant: 20).Active = true;
            messageTextView.TrailingAnchor.ConstraintEqualTo(sendImageView.LeadingAnchor, constant: -20).Active = true;
            messageTextView.TopAnchor.ConstraintEqualTo(messageBoxContainer.TopAnchor, constant: 10).Active = true;
            messageTextView.BottomAnchor.ConstraintEqualTo(messageBoxContainer.BottomAnchor, constant: -10).Active = true;

            chatMessagePlaceholderLabel.LeadingAnchor.ConstraintEqualTo(messageBoxContainer.LeadingAnchor, constant: 20).Active = true;
            chatMessagePlaceholderLabel.TrailingAnchor.ConstraintEqualTo(sendImageView.LeadingAnchor, constant: -20).Active = true;
            chatMessagePlaceholderLabel.CenterYAnchor.ConstraintEqualTo(messageBoxContainer.CenterYAnchor).Active = true;

            sendImageView.TrailingAnchor.ConstraintEqualTo(messageBoxContainer.TrailingAnchor, constant: -20).Active = true;
            sendImageView.BottomAnchor.ConstraintEqualTo(messageBoxContainer.BottomAnchor, constant: -10).Active = true;
            sendImageView.WidthAnchor.ConstraintEqualTo(40).Active = true;
            sendImageView.HeightAnchor.ConstraintEqualTo(40).Active = true;

            if (ViewModel.ChatMessages != null)
            {
                var chatSource = new ChatDataSource(ViewModel.ChatMessages);
                chatTableView.Source = chatSource;

                //var set = this.CreateBindingSet<ChatViewController, MessengerPersonalChatViewModel>();
                //set.Bind(chatTableView.Source).To(vm => vm.ChatMessages);
                //set.Apply();

                chatTableView.TableFooterView = new UIView(CGRect.Empty);
                chatTableView.DelaysContentTouches = false;
                chatTableView.ReloadData();
            }

            await ChatHubMessagingService.SubscribeChatEvent();
            ChatHubMessagingService.PrivateMessageList.CollectionChanged += PrivateMessageList_CollectionChanged;
            ChatHubMessagingService.GroupMessageList.CollectionChanged += GroupMessageList_CollectionChanged;
        }

        private async  void GroupMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("GroupMessageList_CollectionChanged called");
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
                    chatMessageDetail.SenderFirstName = newChatItem.firstName;
                    chatMessageDetail.SenderLastName = newChatItem.lastName;

                    chatMessageDetail.chatMessageId = newChatItem.chatMessageRecipient.chatMessageId;

                    if (!ViewModel.ChatMessages.Any(x => x.chatMessageId == chatMessageDetail.chatMessageId))
                    {
                        ViewModel.ChatMessages.Add(chatMessageDetail);
                    }

                }

                
            }
            chatTableView.ReloadData();
        }

        private async void PrivateMessageList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("PrivateMessageList_CollectionChanged called");
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
                    chatMessageDetail.SenderFirstName = newChatItem.firstName;
                    chatMessageDetail.SenderLastName = newChatItem.lastName;

                    chatMessageDetail.chatMessageId = newChatItem.chatMessageRecipient.chatMessageId;

                    if (!ViewModel.ChatMessages.Any(x => x.chatMessageId == chatMessageDetail.chatMessageId))
                    {
                        ViewModel.ChatMessages.Add(chatMessageDetail);
                    }

                }

                chatTableView.ReloadData();
            }
        }
        void SetupNavigationItem()
        {
            if(ViewModel.ChatMessages != null)
            {
                if(ViewModel.ChatMessages[0].RecipientFirstName != null)
                {
                    Title = ViewModel.ChatMessages[0].RecipientFirstName +
                    ViewModel.ChatMessages[0].RecipientLastName;
                }
                else
                {
                    Title = "Group Chat";
                }               
            }
            else if(MessengerTempData.RecipientName != null)
            {
                Title = MessengerTempData.RecipientName;
            }            
        }

        void RegisterCell()
        {
            chatTableView.RegisterClassForCellReuse(typeof(MessageIncomingCell), MessageIncomingCell.Key);
            chatTableView.RegisterClassForCellReuse(typeof(MessageOutgoingCell), MessageOutgoingCell.Key);
        }

        void RegisterKeyboardObserver()
        {
            UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }       

        private async void getChatData()
        {
            ChatDataRequest chatData = new ChatDataRequest
            {
                SenderId = MessengerTempData.IsGroup ? 0 : EmployeeTempData.EmployeeID,
                RecipientId = MessengerTempData.RecipientID,
                GroupId = MessengerTempData.GroupID
            };
            await ViewModel.GetAllMessages(chatData);

            SetupView();
            SetupNavigationItem();
            RegisterCell();
            ScrollToBottom();
        }
       
        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeText(UITextView textView, NSRange range, string text)
        {
            var oldNSString = new NSString(textView.Text ?? "");
            var replacedString = oldNSString.Replace(range, new NSString(text));

            chatMessagePlaceholderLabel.Hidden = !string.IsNullOrEmpty(replacedString);
            return true;
        }        
        
        public async void OnSend()
        {
            
            if (messageTextView.Text != null)
            {
                var data = new ChatMessageDetail()
                {
                    MessageBody = messageTextView.Text,
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
                    ViewModel.ChatMessages.Add(data);
                }
                else
                {
                    ViewModel.ChatMessages.Add(data);

                }

                ViewModel.Message = messageTextView.Text;
                
                await ViewModel.SendMessage();
                if (ViewModel.SentSuccess)
                {
                    messageTextView.Text = "";
                    getChatData();
                    //chatTableView.ReloadData();
                }
            }
            else
            {
                ViewModel.EmptyChatMessageError();
            }
            
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);

        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs e)
        {
            var keyboardMinY = e.FrameEnd.GetMinY();
            var messageTextViewFrame = messageTextView.ConvertRectToView(messageTextView.Frame, null);
            if (messageTextView.ScrollEnabled)
            {
                messageTextViewFrame.Y += messageTextView.ContentOffset.Y;
            }

            if (keyboardMinY < messageTextViewFrame.GetMaxY())
            {
                var movingDistance = keyboardMinY - messageTextViewFrame.GetMaxY() + View.Frame.GetMinY() - 20;

                if (movingDistance > keyboardMinY)
                    movingDistance = -keyboardMinY;

                messageBoxContainerBottomConstraint.Constant += movingDistance;

                UIView.AnimateAsync(0.5, () => View.LayoutIfNeeded());
                ScrollToBottom();
            }
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs e)
        {
            messageBoxContainerBottomConstraint.Constant = -10;
            UIView.AnimateAsync(0.5, () => View.LayoutIfNeeded());
        }

        void ScrollToBottom()
        {
            var rowCount = chatTableView.NumberOfRowsInSection(0);
            if (rowCount > 0)
            {
                //chatTableView.ScrollToRow(NSIndexPath.FromItemSection(rowCount - 1, 0), UITableViewScrollPosition.Bottom, true);
                chatTableView.SetContentOffset(new CGPoint(0, chatTableView.ContentSize.Height - chatTableView.Frame.Size.Height), false);
            }
        }
    }
}


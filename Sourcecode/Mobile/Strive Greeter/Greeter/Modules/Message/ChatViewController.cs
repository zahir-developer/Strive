using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class ChatViewController : BaseViewController, IUITableViewDataSource, IUITableViewDelegate, IUITextViewDelegate
    {
        UITableView chatTableView;
        UIView messageBoxContainer;
        UITextView messageTextView;
        UILabel chatMessagePlaceholderLabel;

        NSLayoutConstraint messageBoxContainerBottomConstraint;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();
            RegisterKeyboardObserver();

            //Setup Delegate and DataSource
            chatTableView.WeakDelegate = this;
            chatTableView.WeakDataSource = this;

            //Reload once more to sync with original data
            ReloadChatTableView();
        }

        void SetupView()
        {
            View.BackgroundColor = UIColor.White;

            chatTableView = new UITableView(CGRect.Empty);
            chatTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            chatTableView.EstimatedRowHeight = 60;
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
            sendImageView.Image = UIImage.FromBundle(ImageNames.SEND);
            sendImageView.UserInteractionEnabled = true;
            sendImageView.AddGestureRecognizer(new UITapGestureRecognizer(SendTapped));
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
        }

        void SendTapped()
        {
            OnSendMsg(messageTextView.Text, chatInfo);
        }

        void SetupNavigationItem()
        {
            if (chatType == ChatType.Group)
            {
                Title = "Group Chat";
                NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle(ImageNames.PARTICIPANTS), UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
                {
                    NavigationController.PushViewController(new GroupParticipantsViewController(false, chatInfo?.GroupId ?? -1), true);
                });
            }
            else
            {
                Title = "Chat";
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

        void ReloadChatTableView(NSIndexPath[] indexPaths = null)
        {
            if (!IsViewLoaded) return;

            if (indexPaths == null)
                chatTableView.ReloadData();
            else
                chatTableView.ReloadRows(indexPaths, UITableViewRowAnimation.Fade);
        }

        void InsertRowAtChatTableView(NSIndexPath[] indexPaths = null)
        {
            if (!IsViewLoaded && indexPaths == null) return;
            chatTableView.InsertRows(indexPaths, UITableViewRowAnimation.Fade);
            ScrollToBottom();
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return Chats.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var message = Chats[indexPath.Row];
            if (message.SenderID != AppSettings.UserID)
            {
                var incomingCell = tableView.DequeueReusableCell(MessageIncomingCell.Key) as MessageIncomingCell;
                incomingCell.SetupData(message);
                return incomingCell;
            }

            var outgoingCell = tableView.DequeueReusableCell(MessageOutgoingCell.Key) as MessageOutgoingCell;
            outgoingCell.SetupData(message);
            return outgoingCell;
        }

        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeText(UITextView textView, NSRange range, string text)
        {
            var oldNSString = new NSString(textView.Text ?? "");
            var replacedString = oldNSString.Replace(range, new NSString(text));

            chatMessagePlaceholderLabel.Hidden = !string.IsNullOrEmpty(replacedString);
            return true;
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
                chatTableView.ScrollToRow(NSIndexPath.FromItemSection(rowCount - 1, 0), UITableViewScrollPosition.Bottom, true);
            }
        }
    }
}

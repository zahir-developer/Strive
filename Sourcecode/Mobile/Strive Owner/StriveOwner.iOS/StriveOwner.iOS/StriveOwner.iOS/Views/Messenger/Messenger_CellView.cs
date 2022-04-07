using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using Strive.Core.Models.Employee.Messenger;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger
{
    public partial class Messenger_CellView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("Messenger_CellView");
        public static readonly UINib Nib;

        private char[] firstInitial;
        private char[] secondInitial;
        UILabel userIntialLabel;
        UILabel userNameLabel;
        UILabel dateTimeLabel;
        UILabel messageContentLabel;

        static Messenger_CellView()
        {
            Nib = UINib.FromName("Messenger_CellView", NSBundle.MainBundle);
        }

        protected Messenger_CellView(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            userIntialLabel = new UILabel(CGRect.Empty);
            userIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            userIntialLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            userIntialLabel.TextColor = UIColor.Black;
            userIntialLabel.TextAlignment = UITextAlignment.Center;
            userIntialLabel.Layer.CornerRadius = 25;
            userIntialLabel.Layer.MasksToBounds = true;
            userIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(userIntialLabel);

            userNameLabel = new UILabel(CGRect.Empty);
            userNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            userNameLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            userNameLabel.TextColor = UIColor.Black;
            ContentView.Add(userNameLabel);

            dateTimeLabel = new UILabel(CGRect.Empty);
            dateTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            dateTimeLabel.Font = UIFont.SystemFontOfSize(14);
            dateTimeLabel.TextColor = UIColor.Gray;
            ContentView.Add(dateTimeLabel);

            messageContentLabel = new UILabel(CGRect.Empty);
            messageContentLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            messageContentLabel.Font = UIFont.SystemFontOfSize(15);
            messageContentLabel.TextColor = UIColor.Gray;
            ContentView.Add(messageContentLabel);

            userIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            userIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            userIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;
            userIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            userNameLabel.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 20).Active = true;
            userNameLabel.TrailingAnchor.ConstraintEqualTo(dateTimeLabel.LeadingAnchor, constant: -10).Active = true;
            userNameLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 15).Active = true;
            userNameLabel.WidthAnchor.ConstraintEqualTo(160).Active = true;

            dateTimeLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: 12).Active = true;
            dateTimeLabel.LeadingAnchor.ConstraintEqualTo(userNameLabel.TrailingAnchor, constant: 5).Active = true;
            dateTimeLabel.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, constant: 15).Active = true;

            messageContentLabel.LeadingAnchor.ConstraintEqualTo(userIntialLabel.TrailingAnchor, constant: 20).Active = true;
            messageContentLabel.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            messageContentLabel.TopAnchor.ConstraintEqualTo(userNameLabel.BottomAnchor, constant: 3).Active = true;
        }

        public void SetupData(ChatEmployeeList recentChat)
        {
            userIntialLabel.Text = "WH";
            userNameLabel.Text = "William Hoeger";
            dateTimeLabel.Text = "    ";
            messageContentLabel.Text = "                 ";//"Checkout cashier section";

            if (!String.IsNullOrEmpty(recentChat.FirstName))
            {
                firstInitial = recentChat.FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(recentChat.LastName))
            {
                secondInitial = recentChat.LastName.ToCharArray();
            }
            if (secondInitial == null)
            {
                if (firstInitial.Length != 0)
                {
                    userIntialLabel.Text = firstInitial.ElementAt(0).ToString() + firstInitial.ElementAt(1).ToString();
                    userNameLabel.Text = recentChat.FirstName + " " + recentChat.LastName;
                }
            }
            else if (firstInitial == null)
            {
                if (secondInitial.Length != 0)
                {
                    userIntialLabel.Text = secondInitial.ElementAt(0).ToString() + secondInitial.ElementAt(1).ToString();
                    userNameLabel.Text = recentChat.FirstName + " " + recentChat.LastName;
                }
            }
            else
            {
                userIntialLabel.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                userNameLabel.Text = recentChat.FirstName + " " + recentChat.LastName;
            }
            if (!String.IsNullOrEmpty(recentChat.RecentChatMessage))
            {
                DateTime UTCFormat = DateTime.Parse(recentChat.CreatedDate);
                DateTime TimeKindFormat = DateTime.SpecifyKind(UTCFormat, DateTimeKind.Utc);
                DateTime localFormat = TimeKindFormat.ToLocalTime();
                var lastMessage = localFormat.ToString().Split(" ");
                if (String.Equals(DateTime.Now.Date.ToString(), localFormat.Date.ToString()))
                {
                    var messageTime = lastMessage[1].Split(":");

                    var TimeofDay = int.Parse(messageTime[0]);

                    if (TimeofDay >= 12)
                    {
                        dateTimeLabel.Text = messageTime[0] + ":" + messageTime[1] + " " + "PM";
                    }
                    else
                    {
                        dateTimeLabel.Text = messageTime[0] + ":" + messageTime[1] + " " + "AM";
                    }
                }
                else
                {
                    dateTimeLabel.Text = lastMessage[0];
                }
                messageContentLabel.Text = recentChat.RecentChatMessage;
            }
        }
    }
}

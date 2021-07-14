using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger
{
    public enum ContactCellConfigureType
    {
        CreateGroup,
        Participant,
        ContactList
    }

    public interface IContactCellDelegate
    {
        void RemoveParticipant(object item);
    }

    public partial class Contact_CellView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("Contact_CellView");
        public static readonly UINib Nib;

        UILabel contactIntialLabel;
        UILabel contactNameLabel;
        UIImageView selectionImageView;
        private char[] firstInitial;
        private char[] secondInitial;

        public WeakReference<IContactCellDelegate> Delegate;

        static Contact_CellView()
        {
            Nib = UINib.FromName("Contact_CellView", NSBundle.MainBundle);
        }

        protected Contact_CellView(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            contactIntialLabel = new UILabel(CGRect.Empty);
            contactIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactIntialLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            contactIntialLabel.TextColor = UIColor.Black;
            contactIntialLabel.TextAlignment = UITextAlignment.Center;
            contactIntialLabel.Layer.CornerRadius = 25;
            contactIntialLabel.Layer.MasksToBounds = true;
            contactIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(contactIntialLabel);

            contactNameLabel = new UILabel(CGRect.Empty);
            contactNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactNameLabel.Font = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
            contactNameLabel.TextColor = UIColor.Black;
            ContentView.Add(contactNameLabel);

            selectionImageView = new UIImageView(CGRect.Empty);
            selectionImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            selectionImageView.AddGestureRecognizer(new UITapGestureRecognizer(OnRemoveSelected));
            ContentView.Add(selectionImageView);

            contactIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 30).Active = true;
            contactIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            contactIntialLabel.WidthAnchor.ConstraintEqualTo(50).Active = true;
            contactIntialLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            contactNameLabel.LeadingAnchor.ConstraintEqualTo(contactIntialLabel.TrailingAnchor, constant: 20).Active = true;
            contactNameLabel.TrailingAnchor.ConstraintEqualTo(selectionImageView.LeadingAnchor, constant: -10).Active = true;
            contactNameLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;

            selectionImageView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -30).Active = true;
            selectionImageView.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            selectionImageView.WidthAnchor.ConstraintEqualTo(25).Active = true;
            selectionImageView.HeightAnchor.ConstraintEqualTo(25).Active = true;
        }

        void OnRemoveSelected()
        {
            //TODO implement remove logic
            if (Delegate is null) return;

            if (Delegate.TryGetTarget(out IContactCellDelegate cellDelegate))
            {
                cellDelegate.RemoveParticipant(new { });
            }
        }

        public void SetupData(ContactCellConfigureType configureType)
        {
            contactIntialLabel.Text = "WH";
            contactNameLabel.Text = "William Hoeger";

            //if (configureType == ContactCellConfigureType.CreateGroup)
            //{
            //    //TODO check selection condition and change background if is selected
            //    ContentView.BackgroundColor = UIColor.FromRGB(225.0f / 255.0f, 255.0f / 255.0f, 251.0f / 255.0f);
            //    selectionImageView.Image = UIImage.FromBundle(ImageNames.TICK);
            //    selectionImageView.UserInteractionEnabled = false;
            //}
            //else if (configureType == ContactCellConfigureType.Participant)
            //{
            //    selectionImageView.Image = UIImage.FromBundle(ImageNames.CLOSE_SOLID);
            //    selectionImageView.UserInteractionEnabled = true;
            //}

            selectionImageView.Hidden = configureType == ContactCellConfigureType.ContactList;
        }

        public void SetData(NSIndexPath indexPath,Employee employee)
        {
            if (!String.IsNullOrEmpty(employee.FirstName))
            {
                firstInitial = employee.FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(employee.LastName))
            {
                secondInitial = employee.LastName.ToCharArray();
            }
            if (firstInitial != null && secondInitial != null)
            {
                contactIntialLabel.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                contactNameLabel.Text = employee.FirstName + " " + employee.LastName;
            }
            else if (firstInitial != null)
            {
                if (firstInitial.Length > 0)
                {
                    contactIntialLabel.Text = firstInitial.ElementAt(0).ToString() + firstInitial.ElementAt(1).ToString();
                }
            }
            else
            {
                if (secondInitial.Length > 0)
                {
                    contactIntialLabel.Text = secondInitial.ElementAt(0).ToString() + secondInitial.ElementAt(1).ToString();
                }
            }
        }
    }
}

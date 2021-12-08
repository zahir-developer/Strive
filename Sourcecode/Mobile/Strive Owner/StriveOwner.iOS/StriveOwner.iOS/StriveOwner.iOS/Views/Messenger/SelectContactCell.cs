using System;
using System.Collections.Generic;
using System.Linq;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using UIKit;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.ViewModels.Employee;
namespace StriveEmployee.iOS.Views.Messenger
{
    public interface IGroupCellDelegate
    {
        void RemoveParticipant(object item);
    }

    public partial class SelectContactCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("SelectContactCell");
        public static readonly UINib Nib;

        UILabel contactIntialLabel;
        UILabel contactNameLabel;
        UIButton selectionImageView;
        private char[] firstInitial;
        private char[] secondInitial;

        public WeakReference<IGroupCellDelegate> Delegate;

        static SelectContactCell()
        {
            Nib = UINib.FromName("SelectContactCell", NSBundle.MainBundle);
        }

        protected SelectContactCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            SetupView();
        }

        void SetupView()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            contactIntialLabel = new UILabel(CGRect.Empty);
            contactIntialLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactIntialLabel.Font = UIFont.SystemFontOfSize(16, UIFontWeight.Semibold);
            contactIntialLabel.TextColor = UIColor.Black;
            contactIntialLabel.TextAlignment = UITextAlignment.Center;
            contactIntialLabel.Layer.CornerRadius = 20;
            contactIntialLabel.Layer.MasksToBounds = true;
            contactIntialLabel.BackgroundColor = UIColor.FromRGB(162.0f / 255.0f, 229.0f / 255.0f, 221.0f / 255.0f);
            ContentView.Add(contactIntialLabel);

            contactNameLabel = new UILabel(CGRect.Empty);
            contactNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            contactNameLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            contactNameLabel.TextColor = UIColor.Black;
            ContentView.Add(contactNameLabel);

            selectionImageView = new UIButton(CGRect.Empty);
            selectionImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            selectionImageView.AddGestureRecognizer(new UITapGestureRecognizer());
            ContentView.Add(selectionImageView);

            contactIntialLabel.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, constant: 15).Active = true;
            contactIntialLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            contactIntialLabel.WidthAnchor.ConstraintEqualTo(40).Active = true;
            contactIntialLabel.HeightAnchor.ConstraintEqualTo(40).Active = true;

            contactNameLabel.LeadingAnchor.ConstraintEqualTo(contactIntialLabel.TrailingAnchor, constant: 20).Active = true;
            //contactNameLabel.TrailingAnchor.ConstraintEqualTo(selectionImageView.LeadingAnchor, constant: 15).Active = true;
            contactNameLabel.WidthAnchor.ConstraintEqualTo(150).Active = true;
            contactNameLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;

            selectionImageView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, constant: -15).Active = true;
            //selectionImageView.LeadingAnchor.ConstraintEqualTo(contactNameLabel.TrailingAnchor, constant: 15).Active = true;
            selectionImageView.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            selectionImageView.WidthAnchor.ConstraintEqualTo(25).Active = true;
            selectionImageView.HeightAnchor.ConstraintEqualTo(25).Active = true;
            selectionImageView.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);

            //selectionImageView.Image = UIImage.FromBundle("select-Contact");
            //selectionImageView.Layer.Opacity = 0;

        }

        //void OnRemoveSelected()
        //{
        //    //TODO implement remove logic
        //    if (Delegate is null) return;

        //    if (Delegate.TryGetTarget(out IGroupCellDelegate cellDelegate))
        //    {
        //        cellDelegate.RemoveParticipant(new { });
        //    }
        //}

        public void SetData(Employee employee, List<chatUserGroup> Rowselections, NSIndexPath indexpath)
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
            if (Rowselections.Any(x => x.userId == employee.EmployeeId))
            {
                selectionImageView.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
            else
            {
                selectionImageView.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }



        }

        public void updateCell(NSIndexPath indexPath)
        {
            selectionImageView.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
        }

        public void deselectRow(NSIndexPath indexPath)
        {
            selectionImageView.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
        }

    }
}

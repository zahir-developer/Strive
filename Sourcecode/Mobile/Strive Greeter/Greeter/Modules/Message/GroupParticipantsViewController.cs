﻿using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class GroupParticipantsViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IContactCellDelegate
    {
        UITableView participantTableView;
        UIView headerContainerView;
        UITextField groupNameTextField;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();

            //Setup Delegate and DataSource
            participantTableView.WeakDelegate = this;
            participantTableView.WeakDataSource = this;
        }

        void SetupView()
        {
            View.BackgroundColor = UIColor.White;

            if (isCreateGroup)
            {
                headerContainerView = new UIView(CGRect.Empty);
                headerContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
                headerContainerView.BackgroundColor = UIColor.FromRGB(225.0f / 255.0f, 255.0f / 255.0f, 251.0f / 255.0f);
                View.Add(headerContainerView);

                var titleLabel = new UILabel(CGRect.Empty);
                titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
                titleLabel.Font = UIFont.BoldSystemFontOfSize(16);
                titleLabel.Text = "Type Group Name";
                titleLabel.TextColor = UIColor.Black;
                headerContainerView.Add(titleLabel);

                groupNameTextField = new UITextField(CGRect.Empty);
                groupNameTextField.TranslatesAutoresizingMaskIntoConstraints = false;
                groupNameTextField.BorderStyle = UITextBorderStyle.Line;
                groupNameTextField.BackgroundColor = UIColor.White;
                headerContainerView.Add(groupNameTextField);

                headerContainerView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
                headerContainerView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
                headerContainerView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;

                titleLabel.LeadingAnchor.ConstraintEqualTo(headerContainerView.LeadingAnchor, constant: 30).Active = true;
                titleLabel.TrailingAnchor.ConstraintEqualTo(headerContainerView.TrailingAnchor, constant: -30).Active = true;
                titleLabel.TopAnchor.ConstraintEqualTo(headerContainerView.SafeAreaLayoutGuide.TopAnchor, constant: 20).Active = true;

                groupNameTextField.LeadingAnchor.ConstraintEqualTo(headerContainerView.LeadingAnchor, constant: 30).Active = true;
                groupNameTextField.TrailingAnchor.ConstraintEqualTo(headerContainerView.TrailingAnchor, constant: -30).Active = true;
                groupNameTextField.TopAnchor.ConstraintEqualTo(titleLabel.BottomAnchor, constant: 10).Active = true;
                groupNameTextField.BottomAnchor.ConstraintEqualTo(headerContainerView.BottomAnchor, constant: -30).Active = true;
                groupNameTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;
            }

            var participantTitleLabel = new UILabel(CGRect.Empty);
            participantTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            participantTitleLabel.Text = "Participants";
            participantTitleLabel.TextColor = UIColor.Black;
            participantTitleLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            View.Add(participantTitleLabel);

            var addParticipantImageView = new UIImageView(CGRect.Empty);
            addParticipantImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            addParticipantImageView.Image = UIImage.FromBundle(ImageNames.ADD_CIRCLE);
            addParticipantImageView.UserInteractionEnabled = true;
            addParticipantImageView.AddGestureRecognizer(new UITapGestureRecognizer(NavigateToContact));
            View.Add(addParticipantImageView);

            var horizontalDividerView = new UIView(CGRect.Empty);
            horizontalDividerView.TranslatesAutoresizingMaskIntoConstraints = false;
            horizontalDividerView.BackgroundColor = UIColor.LightGray;
            View.Add(horizontalDividerView);

            participantTableView = new UITableView(CGRect.Empty);
            participantTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            participantTableView.RowHeight = 70;
            participantTableView.SeparatorInsetReference = UITableViewSeparatorInsetReference.CellEdges;
            participantTableView.TableFooterView = new UIView();
            participantTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            View.Add(participantTableView);

            participantTitleLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 30).Active = true;
            participantTitleLabel.HeightAnchor.ConstraintEqualTo(25).Active = true;

            if(isCreateGroup)
            {
                participantTitleLabel.TopAnchor.ConstraintEqualTo(headerContainerView.BottomAnchor, constant: 25).Active = true;
            }
            else
            {
                participantTitleLabel.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 25).Active = true;
            }

            addParticipantImageView.LeadingAnchor.ConstraintEqualTo(participantTitleLabel.TrailingAnchor, constant: 30).Active = true;
            addParticipantImageView.TrailingAnchor.ConstraintLessThanOrEqualTo(View.TrailingAnchor, constant: -30).Active = true;
            addParticipantImageView.CenterYAnchor.ConstraintEqualTo(participantTitleLabel.CenterYAnchor).Active = true;
            addParticipantImageView.WidthAnchor.ConstraintEqualTo(25).Active = true;
            addParticipantImageView.HeightAnchor.ConstraintEqualTo(25).Active = true;

            horizontalDividerView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            horizontalDividerView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            horizontalDividerView.TopAnchor.ConstraintEqualTo(participantTitleLabel.BottomAnchor, constant: 16).Active = true;
            horizontalDividerView.HeightAnchor.ConstraintEqualTo(1).Active = true;

            participantTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            participantTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            participantTableView.TopAnchor.ConstraintEqualTo(horizontalDividerView.BottomAnchor, constant: 10).Active = true;
            participantTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            if (isCreateGroup)
            {
                Title = "Create Group";
            }
            else
            {
                //TODO show group name here
                Title = "New York Branch I";
            }

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => SaveParticipant());
        }

        void RegisterCell()
        {
            participantTableView.RegisterClassForCellReuse(typeof(ContactCell), ContactCell.Key);
        }

        void ReloadParticipantTableView()
        {
            participantTableView.ReloadData();
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return participants.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ContactCell.Key) as ContactCell;
            cell.SetupData(isCreateGroup ? ContactConfigureType.CreateGroup : ContactConfigureType.Participant, participants[indexPath.Row]);
            cell.Delegate = new WeakReference<IContactCellDelegate>(this);
            return cell;
        }

        void NavigateToContact()
        {
            NavigationController.PushViewController(new ContactViewController(ContactConfigureType.CreateGroup), true);
        }
    }
}
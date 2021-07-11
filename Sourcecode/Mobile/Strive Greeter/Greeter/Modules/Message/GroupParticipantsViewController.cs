using System;
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
            View.Add(participantTableView);

            participantTitleLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 30).Active = true;
            participantTitleLabel.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 25).Active = true;
            participantTitleLabel.HeightAnchor.ConstraintEqualTo(25).Active = true;

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
            Title = "New York Branch I";

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) => SaveParticipant());
        }

        void RegisterCell()
        {
            participantTableView.RegisterClassForCellReuse(typeof(ContactCell), ContactCell.Key);
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return participants.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ContactCell.Key) as ContactCell;
            cell.SetupData(ContactCellConfigureType.Participant);
            return cell;
        }

        void NavigateToContact()
        {
            NavigationController.PushViewController(new ContactViewController(), true);
        }
    }
}
using System;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using UIKit;

namespace Greeter.Modules.Message
{
    public partial class ContactViewController: UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUITextFieldDelegate
    {
        UITableView contactTableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            SetupNavigationItem();
            RegisterCell();

            //Setup Delegate and DataSource
            contactTableView.WeakDelegate = this;
            contactTableView.WeakDataSource = this;

            HidesBottomBarWhenPushed = true;
        }

        void SetupView()
        {
            View.BackgroundColor = UIColor.White;

            var searchContainerView = new UIView(CGRect.Empty);
            searchContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            searchContainerView.Layer.BorderColor = UIColor.LightGray.CGColor;
            searchContainerView.Layer.BorderWidth = 2;
            View.Add(searchContainerView);

            var searchTextField = new UITextField(CGRect.Empty);
            searchTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            searchTextField.Placeholder = "Search Employees";
            searchTextField.WeakDelegate = this;
            searchContainerView.Add(searchTextField);

            var searchImageView = new UIImageView(CGRect.Empty);
            searchImageView.TranslatesAutoresizingMaskIntoConstraints = false;
            searchImageView.Image = UIImage.FromBundle(ImageNames.SEARCH);
            searchContainerView.Add(searchImageView);

            contactTableView = new UITableView(CGRect.Empty);
            contactTableView.TranslatesAutoresizingMaskIntoConstraints = false;
            contactTableView.RowHeight = 70;
            contactTableView.SeparatorInsetReference = UITableViewSeparatorInsetReference.CellEdges;
            contactTableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive;
            contactTableView.TableFooterView = new UIView();
            View.Add(contactTableView);

            searchContainerView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, constant: 20).Active = true;
            searchContainerView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, constant: -20).Active = true;
            searchContainerView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, constant: 20).Active = true;
            searchContainerView.HeightAnchor.ConstraintEqualTo(50).Active = true;

            searchTextField.LeadingAnchor.ConstraintEqualTo(searchContainerView.LeadingAnchor, constant: 20).Active = true;
            searchTextField.TrailingAnchor.ConstraintEqualTo(searchImageView.LeadingAnchor, constant: -20).Active = true;
            searchTextField.TopAnchor.ConstraintEqualTo(searchContainerView.TopAnchor).Active = true;
            searchTextField.BottomAnchor.ConstraintEqualTo(searchContainerView.BottomAnchor).Active = true;

            searchImageView.TrailingAnchor.ConstraintEqualTo(searchContainerView.TrailingAnchor, constant: -20).Active = true;
            searchImageView.CenterYAnchor.ConstraintEqualTo(searchContainerView.CenterYAnchor).Active = true;
            searchImageView.WidthAnchor.ConstraintEqualTo(25).Active = true;
            searchImageView.HeightAnchor.ConstraintEqualTo(25).Active = true;

            contactTableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            contactTableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            contactTableView.TopAnchor.ConstraintEqualTo(searchContainerView.BottomAnchor, constant: 10).Active = true;
            contactTableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            Title = "Contacts";

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle(ImageNames.ADD_CIRCLE), UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {

            });
        }

        void RegisterCell()
        {
            contactTableView.RegisterClassForCellReuse(typeof(ContactCell), ContactCell.Key);
        }

        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeText(UITextView textView, NSRange range, string text)
        {
            var oldNSString = new NSString(textView.Text ?? "");
            var replacedString = oldNSString.Replace(range, new NSString(text));
            SearchContact(replacedString).ConfigureAwait(false);
            return true;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return contacts.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ContactCell.Key) as ContactCell;
            cell.SetupData(ContactCellConfigureType.ContactList);
            return cell;
        }

        //[Export("sectionIndexTitlesForTableView:")]
        //public string[] SectionIndexTitles(UITableView tableView)
        //{
        //    return new string[] { "A", "B" };
        //}
    }
}
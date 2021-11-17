using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using UIKit;

namespace Greeter.CustomView
{
    public interface IMultiSelectPickerDelegate
    {
        public void DidCompleted(MultiSelectPicker pickerView, List<int> selectedIndex);

        public void DidCancel(MultiSelectPicker pickerView);
    }

    public class MultiSelectPicker : UIViewController, IUITableViewDelegate, IUITableViewDataSource
    {
        UITableView tableView;

        public List<string> Options;
        public List<int> DefaultSelectedIndex;

        public IMultiSelectPickerDelegate PickerDelegate;
        readonly List<int> selectedIndex = new();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if(DefaultSelectedIndex is not null && DefaultSelectedIndex.Count > 0)
                selectedIndex.AddRange(DefaultSelectedIndex);

            SetupView();
            SetupNavigationItem();
        }

        void SetupView()
        {
            tableView = new UITableView(frame: CGRect.Empty);
            tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            tableView.WeakDataSource = this;
            tableView.WeakDelegate = this;
            View.Add(tableView);

            tableView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            tableView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            tableView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            tableView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        void SetupNavigationItem()
        {
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Cancel", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                DismissViewController(true, null);
                PickerDelegate?.DidCancel(this);
            });

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Done", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
            {
                DismissViewController(true, null);
                PickerDelegate?.DidCompleted(this, selectedIndex);
            });
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return Options?.Count ?? 0;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new UITableViewCell();
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.TextLabel.Text = Options[indexPath.Row];
            if (selectedIndex.Contains(indexPath.Row))
            {
                cell.AccessoryView = new UIImageView(UIImage.FromBundle(ImageNames.TICK));
            }
            else
            {
                cell.AccessoryView = null;
            }
            return cell;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (selectedIndex.Contains(indexPath.Row))
            {
                selectedIndex.Remove(indexPath.Row);
                tableView.ReloadRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
            }
            else
            {
                if (Options[0].Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    if (selectedIndex.Contains(0))
                    {
                        return;
                    }

                    if (indexPath.Row == 0)
                    {
                        selectedIndex.Clear();
                        selectedIndex.Add(indexPath.Row);
                        //tableView.ReloadData();
                        tableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(0, 0), indexPath }, UITableViewRowAnimation.Fade);
                    }
                    else
                    {
                        selectedIndex.Add(indexPath.Row);
                        tableView.ReloadRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                    }
                }
                else
                {
                    selectedIndex.Add(indexPath.Row);
                    tableView.ReloadRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                }
            }

            //if (indexPath.Contains(0))
            //{

            //}
        }
    }
}
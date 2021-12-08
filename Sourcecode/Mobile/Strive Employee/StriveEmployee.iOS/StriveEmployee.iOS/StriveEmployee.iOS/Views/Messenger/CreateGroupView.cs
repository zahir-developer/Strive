using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.iOS.UIUtils;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{

    public partial class CreateGroupView : MvxViewController<MessengerCreateGroupViewModel>
    {

        public CreateGroupView() : base("CreateGroupView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Create Group";

            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += async (sender, e) =>
            {
                await ViewModel.CreateGroupChat();

            };

            CreateGroup_ParentView.Layer.CornerRadius = 5;
            CreateGroup_TableView.Layer.CornerRadius = 5;

            CreateGroup_TableView.RegisterNibForCellReuse(SelectContactCell.Nib, SelectContactCell.Key);
            CreateGroup_TableView.BackgroundColor = UIColor.Clear;
            CreateGroup_TableView.ReloadData();

            ContactSearchBar.TextChanged += ContactSearchBar_TextChanged;

            getContacts();

            

        }

        private void ContactSearchBar_TextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.SearchText) && MessengerTempData.employeeList_Contact != null)
            {
                var searchText = e.SearchText.ToLower();
                var filteredList = MessengerTempData.employeeList_Contact.EmployeeList.Employee.Where(x => x.FirstName.ToLower().Contains(searchText)).ToList();

                var contactSource = new CreateGroupDataSource(filteredList);
                CreateGroup_TableView.Source = contactSource;
                CreateGroup_TableView.TableFooterView = new UIView(CGRect.Empty);
                CreateGroup_TableView.DelaysContentTouches = false;
                CreateGroup_TableView.ReloadData();
            }
            else
            {
                getContacts();
            }
        }

        async void getContacts()
        {
            await ViewModel.GetContactsList();

            if(ViewModel.EmployeeLists != null)
            {
                var contactSource = new CreateGroupDataSource(ViewModel.EmployeeLists.EmployeeList.Employee);
                CreateGroup_TableView.Source = contactSource;
                CreateGroup_TableView.TableFooterView = new UIView(CGRect.Empty);
                CreateGroup_TableView.DelaysContentTouches = false;
                CreateGroup_TableView.ReloadData();
            }                        
        }
    }

    public class CreateGroupDataSource : UITableViewSource
    {
        List<Employee> list;
        //public List<NSIndexPath> RowSelections = new List<NSIndexPath>();
        chatUserGroup chatUser = new chatUserGroup();
        public CreateGroupDataSource(List<Employee> contactList)
        {
            this.list = contactList;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("SelectContactCell", indexPath) as SelectContactCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(list[indexPath.Row],MessengerCreateGroupViewModel.chatUserGroups,indexPath);
            
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return list.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            SelectContactCell cell = (SelectContactCell)tableView.CellAt(indexPath);
            var tempgrplist = list[indexPath.Row];
            var temp = new chatUserGroup();
            temp.CommunicationId = list[indexPath.Row].CommunicationId;
            temp.createdBy = 0;
            temp.createdDate = (System.DateTime.Now).ToString("yyy/MM/dd HH:mm:ss").ToString();
            temp.isActive = true;
            temp.isDeleted = false;
            temp.userId = list[indexPath.Row].EmployeeId;
            temp.chatGroupUserId = 0;
            temp.chatGroupId = 0;
            if (MessengerCreateGroupViewModel.chatUserGroups.Any(x => x.userId == list[indexPath.Row].EmployeeId))
            {
                var itemToRemove = MessengerCreateGroupViewModel.chatUserGroups.FindIndex(r => r.userId == list[indexPath.Row].EmployeeId);
                MessengerCreateGroupViewModel.chatUserGroups.RemoveAt(itemToRemove);
                //var itemToRemove = RowSelections.FindIndex(r => r == indexPath);
                //RowSelections.RemoveAt(itemToRemove);
                cell.deselectRow(indexPath);
            }
            else
            {     
                if (MessengerCreateGroupViewModel.chatUserGroups!=null)
                {
                    if (MessengerCreateGroupViewModel.chatUserGroups.Contains(temp))
                    {
                        temp = new chatUserGroup();
                    }
                    else
                    {
                        MessengerCreateGroupViewModel.chatUserGroups.Add(temp);
                    }
                }        
                //RowSelections.Add(indexPath);
                cell.updateCell(indexPath);
            }
        }
        

        //public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        //{
        //    SelectContactCell cell = (SelectContactCell)tableView.CellAt(indexPath);
        //    cell.deselectRow(indexPath);

        //}
    }
}


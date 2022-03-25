using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;

namespace StriveOwner.iOS.Views.Messenger
{
    public partial class MessengerView : MvxViewController<MessengerViewModel>
    {
        public MessengerContactViewModel contactSView;
        public Msg_RecentChatViewModel recentViewModel;
        public MessengerGroupContactViewModel groupViewModel;
        public static string ConnectionID;
        public nint selectedIndex = 0;
        public Contact_DataSource contactSource;

        public MessengerView() : base("MessengerView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void DoInitialSetup()
        {
            contactSView = new MessengerContactViewModel();
            recentViewModel = new Msg_RecentChatViewModel();
            groupViewModel = new MessengerGroupContactViewModel();
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Messenger";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);

            Messenger_HomeView.Layer.CornerRadius = 5;
            Messenger_TableView.Layer.CornerRadius = 5;
            SearchBar_HeightConst.Constant = 0;

            Messenger_TableView.RegisterNibForCellReuse(Messenger_CellView.Nib, Messenger_CellView.Key);
            Messenger_TableView.BackgroundColor = UIColor.Clear;
            Messenger_TableView.ReloadData();

            Messenger_SearchBar.TextChanged += SearchTextchanged;

            if (ChatHubMessagingService.RecipientsID == null)
            {
                ChatHubMessagingService.RecipientsID = new ObservableCollection<RecipientsCommunicationID>();
                ChatHubMessagingService.RecipientsID.CollectionChanged += RecipientsID_CollectionChanged;
            }
            EstablishHubConnection();

            getRecentContacts();
            //setData();
        }

        partial void Messenger_SegmentTouch(UISegmentedControl sender)
        {
            var index = Messenger_SegCtrl.SelectedSegment;
            if(index == 0)
            {
                selectedIndex = index;
                SearchBar_HeightConst.Constant = 0;

                Messenger_TableView.RegisterNibForCellReuse(Messenger_CellView.Nib, Messenger_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                getRecentContacts();
            }
            else if(index == 1)
            {
                selectedIndex = index;
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Contact_CellView.Nib, Contact_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                setContactData();
            }
            else if(index == 2)
            {
                selectedIndex = index;
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Contact_CellView.Nib, Contact_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                getGroupChat();
            }
        }

        private void SearchTextchanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            if (selectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(e.SearchText) && MessengerTempData.employeeList_Contact != null)
                {
                    var searchText = e.SearchText.ToLower();
                    var filteredList = MessengerTempData.employeeList_Contact.EmployeeList.Employee.Where(x => x.FirstName.ToLower().Contains(searchText)).ToList();

                    contactSource = new Contact_DataSource(filteredList, contactSView);
                    Messenger_TableView.Source = contactSource;
                    Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
                    Messenger_TableView.DelaysContentTouches = false;
                    Messenger_TableView.ReloadData();
                }
                else
                {
                    setContactData();
                }
            }
            else if (selectedIndex == 2)
            {
                if (!string.IsNullOrEmpty(e.SearchText) && MessengerTempData.GroupLists != null)
                {
                    var searchText = e.SearchText.ToLower();
                    var filteredGroupList = MessengerTempData.GroupLists.ChatEmployeeList.Where(x => x.FirstName.ToLower().Contains(searchText)).ToList();

                    var groupSource = new MsgGroup_DataSource(filteredGroupList, groupViewModel);
                    Messenger_TableView.Source = groupSource;
                    Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
                    Messenger_TableView.DelaysContentTouches = false;
                    Messenger_TableView.ReloadData();
                }
                else
                {
                    setGroupData();
                }
            }
        }

        private async void RecipientsID_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (MessengerTempData.RecipientsConnectionID == null)
                {
                    MessengerTempData.RecipientsConnectionID = new Dictionary<string, string>();
                }
                foreach (var item in e.NewItems)
                {
                    var datas = (RecipientsCommunicationID)item;
                    if (MessengerTempData.RecipientsConnectionID.ContainsKey(datas.employeeId))
                    {
                        MessengerTempData.RecipientsConnectionID.Remove(datas.employeeId);
                        MessengerTempData.RecipientsConnectionID.Add(datas.employeeId, datas.communicationId);
                    }
                    else
                    {
                        MessengerTempData.RecipientsConnectionID.Add(datas.employeeId, datas.communicationId);
                    }
                }
            }
        }

        private async void EstablishHubConnection()
        {
            ConnectionID = await this.ViewModel.StartCommunication();

            await ChatHubMessagingService.SendEmployeeCommunicationId(EmployeeTempData.EmployeeID.ToString(), ConnectionID);

            MessengerTempData.ConnectionID = ConnectionID;

            await this.ViewModel.SetChatCommunicationDetails(MessengerTempData.ConnectionID);
            await ChatHubMessagingService.SubscribeChatEvent();

        }

        private async void getRecentContacts()
        {
            await recentViewModel.GetRecentContactsList();
            if (recentViewModel.EmployeeList != null)
            {
                if (recentViewModel.EmployeeList.ChatEmployeeList.Count > 0)
                {
                    setData();
                }
            }
        }

        void setData()
        {
            var messageSource = new Messenger_BaseDataSource(recentViewModel.EmployeeList.ChatEmployeeList, recentViewModel);
            Messenger_TableView.Source = messageSource;
            Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
            Messenger_TableView.DelaysContentTouches = false;
            Messenger_TableView.ReloadData();
        }

        async void setContactData()
        {
            if (MessengerTempData.EmployeeLists == null || MessengerTempData.ContactsCount < MessengerTempData.EmployeeLists.EmployeeList.Count)
            {
                var employeeLists = await contactSView.GetContactsList();

                if (MessengerTempData.employeeList_Contact != null || employeeLists != null || employeeLists.EmployeeList != null || employeeLists.EmployeeList.Employee != null)
                {
                    var contactSource = new Contact_DataSource(employeeLists.EmployeeList.Employee, contactSView);
                    Messenger_TableView.Source = contactSource;
                    Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
                    Messenger_TableView.DelaysContentTouches = false;
                    Messenger_TableView.ReloadData();
                }
            }           
        }

        public async void getGroupChat()
        {
            await groupViewModel.GetGroupsList();
            if (groupViewModel.GroupList != null)
            {
                if (groupViewModel.GroupList.ChatEmployeeList.Count > 0)
                {
                    setGroupData();
                }
            }
        }

        void setGroupData()
        {
            var groupSource = new MsgGroup_DataSource(groupViewModel.GroupList.ChatEmployeeList, groupViewModel);
            Messenger_TableView.Source = groupSource;
            Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
            Messenger_TableView.DelaysContentTouches = false;
            Messenger_TableView.ReloadData();
        }
        partial void MenuBtnTouch(UIButton sender)
        {
            ViewModel.navigateToCreateGroup();
        }

    }
}


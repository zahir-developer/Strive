using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveEmployee.iOS.UIUtils;
using UIKit;

namespace StriveEmployee.iOS.Views.Messenger
{
    public partial class MessengerView : MvxViewController<MessengerViewModel>
    {
        public MessengerContactViewModel contactSView;
        public MessengerRecentContactsViewModel recentViewModel;
        public static string ConnectionID;
        public nint index = 0;
        public Contact_DataSource contactSource;

        public MessengerView() : base("MessengerView", null)
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

        private void InitialSetup()
        {
            recentViewModel = new MessengerRecentContactsViewModel();
            contactSView = new MessengerContactViewModel();

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Messenger";

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
        }

        partial void Messenger_SegmentTouch(UISegmentedControl sender)
        {
            index = Messenger_SegCtrl.SelectedSegment;
            if (index == 0)
            {
                SearchBar_HeightConst.Constant = 0;

                Messenger_TableView.RegisterNibForCellReuse(Messenger_CellView.Nib, Messenger_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                getRecentContacts();
            }
            else if (index == 1)
            {
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Contact_CellView.Nib, Contact_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                setContactData();
            }
            else if (index == 2)
            {
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Contact_CellView.Nib, Contact_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                setGroupData();
            }
        }

        partial void MenuBtn_Touch(UIButton sender)
        {
            ViewModel.navigateToCreateGroup();
        }

        private void SearchTextchanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            if(index == 1)
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
            }
            else if(index == 2)
            {

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
            var messageSource = new MessengerRecents_DataSource(recentViewModel.EmployeeList.ChatEmployeeList, recentViewModel);
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
                    contactSource = new Contact_DataSource(employeeLists.EmployeeList.Employee, contactSView);
                    Messenger_TableView.Source = contactSource;
                    Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
                    Messenger_TableView.DelaysContentTouches = false;
                    Messenger_TableView.ReloadData();
                }
            }
        }

        void setGroupData()
        {
            var groupSource = new MsgGroup_DataSource();
            Messenger_TableView.Source = groupSource;
            Messenger_TableView.TableFooterView = new UIView(CGRect.Empty);
            Messenger_TableView.DelaysContentTouches = false;
            Messenger_TableView.ReloadData();
        }
    }
}


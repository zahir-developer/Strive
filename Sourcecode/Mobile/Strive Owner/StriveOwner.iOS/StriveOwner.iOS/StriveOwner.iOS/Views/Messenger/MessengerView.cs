using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static string ConnectionID;

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
                SearchBar_HeightConst.Constant = 0;

                Messenger_TableView.RegisterNibForCellReuse(Messenger_CellView.Nib, Messenger_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                setData();
            }
            else if(index == 1)
            {
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Contact_CellView.Nib, Contact_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();

                setContactData();
            }
            else if(index == 2)
            {
                SearchBar_HeightConst.Constant = 50;

                Messenger_TableView.RegisterNibForCellReuse(Messenger_CellView.Nib, Messenger_CellView.Key);
                Messenger_TableView.BackgroundColor = UIColor.Clear;
                Messenger_TableView.ReloadData();
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
            var messageSource = new Messenger_BaseDataSource(recentViewModel.EmployeeList.ChatEmployeeList);
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
    }
}


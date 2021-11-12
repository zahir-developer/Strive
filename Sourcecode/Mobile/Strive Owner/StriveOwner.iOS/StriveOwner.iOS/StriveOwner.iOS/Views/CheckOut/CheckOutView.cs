using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;

namespace StriveOwner.iOS.Views.CheckOut
{
    public partial class CheckOutView : MvxViewController<CheckOutViewModel>
    {
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;

        public CheckOutView() : base("CheckOutView", null)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            await RefreshAsync();

            AddRefreshControl();
            CheckOut_TableView.Add(RefreshControl);


            // Perform any additional setup after loading the view, typically from a nib.
        }
        public override void ViewDidAppear(bool animated)
        {
            InitialSetup();
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available
            if (useRefreshControl)
                RefreshControl.BeginRefreshing();

            if (useRefreshControl)
                RefreshControl.EndRefreshing();
            CheckOut_TableView.ReloadData();

        }
        void AddRefreshControl()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {

                // the refresh control is available, let's add it
                RefreshControl = new UIRefreshControl();
                RefreshControl.ValueChanged += async (sender, e) =>
                {
                    InitialSetup();
                    await RefreshAsync();
                };
                useRefreshControl = true;

            }
        }



        private void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "CheckOut";

            CheckOut_TableView.Layer.CornerRadius = 5;
            CheckOut_View.Layer.CornerRadius = 5;

            CheckOut_TableView.RegisterNibForCellReuse(CheckOut_Cell.Nib, CheckOut_Cell.Key);
            CheckOut_TableView.BackgroundColor = UIColor.Clear;
            CheckOut_TableView.ReloadData();

            GetCheckoutDetails();
        }

        private async void GetCheckoutDetails()
        {
            await ViewModel.GetCheckOutDetails();
            if (ViewModel.CheckOutVehicleDetails != null)
            {
                if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                    || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                {
                    var checkoutSource = new CheckOut_DataSource(ViewModel.CheckOutVehicleDetails, this);
                    CheckOut_TableView.Source = checkoutSource;
                    CheckOut_TableView.TableFooterView = new UIView(CGRect.Empty);
                    CheckOut_TableView.DelaysContentTouches = false;
                    CheckOut_TableView.ReloadData();
                }
            }
            else
            {
                CheckOut_TableView.Source = null;
                CheckOut_TableView.TableFooterView = new UIView(CGRect.Empty);
                CheckOut_TableView.DelaysContentTouches = false;
                CheckOut_TableView.ReloadData();
            }
        }

        public void HoldTicket(checkOutViewModel checkout)
        {
            ShowAlertMsg("Are you sure want to change the status to hold?", () =>
            {
                HoldCheckout(checkout);
            }, true, "Hold");
        }

        public async void HoldCheckout(checkOutViewModel checkout)
        {
            await ViewModel.updateHoldStatus(int.Parse(checkout.TicketNumber));

            if (ViewModel.holdResponse.UpdateJobStatus)
            {
                ShowAlertMsg("Service status changed to hold successfully", () =>
                {
                    // Refreshing checkout list
                    GetCheckoutDetails();
                }, titleTxt: "Hold");
            }

        }

        public void CompleteTicket(checkOutViewModel checkout)
        {
            ShowAlertMsg("Are you sure want to change the status to complete?", () =>
            {
                CompleteCheckout(checkout);
            }, true, "Complete");
        }

        public async void CompleteCheckout(checkOutViewModel checkout)
        {
            await ViewModel.updateCompleteStatus(int.Parse(checkout.TicketNumber));

            if (ViewModel.holdResponse.UpdateJobStatus)
            {
                ShowAlertMsg("Service has been completed successfully", () =>
                {
                    GetCheckoutDetails();
                }, titleTxt: "Complete");
            }
        }

        public void CheckoutTicket(checkOutViewModel checkout)
        {
            if (checkout.MembershipNameOrPaymentStatus.Contains("Paid"))
            {
                ShowAlertMsg("Are you sure want to change the status to checkout?", () =>
                {
                    Checkout(checkout);
                }, true, "Checkout");
            }
            else
            {
                ShowAlertMsg("Cann't Checkout without payment", () =>
                {

                }, true, "Checkout");
            }
        }

        public async void Checkout(checkOutViewModel checkout)
        {
            await ViewModel.DoCheckout(int.Parse(checkout.TicketNumber));

            if (ViewModel.status.SaveCheckoutTime)
            {
                ShowAlertMsg("Vehicle has been checked out successfully", () =>
                {
                    GetCheckoutDetails();
                }, titleTxt: "Checkout");
            }
        }

        public void ShowAlertMsg(string msg, Action okAction = null, bool isCancel = false, string titleTxt = null)
        {
            string title = "Alert";
            string ok = "Ok";
            string cancel = "Cancel";

            if (!string.IsNullOrEmpty(titleTxt))
            {
                title = titleTxt;
            }

            var okAlertController = UIAlertController.Create(title, msg, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(ok, UIAlertActionStyle.Default,
                alert =>
                {
                    okAction?.Invoke();
                }));
            if (isCancel)
                okAlertController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, null));
            PresentViewController(okAlertController, true, null);
        }
    }
}


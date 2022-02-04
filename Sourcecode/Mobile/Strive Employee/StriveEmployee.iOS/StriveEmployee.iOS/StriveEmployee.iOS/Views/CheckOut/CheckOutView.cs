using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.ViewModels.Employee.CheckOut;
using StriveEmployee.iOS.UIUtils;
using StriveEmployee.iOS.Views.CheckOut;
using UIKit;
using MvvmCross.Binding.BindingContext;
using Strive.Core.Utils.Employee;

namespace StriveEmployee.iOS.Views
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
            ViewModel.EmployeeLocations = EmployeeTempData.employeeLocationdata;
            InitialSetup();
           
            await RefreshAsync();

            AddRefreshControl();
            CheckOut_TableView.Add(RefreshControl);
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
           // GetCheckoutDetails();
        }
        
        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available
            if (useRefreshControl)
                RefreshControl.BeginRefreshing(); if (useRefreshControl)
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
                    RefreshSetup();
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
            
            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            CheckOut_TableView.Layer.CornerRadius = 5;
            CheckOut_View.Layer.CornerRadius = 5;

            CheckOut_TableView.RegisterNibForCellReuse(CheckOut_Cell.Nib, CheckOut_Cell.Key);
            CheckOut_TableView.BackgroundColor = UIColor.Clear;
            CheckOut_TableView.ReloadData();
            ViewModel.locationID = ViewModel.EmployeeLocations[0].LocationId;
            GetCheckoutDetails();

            var pickerView = new UIPickerView();
            var PickerViewModel = new LocationPicker(ViewModel, pickerView);
            pickerView.Model = PickerViewModel;
            pickerView.ShowSelectionIndicator = true;
            AddPickerToolbar(locationTextField, "Location", PickerDone);
            locationTextField.InputView = pickerView;
            //PickerDone();
            ViewModel.ItemLocation = ViewModel.EmployeeLocations[0].LocationName;
            ViewModel.Location = ViewModel.EmployeeLocations[0].LocationId;

            var set = this.CreateBindingSet<CheckOutView,CheckOutViewModel>();
            set.Bind(locationTextField).To(vm => vm.ItemLocation);
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }
        private void RefreshSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "CheckOut";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            CheckOut_TableView.Layer.CornerRadius = 5;
            CheckOut_View.Layer.CornerRadius = 5;

            CheckOut_TableView.RegisterNibForCellReuse(CheckOut_Cell.Nib, CheckOut_Cell.Key);
            CheckOut_TableView.BackgroundColor = UIColor.Clear;
            CheckOut_TableView.ReloadData();

            GetCheckoutDetails();

            var pickerView = new UIPickerView();
            var PickerViewModel = new LocationPicker(ViewModel, pickerView);
            pickerView.Model = PickerViewModel;
            pickerView.ShowSelectionIndicator = true;
            AddPickerToolbar(locationTextField, "Location", PickerDone);
            locationTextField.InputView = pickerView;
            //PickerDone();
            var set = this.CreateBindingSet<CheckOutView, CheckOutViewModel>();
            set.Bind(locationTextField).To(vm => vm.ItemLocation);
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        void PickerDone()
        {
            GetCheckoutDetails();
            CheckOut_TableView.ReloadData();
            //if (locationTextField.Text == "")
            //{
            //    locationTextField.Text = EmployeeTempData.LocationName;

            //}
            View.EndEditing(true);
        }

        private async void GetCheckoutDetails()
        {
            try
            {
                await ViewModel.GetCheckOutDetails();
                if (ViewModel.CheckOutVehicleDetails != null)
                {
                    if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                        || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                    {
                        var documentSource = new Checkout_DataSource(ViewModel.CheckOutVehicleDetails, this);
                        CheckOut_TableView.Source = documentSource;
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
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
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
            try
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
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
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
            try
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
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
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
            try
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
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
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

        public void AddPickerToolbar(UITextField textField, string title, Action action)
        {
            const string CANCEL_BUTTON_TXT = "Cancel";
            const string DONE_BUTTON_TXT = "Done";

            var toolbarDone = new UIToolbar();
            toolbarDone.SizeToFit();

            var barBtnCancel = new UIBarButtonItem(CANCEL_BUTTON_TXT, UIBarButtonItemStyle.Plain, (sender, s) =>
            {
                textField.EndEditing(false);
            });

            var barBtnDone = new UIBarButtonItem(DONE_BUTTON_TXT, UIBarButtonItemStyle.Done, (sender, s) =>
            {
                textField.EndEditing(false);
                action.Invoke();

            });

            var barBtnSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            var lbl = new UILabel();
            lbl.Text = title;
            lbl.TextAlignment = UITextAlignment.Center;
            lbl.Font = UIFont.BoldSystemFontOfSize(size: 16.0f);
            var lblBtn = new UIBarButtonItem(lbl);

            toolbarDone.Items = new UIBarButtonItem[] { barBtnCancel, barBtnSpace, lblBtn, barBtnSpace, barBtnDone };
            textField.InputAccessoryView = toolbarDone;
        }

    }
}


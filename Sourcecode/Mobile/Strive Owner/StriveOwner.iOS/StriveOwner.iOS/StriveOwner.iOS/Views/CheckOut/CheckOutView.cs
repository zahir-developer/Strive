﻿using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;
using MvvmCross.Binding.BindingContext;
using Strive.Core.Utils.Employee;

namespace StriveOwner.iOS.Views.CheckOut
{
    public partial class CheckOutView : MvxViewController<CheckOutViewModel>
    {
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;
        public CheckoutDetails CheckOutVehicle { get; set; }
        public CheckOutView() : base("CheckOutView", null)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.EmployeeLocations = EmployeeTempData.employeeLocationdata;
            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.NavigateBackCommand();
            };
            await RefreshAsync();
            InitialSetup();
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
                    //InitialSetup();
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

            var set = this.CreateBindingSet<CheckOutView, CheckOutViewModel>();
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
            if (checkout.IsHold == true)
            {
                ShowAlertMsg("Are you sure want to change the status to unhold?", () =>
            {
                HoldCheckout(checkout);
            }, true, "Unhold");
            }
            else
            {
                ShowAlertMsg("Are you sure want to change the status to hold?", () =>
                {
                    HoldCheckout(checkout);
                }, true, "Hold");
            }
        }

        public async void HoldCheckout(checkOutViewModel checkout)
        {
            await ViewModel.updateHoldStatus((int)checkout.JobId , checkout.IsHold);

            if (ViewModel.holdResponse.UpdateJobStatus)
            {
                if (checkout.IsHold == true)
                {
                    ShowAlertMsg("Service status changed to unhold successfully", () =>
                {
                    // Refreshing checkout list
                    GetCheckoutDetails();
                }, titleTxt: "Unhold");
                }
                else
                {
                    ShowAlertMsg("Service status changed to hold successfully", () =>
                    {
                        // Refreshing checkout list
                        GetCheckoutDetails();
                    }, titleTxt: "Hold");
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
            await ViewModel.updateCompleteStatus((int)checkout.JobId);

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
            if (checkout.MembershipNameOrPaymentStatus.Contains("Paid") && checkout.valuedesc == "Completed")
            {
                ShowAlertMsg("Are you sure want to change the status to checkout?", () =>
                {
                    Checkout(checkout);
                }, true, "Checkout");
            }
            else if (checkout.valuedesc != "Completed")
            {
                ShowAlertMsg("Can't Checkout without ticket completion", () =>
                {

                }, true, "Checkout");
            }
            else
            {
                ShowAlertMsg("Can't Checkout without payment", () =>
                {

                }, true, "Checkout");
            }
        }

        public async void Checkout(checkOutViewModel checkout)
        {
            await ViewModel.DoCheckout((int)checkout.JobId);

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


﻿using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using Strive.Core.Models.Employee.PayRoll;
using StriveEmployee.iOS.UIUtils;
using UIKit;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using MvvmCross.Binding.BindingContext;

namespace StriveEmployee.iOS.Views.PayRoll
{
    public partial class PayRollView :MvxViewController<PayRollViewModel>
    {
        
        public PayRollView() : base("PayRollView", null)
        {
        }
        DateTime fromdate;
        DateTime todate;
         
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.EmployeeLocations = EmployeeTempData.employeeLocationdata;
            InitialSetup();
            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            InitialSetup();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        private  async Task InitialSetup()
        {
            NavigationItem.Title = "Pay Roll";
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            //Initial Call
            FromDate.Date = (NSDate)DateTime.Now.AddDays(-15);
            FromDate.Mode = UIDatePickerMode.Date;
            ToDate.Mode = UIDatePickerMode.Date;
            FromDate.MaximumDate = NSDate.Now;
            ToDate.MaximumDate = NSDate.Now;
            ViewModel.Todate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            ViewModel.Fromdate = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            ViewModel.Location = EmployeeTempData.LocationId;
            ViewModel.employeeid = EmployeeTempData.EmployeeID;
            PayRoll.Layer.CornerRadius = 5;
            FindPayRoll.Layer.CornerRadius = 5;
            if (ViewModel.employeeid != 0)
            {
                await ViewModel.GetPayRollProcess();
                
            }
            
            //Setting Data
            EmployeeName.Text = ViewModel.PayRoll.PayeeName;
            WashHrs.Text = ViewModel.PayRoll.TotalWashHours;
            DetailHrs.Text = ViewModel.PayRoll.TotalDetailHours;
            WashRate.Text = "$"+ SetTwoDecimel(ViewModel.PayRoll.WashRate);
            RegPay.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashAmount);
            OtHrs.Text = ViewModel.PayRoll.OverTimeHours;
            OtPay.Text = "$"+ SetTwoDecimel(ViewModel.PayRoll.OverTimePay);
            Cols.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Collision);
            Adjs.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Adjustment);
            Commission.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailCommission);
            Cashtip.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CashTip);
            Cardtip.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CardTip);
            Washtip.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashTip);
            Detailtip.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailTip);
            Bonus.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Bonus);
            Total.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.PayeeTotal);
           
            FindPayRoll.TouchUpInside += (sender, e) =>
            {  
                GetDate();
            };

            //Spinner
            var pickerView = new UIPickerView();
            var PickerViewModel = new LocationPicker(ViewModel, pickerView);
            pickerView.Model = PickerViewModel;
            pickerView.ShowSelectionIndicator = true;
            AddPickerToolbar(locationTextField, "Location", PickerDone);
            locationTextField.InputView = pickerView;
             //PickerDone();
            ViewModel.ItemLocation = ViewModel.EmployeeLocations[0].LocationName;
            ViewModel.Location = ViewModel.EmployeeLocations[0].LocationId;
           
            var set = this.CreateBindingSet<PayRollView, PayRollViewModel>();
            set.Bind(locationTextField).To(vm => vm.ItemLocation);
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);

        }
        


        private string SetTwoDecimel(float value)
        {
            return string.Format("{0:0.00}", value);
        }
        
        public async Task GetDate()
        {
            fromdate = (DateTime)FromDate.Date;
            todate = (DateTime)ToDate.Date;
           // ViewModel.Location = 1;
            ViewModel.Fromdate = fromdate.Date.ToString("yyyy-MM-dd");
            ViewModel.Todate = todate.Date.ToString("yyyy-MM-dd");
            ViewModel.employeeid = EmployeeTempData.EmployeeID;
            if (ViewModel.employeeid != 0)
            {
                await ViewModel.GetPayRollProcess();
                EmployeeName.Text = ViewModel.PayRoll.PayeeName;
                WashHrs.Text = ViewModel.PayRoll.TotalWashHours;
                DetailHrs.Text = ViewModel.PayRoll.TotalDetailHours;
                WashRate.Text = "$" + ViewModel.PayRoll.WashRate.ToString();
                RegPay.Text = "$" + ViewModel.PayRoll.WashAmount.ToString();
                OtHrs.Text = ViewModel.PayRoll.OverTimeHours;
                OtPay.Text = "$" + ViewModel.PayRoll.OverTimePay.ToString();
                Cols.Text = "$" + ViewModel.PayRoll.Collision;
                Adjs.Text = "$" + ViewModel.PayRoll.Adjustment;
                Commission.Text = "$" + ViewModel.PayRoll.DetailCommission;
                Cashtip.Text = "$" + ViewModel.PayRoll.CashTip;
                Cardtip.Text = "$" + ViewModel.PayRoll.CardTip;
                Washtip.Text = "$" + ViewModel.PayRoll.WashTip;
                Detailtip.Text = "$" + ViewModel.PayRoll.DetailTip;
                Bonus.Text = "$" + ViewModel.PayRoll.Bonus;
                Total.Text = "$" + ViewModel.PayRoll.PayeeTotal;
            }
        
        }

        void PickerDone()
        {
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
    }    
}


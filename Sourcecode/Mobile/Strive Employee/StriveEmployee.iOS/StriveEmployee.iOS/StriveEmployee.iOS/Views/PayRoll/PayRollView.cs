using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using Strive.Core.Models.Employee.PayRoll;
using StriveEmployee.iOS.UIUtils;
using UIKit;
using System.Threading.Tasks;

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
            WashRate.Text = "$"+ViewModel.PayRoll.WashRate.ToString();
            RegPay.Text = "$" + ViewModel.PayRoll.WashAmount.ToString();
            OtHrs.Text = ViewModel.PayRoll.OverTimeHours;
            OtPay.Text = "$"+ViewModel.PayRoll.OverTimePay.ToString();
            Cols.Text = "$" + ViewModel.PayRoll.Collision;
            Adjs.Text = "$" + ViewModel.PayRoll.Adjustment;
            Commission.Text = "$" + ViewModel.PayRoll.DetailCommission;
            Cashtip.Text = "$" + ViewModel.PayRoll.CashTip;
            Cardtip.Text = "$" + ViewModel.PayRoll.CardTip;
            Washtip.Text = "$" + ViewModel.PayRoll.WashTip;
            Detailtip.Text = "$" + ViewModel.PayRoll.DetailTip;
            Bonus.Text = "$" + ViewModel.PayRoll.Bonous;
            Total.Text = "$" + ViewModel.PayRoll.PayeeTotal;
            Location.Text = EmployeeTempData.LocationName;
            FindPayRoll.TouchUpInside += (sender, e) =>
            {  
                GetDate();
            };
            
        }
        
        public async Task GetDate()
        {
            fromdate = (DateTime)FromDate.Date;
            todate = (DateTime)ToDate.Date;
            ViewModel.Location = 1;
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
                Bonus.Text = "$" + ViewModel.PayRoll.Bonous;
            }
        
        }
    }    
}


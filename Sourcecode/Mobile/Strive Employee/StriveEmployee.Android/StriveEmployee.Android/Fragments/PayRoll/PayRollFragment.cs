using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriveEmployee.Android.Fragments.Payroll
{
    public class PayRollFragment : MvxFragment<PayRollViewModel>
    {
        private TextView FromDate;
        private TextView ToDate;
        private TextView PayeeName;       
        private Spinner  LocationNameSpinner;
        private TextView WashHours;
        private TextView DetailHours;
        private TextView OTHours;
        private TextView Collision;
        private TextView CashTips;
        private TextView CardTips;
        private TextView DetailTips;
        private TextView WashRate;
        private TextView RegPay;
        private TextView OTPay;
        private TextView Adjustment;
        private TextView Commission;
        private TextView WashTips;
        private TextView Bonus;
        private TextView Total;
        private Button Go_Button;
        private List<string> Locations;        
        private ArrayAdapter<string> LocationAdapter;
        private int position { get; set; }   
        private string date { get; set; }
       

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView= this.BindingInflate(Resource.Layout.PayRoll_Fragment, null);
            this.ViewModel = new PayRollViewModel();
            ViewModel.EmployeeLocations= EmployeeTempData.employeeLocationdata;
            FromDate = rootView.FindViewById<TextView>(Resource.Id.fromDate);
            ToDate = rootView.FindViewById<TextView>(Resource.Id.toDate);
            PayeeName = rootView.FindViewById<TextView>(Resource.Id.Payee_Name);
            LocationNameSpinner = rootView.FindViewById<Spinner>(Resource.Id.LocationName);
            WashHours = rootView.FindViewById<TextView>(Resource.Id.WashHrs);
            DetailHours = rootView.FindViewById<TextView>(Resource.Id.DetailHrs);
            OTHours = rootView.FindViewById<TextView>(Resource.Id.OTHrs);
            Collision = rootView.FindViewById<TextView>(Resource.Id.Collision);
            CashTips = rootView.FindViewById<TextView>(Resource.Id.CashTips);
            CardTips = rootView.FindViewById<TextView>(Resource.Id.CardTips);
            DetailTips = rootView.FindViewById<TextView>(Resource.Id.DetailTips);
            WashRate = rootView.FindViewById<TextView>(Resource.Id.WashRate);
            RegPay = rootView.FindViewById<TextView>(Resource.Id.RegPay);
            OTPay = rootView.FindViewById<TextView>(Resource.Id.OTPay);
            Adjustment = rootView.FindViewById<TextView>(Resource.Id.Adjustment);
            Commission = rootView.FindViewById<TextView>(Resource.Id.Commission);
            WashTips = rootView.FindViewById<TextView>(Resource.Id.WashTips);
            Bonus = rootView.FindViewById<TextView>(Resource.Id.Bonus);
            Total = rootView.FindViewById<TextView>(Resource.Id.Total);
            Go_Button = rootView.FindViewById<Button>(Resource.Id.go_btn);
            Go_Button.Click += Go_Button_Click;
            FromDate.Click += FromDate_Click;
            ToDate.Click += ToDate_Click;
            LocationNameSpinner.ItemSelected += LocationSpinner_ItemSelected;
            InitialSetup();
            return rootView;
        }

        private void LocationSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.locationCommand(ViewModel.EmployeeLocations[e.Position]);          

        }

        private string SetTwoDecimel(float value)
        {
            return string.Format("{0:0.00}", value);
        }

        private async Task InitialSetup()
        {
            //Initial Call
            FromDate.Text = DateTime.Now.Date.AddDays(-15).ToString("yyyy-MM-dd");
            ToDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //FromDate.MaximumDate = DateTime.Now;
            //ToDate.MaximumDate = DateTime.Now;
            ViewModel.Todate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            ViewModel.Fromdate = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
            ViewModel.Location = EmployeeTempData.LocationId;
            ViewModel.employeeid = EmployeeTempData.EmployeeID;
           
            if (ViewModel.employeeid != 0)
            {
                await ViewModel.GetPayRollProcess();

            }

            //Setting Data
            if (ViewModel.PayRoll != null)
            {
                PayeeName.Text = ViewModel.PayRoll.PayeeName;
                WashHours.Text = ViewModel.PayRoll.TotalWashHours;
                DetailHours.Text = ViewModel.PayRoll.TotalDetailHours;
                WashRate.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashRate);
                RegPay.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashAmount);
                OTHours.Text = ViewModel.PayRoll.OverTimeHours;
                OTPay.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.OverTimePay);
                Collision.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Collision);
                Adjustment.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Adjustment);
                Commission.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailCommission);
                CashTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CashTip);
                CardTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CardTip);
                WashTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashTip);
                DetailTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailTip);
                Bonus.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Bonus);
                Total.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.PayeeTotal);
            }
            else 
            {

                WashHours.Text = "0.00";
                DetailHours.Text = "0.00";
                WashRate.Text = "$" + "0.00";
                RegPay.Text = "$" + "0.00";
                OTHours.Text = "0.00";
                OTPay.Text = "$" + "0.00";
                Collision.Text = "$" + "0.00";
                Adjustment.Text = "$" + "0.00";
                Commission.Text = "$" + "0.00";
                CashTips.Text = "$" + "0.00";
                CardTips.Text = "$" + "0.00";
                WashTips.Text = "$" + "0.00";
                DetailTips.Text = "$" + "0.00";
                Bonus.Text = "$" + "0.00";
                Total.Text = "$" + "0.00";
            }
            

            ViewModel.ItemLocation = ViewModel.EmployeeLocations[0].LocationName;
            ViewModel.Location = ViewModel.EmployeeLocations[0].LocationId;

            if (ViewModel.EmployeeLocations.Count != 0)
            {
                Locations = new List<string>();
                var LocationID = this.ViewModel.EmployeeLocations[0].LocationId;
                position = this.ViewModel.EmployeeLocations.FindIndex(x => x.LocationId == LocationID);
                foreach (var LocationData in ViewModel.EmployeeLocations) 
                {                    
                    Locations.Add(LocationData.LocationName);                    
                }                
                LocationAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item,Locations);
                LocationNameSpinner.Adapter = LocationAdapter;
                LocationNameSpinner.SetSelection(position);
            }
            var set = this.CreateBindingSet<PayRollFragment, PayRollViewModel>();
            set.Bind(LocationNameSpinner).To(vm => vm.ItemLocation);
            set.Apply();
        }

        private void FromDate_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today.Date;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnFromDateSet, today.Year, today.Month - 1, today.Day);
            //dialog.DatePicker.MinDate = today.Millisecond;
            var calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            var day = calendar.Get(CalendarField.DayOfMonth);
            var year = calendar.Get(CalendarField.Year);
            var month = calendar.Get(CalendarField.Month);
            dialog.DatePicker.MaxDate = calendar.TimeInMillis;             
            dialog.Show();            
        }      

        private void OnFromDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {            
            FromDate.Text = e.Date.ToString("yyyy-MM-dd");           
        }

        private void ToDate_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today.Date;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnToDateSet, today.Year, today.Month - 1, today.Day);            
            //dialog.DatePicker.MinDate = today.Millisecond;
            var calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            var day = calendar.Get(CalendarField.DayOfMonth);
            var year = calendar.Get(CalendarField.Year);
            var month = calendar.Get(CalendarField.Month);
            dialog.DatePicker.MaxDate = calendar.TimeInMillis;
            dialog.Show();           
        }

        private void OnToDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            ToDate.Text= e.Date.ToString("yyyy-MM-dd");
        }

        private async void Go_Button_Click(object sender, EventArgs e)
        {
            var fromdate = FromDate.Text;
            var  todate = ToDate.Text;
            // ViewModel.Location = 1;
            ViewModel.Fromdate = fromdate;
            ViewModel.Todate = todate;
            ViewModel.employeeid = EmployeeTempData.EmployeeID;

            if (ViewModel.employeeid != 0)
            {
                await ViewModel.GetPayRollProcess();

            }
            
            if (ViewModel.PayRoll != null)
            {
                PayeeName.Text = ViewModel.PayRoll.PayeeName;
                WashHours.Text = ViewModel.PayRoll.TotalWashHours;
                DetailHours.Text = ViewModel.PayRoll.TotalDetailHours;
                WashRate.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashRate);
                RegPay.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashAmount);
                OTHours.Text = ViewModel.PayRoll.OverTimeHours;
                OTPay.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.OverTimePay);
                Collision.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Collision);
                Adjustment.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Adjustment);
                Commission.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailCommission);
                CashTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CashTip);
                CardTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.CardTip);
                WashTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.WashTip);
                DetailTips.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.DetailTip);
                Bonus.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.Bonus);
                Total.Text = "$" + SetTwoDecimel(ViewModel.PayRoll.PayeeTotal);
            }
            else
            {

                WashHours.Text = "0.00";
                DetailHours.Text = "0.00";
                WashRate.Text = "$" + "0.00";
                RegPay.Text = "$" + "0.00";
                OTHours.Text = "0.00";
                OTPay.Text = "$" + "0.00";
                Collision.Text = "$" + "0.00";
                Adjustment.Text = "$" + "0.00";
                Commission.Text = "$" + "0.00";
                CashTips.Text = "$" + "0.00";
                CardTips.Text = "$" + "0.00";
                WashTips.Text = "$" + "0.00";
                DetailTips.Text = "$" + "0.00";
                Bonus.Text = "$" + "0.00";
                Total.Text = "$" + "0.00";
            }


        }
    }
}
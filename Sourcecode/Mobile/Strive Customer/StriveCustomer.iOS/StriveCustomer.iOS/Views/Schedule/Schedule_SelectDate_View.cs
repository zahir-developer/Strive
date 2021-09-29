using System;
using Acr.UserDialogs;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectDate_View : MvxViewController<ScheduleAppointmentDateViewModel>
    {
        NSDate date = NSDate.Now;
        //DateTime date = DateTime.Now;
        public Schedule_SelectDate_View() : base("Schedule_SelectDate_View", null)
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

        partial void dateChange(UIDatePicker sender)
        {            
            date = Schedule_datePicker.Date;
            //date = Schedule_datePicker.Date.ToDateTime();
            getTimeSlots();
        }

        public void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Select Date";

            var rightBtn = new UIButton(UIButtonType.Custom);
            rightBtn.SetTitle("Next", UIControlState.Normal);
            rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var rightBarBtn = new UIBarButtonItem(rightBtn);
            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            rightBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.NavToSelect_Preview();
            };

            SelectDate_ParentView.Layer.CornerRadius = 5;
            Date_CollectionView.Layer.CornerRadius = 5;
            Cancel_DateSchedule.Layer.CornerRadius = 5;
            Schedule_datePicker.Layer.CornerRadius = 5;
            Schedule_Date_ChildView.Layer.CornerRadius = 5;

            Date_CollectionView.RegisterNibForCell(Schedule_Time_Cell.Nib, Schedule_Time_Cell.Key);
            Date_CollectionView.ReloadData();

            getTimeSlots();

        }
       
        public async void getTimeSlots()
        {
            var dates = date.ToString();
            var FullSplitDates = dates.Split(" ");
            var fullDateInfo = FullSplitDates[0].Split("-");

            switch (fullDateInfo[1])
            {
                case "01":
                    CustomerScheduleInformation.ScheduleMonth = "January";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "02":
                    CustomerScheduleInformation.ScheduleMonth = "February";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "03":
                    CustomerScheduleInformation.ScheduleMonth = "March";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "04":
                    CustomerScheduleInformation.ScheduleMonth = "April";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "05":
                    CustomerScheduleInformation.ScheduleMonth = "May";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "06":
                    CustomerScheduleInformation.ScheduleMonth = "June";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "07":
                    CustomerScheduleInformation.ScheduleMonth = "July";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "08":
                    CustomerScheduleInformation.ScheduleMonth = "August";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "09":
                    CustomerScheduleInformation.ScheduleMonth = "September";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "10":
                    CustomerScheduleInformation.ScheduleMonth = "October";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "11":
                    CustomerScheduleInformation.ScheduleMonth = "November";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
                case "12":
                    CustomerScheduleInformation.ScheduleMonth = "December";
                    CustomerScheduleInformation.ScheduleDate = fullDateInfo[2].ToString();
                    CustomerScheduleInformation.ScheduleYear = fullDateInfo[0].ToString();
                    break;
            }
            this.ViewModel.checkDate = CustomerScheduleInformation.ScheduleDate + "/" + CustomerScheduleInformation.ScheduleMonth + "/"
                + CustomerScheduleInformation.ScheduleYear;
            CustomerScheduleInformation.ScheduleFullDate = (date.ToString()).Substring(0,10);
            //CustomerScheduleInformation.ScheduleFullDate = date.Year + "-" + date.Month + "-" + date.Day;
            await this.ViewModel.GetSlotAvailability(CustomerScheduleInformation.ScheduleLocationCode, date.ToString());           

            if (this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
            {
                Date_CollectionView.Hidden = false;
                Date_CollectionView.DataSource = new ScheduleDate_CollectionSource(this.ViewModel.ScheduleSlotInfo);
                Date_CollectionView.Delegate = new timeSlotSourceDelegate(Date_CollectionView, this.ViewModel.ScheduleSlotInfo);
            }
            else
            {
                Date_CollectionView.Hidden = true;
            }
        }

        partial void CancelDate_BtnTouch(UIButton sender)
        {
            ViewModel.NavToSchedule();
        }
    }

    public partial class timeSlotSourceDelegate : UICollectionViewDelegate
    {        
        public UICollectionView timeSlot_CollectionView { get; set; }
        public AvailableScheduleSlots timeSlots { get; set; }

        public timeSlotSourceDelegate(UICollectionView uICollectionView, AvailableScheduleSlots slots)
        {
            timeSlot_CollectionView = uICollectionView;
            timeSlots = slots;
        }

        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }
        public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }       
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("Schedule_Time_Cell", indexPath) as Schedule_Time_Cell;
            cell.ContentView.BackgroundColor = UIColor.Gray;           
        }        
    }
}


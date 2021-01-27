using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectDate_View : MvxViewController<ScheduleAppointmentDateViewModel>
    {
        NSDate date = NSDate.Now;
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

            //save the date in viewmodel
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
            await this.ViewModel.GetSlotAvailability(CustomerScheduleInformation.ScheduleLocationCode, date.ToString());

            //var timeSlotSource = new ScheduleDate_CollectionSource(this.ViewModel.ScheduleSlotInfo);
            //Date_CollectionView.Source = timeSlotSource;
            //Date_CollectionView.DelaysContentTouches = false;
            //Date_CollectionView.ReloadData();

            if (this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
            {
                Date_CollectionView.Hidden = false;
                Date_CollectionView.DataSource = new ScheduleDate_CollectionSource(this.ViewModel.ScheduleSlotInfo);
                Date_CollectionView.Delegate = new timeSlotSourceDelegate(Date_CollectionView);
            }
            else
            {
                Date_CollectionView.Hidden = true;
            }
        }        
    }

    public partial class timeSlotSourceDelegate : UICollectionViewDelegate
    {
        public UICollectionView timeSlot_CollectionView { get; set; }

        public timeSlotSourceDelegate(UICollectionView uICollectionView)
        {
            timeSlot_CollectionView = uICollectionView;
        }

        public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            base.ItemHighlighted(collectionView, indexPath);
        }

        public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        {
            base.ItemUnhighlighted(collectionView, indexPath);
        }
    }
}


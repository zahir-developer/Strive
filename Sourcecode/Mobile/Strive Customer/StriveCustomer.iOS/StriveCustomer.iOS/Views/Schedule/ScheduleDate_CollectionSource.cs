using System;
using Foundation;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class ScheduleDate_CollectionSource : UICollectionViewDataSource
    {
        AvailableScheduleSlots slots;
        public ScheduleDate_CollectionSource(AvailableScheduleSlots slotsAvailable)  
        {
            this.slots = slotsAvailable;
        }
        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return slots.GetTimeInDetails.Count;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("Schedule_Time_Cell", indexPath) as Schedule_Time_Cell;
            cell.SetData(slots, indexPath);
            return cell;
        }

        public override bool CanMoveItem(UICollectionView collectionView, NSIndexPath indexPath)
        {            
            return true;
        }
    }
}

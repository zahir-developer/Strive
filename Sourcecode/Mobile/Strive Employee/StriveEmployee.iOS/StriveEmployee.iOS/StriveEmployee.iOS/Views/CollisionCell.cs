using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.PersonalDetails;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public partial class CollisionCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CollisionCell");
        public static readonly UINib Nib;

        static CollisionCell()
        {
            Nib = UINib.FromName("CollisionCell", NSBundle.MainBundle);
        }

        protected CollisionCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexpath, List<EmployeeCollision> collisionList)
        {
            Collision_CellView.Layer.CornerRadius = 5;
            Collision_CellText.Text = collisionList[indexpath.Row].LiabilityType;

            if (!String.IsNullOrEmpty(collisionList[indexpath.Row].CreatedDate))
            {
                var date = collisionList[indexpath.Row].CreatedDate.Split("T");
                Collision_CellDate.Text = date[0];
            }
            Collision_CellAmount.Text = collisionList[indexpath.Row].Amount.ToString();
        }
    }
}

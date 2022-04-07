using System;

using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Sources;
using Greeter.Storyboards;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;

namespace Greeter.Cells
{
    public partial class IssueCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("IssueCell");
        public static readonly UINib Nib;
        public int issueid = 0;
        
        ImagesSource imagesSource; 
        
        IssuesViewController IssuesViewController;
        //IssuesSource IssuesSource = new IssuesSource();
        Action<int> DeleteAction = null;
        static IssueCell()
        {
            
            Nib = UINib.FromName("IssueCell", NSBundle.MainBundle);

        }

    
        protected IssueCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            cvImages.RegisterNibForCell(ImageCell.Nib, ImageCell.Key);
            
        }

        public void UpdateData( VehicleIssueResponse vehicleIssue, NSIndexPath index)
        {
             Dictionary<int, UIImage> Images = new Dictionary<int, UIImage>();
             
            lblDate.Text = vehicleIssue.VehicleIssueThumbnail.VehicleIssue[index.Row].CreatedDate.Substring(0,10);
            lblDesc.Text = vehicleIssue.VehicleIssueThumbnail.VehicleIssue[index.Row].Description;
            issueid = vehicleIssue.VehicleIssueThumbnail.VehicleIssue[index.Row].VehicleIssueid;
            var images = vehicleIssue.VehicleIssueThumbnail.VehicleIssueImage.FindAll(x => x.VehicleIssueId == issueid);
            foreach (var item in images)
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(item.Base64Thumbnail);
                NSData data = NSData.FromArray(encodedDataAsBytes);
                var uiImage = UIImage.LoadFromData(data);
                //uiImage.Transform = CGAffineTransform.MakeRotation(3.14159f * 90 / 180f);
                if (!Images.ContainsKey(item.VehicleIssueImageId))
                    Images.Add(item.VehicleIssueImageId, uiImage);
                
            }    
                    

            imagesSource = new(Images.Values.ToList(),Images.Keys.ToList());
            cvImages.WeakDataSource = imagesSource;
            cvImages.WeakDelegate = imagesSource;
            
        }
        partial void CloseBtnClicked(UIButton sender)
        {

            var dict = new NSDictionary(new NSString("issueid"), new NSString(issueid.ToString()));
            NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.delete_clicked"), null, dict);
        }

    }
}

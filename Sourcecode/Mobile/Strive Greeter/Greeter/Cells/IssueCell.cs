using System;

using Foundation;
using Greeter.Sources;
using UIKit;

namespace Greeter.Cells
{
    public partial class IssueCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("IssueCell");
        public static readonly UINib Nib;

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

            var imagesSource = new ImagesSource();
            cvImages.RegisterNibForCell(ImageCell.Nib, ImageCell.Key);
            cvImages.Source = imagesSource;
        }

        public void UpdateData(string date, string desc, string[] images)
        {
            // TODO : update data to ui
        }
    }
}

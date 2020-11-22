using System;

using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class PastDetailViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PastDetailViewCell");
        public static readonly UINib Nib;
        public ProfileView profileViewController = new ProfileView();
        private PastClientServices pservices = new PastClientServices();

        static PastDetailViewCell()
        {
            Nib = UINib.FromName("PastDetailViewCell", NSBundle.MainBundle);
        }

        protected PastDetailViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetCell()
        {
            PastDetailCellView.Layer.CornerRadius = 5;  
        }

        public void SetData(PastClientServices services, NSIndexPath indexpath)
        {           
            PastDetailLabel.Text = services.PastClientDetails[indexpath.Row].Color + " " + services.PastClientDetails[indexpath.Row].Make + " " + services.PastClientDetails[indexpath.Row].Model;
            PastDetail_Arrow.Image = UIImage.FromBundle("icon-right-arrow");
        }
    }
}

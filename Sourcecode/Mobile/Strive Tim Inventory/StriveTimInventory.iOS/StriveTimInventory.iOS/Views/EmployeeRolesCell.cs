using System;
using Acr.UserDialogs;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class EmployeeRolesCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("EmployeeRolesCell");
        public static readonly UINib Nib;
        private static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        static EmployeeRolesCell()
        {
            Nib = UINib.FromName("EmployeeRolesCell", NSBundle.MainBundle);
            
        }

        protected EmployeeRolesCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() =>
            {
                var set = (this).CreateBindingSet<EmployeeRolesCell, EmployeeRole>();
                set.Apply();
            });
        }

        public void SetCell(EmployeeRolesCell cell, EmployeeRole role)
        {
            cell.ImgView.Image = UIImage.FromBundle(role.ImageUri);
            if(role.ImageUri != null)
            {
                _userDialog.AlertAsync("Got Roles");
            }
        }
    }
}

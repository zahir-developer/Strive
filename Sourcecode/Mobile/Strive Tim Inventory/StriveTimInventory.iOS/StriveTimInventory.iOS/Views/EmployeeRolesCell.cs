using System;

using Foundation;
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
                //set.Bind(bookThumbnail).For(c => c.ImagePath).To(vm => vm.PhotoUri);
                //set.Bind(bookName).To(vm => vm.Name);
                set.Bind(RoleTitileLabel).To(vm => vm.Title);
                set.Apply();
            });
        }
    }
}

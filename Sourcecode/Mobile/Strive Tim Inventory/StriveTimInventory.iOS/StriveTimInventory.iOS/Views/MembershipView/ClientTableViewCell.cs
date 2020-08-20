using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ClientTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("ClientTableViewCell`");
        public static readonly UINib Nib;

        static ClientTableViewCell()
        {
            Nib = UINib.FromName("ClientTableViewCell", NSBundle.MainBundle);
        }

        protected ClientTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() => {
                var set = this.CreateBindingSet<ClientTableViewCell, ClientDetail>();
                set.Bind(ClientName).To(vm => vm.FirstName);
                set.Apply();
            });
        }
    }
}

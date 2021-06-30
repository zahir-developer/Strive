// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Greeter
{
    [Register("LocationViewController")]
    partial class LocationViewController
    {
        [Outlet]
        UIKit.UIButton btnDropdown { get; set; }

        [Outlet]
        UIKit.UIButton btnNext { get; set; }

        [Outlet]
        UIKit.UITextField tfLocation { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (btnDropdown != null)
            {
                btnDropdown.Dispose();
                btnDropdown = null;
            }

            if (btnNext != null)
            {
                btnNext.Dispose();
                btnNext = null;
            }

            if (tfLocation != null)
            {
                tfLocation.Dispose();
                tfLocation = null;
            }

            if (btnDropdown != null)
            {
                btnDropdown.Dispose();
                btnDropdown = null;
            }
        }
    }
}

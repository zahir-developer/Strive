using System;
using System.Collections.Specialized;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using UIKit;

namespace StriveCustomer.iOS.MvvmCross
{
    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public void Include(UITextField textField)
        {
            textField.Text = textField.Text + "";
            textField.EditingChanged += (sender, args) => { textField.Text = ""; };
            textField.EditingDidEnd += (sender, args) => { textField.Text = ""; };
        }

        public void Include(UIButton uiButton)
        {
            uiButton.TouchUpInside += (s, e) =>
                                        uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
        }
    }
}

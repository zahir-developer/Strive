using System;
using UIKit;

namespace StriveOwner.iOS.MvvmCross
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

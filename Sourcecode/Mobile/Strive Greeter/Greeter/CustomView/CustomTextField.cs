using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Greeter.CustomView
{
    public class CustomTextField : UITextField
    {
        public CustomTextField(): base() { }

        public CustomTextField(CGRect rect) : base(rect) { }

        public CustomTextField(IntPtr ptr) : base(ptr) { }

        public CustomTextField(NSObjectFlag flag) : base(flag) { }

        public CustomTextField(NSCoder coder) : base(coder) { }

        public override bool CanPerform(Selector action, NSObject withSender)
        {
            if (action == new Selector("paste:")) return false;

            //if (action == new Selector("copy:")) return false;

            if (action == new Selector("cut:")) return false;

            return base.CanPerform(action, withSender);
        }
    }
}

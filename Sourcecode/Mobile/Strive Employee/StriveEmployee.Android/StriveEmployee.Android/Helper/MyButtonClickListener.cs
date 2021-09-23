using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveEmployee.Android.Helper
{
    public interface MyButtonClickListener
    {
        void OnClick(int position,checkOutViewModel checkOut); 
    }
}
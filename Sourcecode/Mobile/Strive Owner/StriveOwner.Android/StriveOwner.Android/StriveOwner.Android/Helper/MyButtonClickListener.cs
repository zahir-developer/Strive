using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StriveOwner.Android.Helper
{
    public interface MyButtonClickListener
    {
        void OnClick(int position,checkOutViewModel checkOut);         
    }
    public interface DeleteButtonClickListener 
    {
        void OnClick(int position, InventoryDataModel filteredlist);
    }
}
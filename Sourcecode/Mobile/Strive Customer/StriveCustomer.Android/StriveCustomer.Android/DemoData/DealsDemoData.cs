using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.DemoData
{
    public class DealsDemoData
    {
        public string DealName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ExpiryDate {get; set;}
        public int DealId { get; set; }
        public string DealWashes { get; set; }
        public string DealCost { get; set; }
        public string Description { get; set; }
    }

    public static class DealsInformation
    {
        public static DealsDemoData selectedDeal { get; set; }
    }
}
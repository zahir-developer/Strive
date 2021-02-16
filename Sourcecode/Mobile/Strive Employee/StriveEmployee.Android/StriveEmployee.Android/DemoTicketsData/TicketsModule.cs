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

namespace StriveEmployee.Android.DemoTicketsData
{
    public class TicketsModule
    {
        public string TicketNumber { get; set; }
        public string WashService { get; set; }
        public string MakeModelColor { get; set; }
        public string Upcharge { get; set; }
        public string Barcode { get; set; }
        public string Customer { get; set; }
        public string AdditionalServices { get; set; }
    }
}
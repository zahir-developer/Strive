using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Utils.Employee;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StriveEmployee.Android.Views
{
    [Activity(Label = "DocumentView")]
    public class DocumentView : Activity
    {
        private ImageView Docview;
        private TextView text;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DocumentView);

            //Docview = this.FindViewById<ImageView>(Resource.Id.imageView1);
            //Image image;
            text = this.FindViewById<TextView>(Resource.Id.textView1);
            var data = MyProfileTempData.DocumentString;
            byte[] dataconverted = Convert.FromBase64String(data);
            string decodedString = Encoding.UTF8.GetString(dataconverted);
            text.Text = decodedString;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Strive.Core.Services.Interfaces;
using static Android.Manifest;
using ContentPermission = Android.Content.PM;
namespace StriveCustomer.Android.Services
{
    public static class AndroidPermissions 
    {
        const int REQUEST_LOCATION = 99;
        
        public static Task checkPermissions(Activity context)
        {
            var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation };
            ActivityCompat.RequestPermissions(context,requiredPermissions,REQUEST_LOCATION);

            return Task.CompletedTask;
        }
    }
}
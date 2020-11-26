using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace StriveEmployee.Android.Resources.Services
{
    public static class Permission_Services
    {
        const int PERMISSION_REQUEST_CAMERA = 1;
        const int PERMISSION_REQUEST_READ_EXTERNAL_STORAGE = 2;
        const int PERMISSION_REQUEST_WRITE_EXTERNAL_STORAGE = 3;
        public static Task checkCameraPermission(DialogFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.Camera };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions, PERMISSION_REQUEST_CAMERA);
            }
            return Task.CompletedTask;
        }

        public static Task ReadExternalStoragePermission(DialogFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.ReadExternalStorage };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions, PERMISSION_REQUEST_READ_EXTERNAL_STORAGE);
            }
            return Task.CompletedTask;
        }

        public static Task WriteExternalStoragePermission(DialogFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.WriteExternalStorage };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions, PERMISSION_REQUEST_WRITE_EXTERNAL_STORAGE);
            }
            return Task.CompletedTask;
        }
    }
}
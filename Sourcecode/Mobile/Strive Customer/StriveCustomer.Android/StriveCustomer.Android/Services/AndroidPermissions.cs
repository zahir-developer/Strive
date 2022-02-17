using System;
using System.Threading.Tasks;
using Android;
using Android.Support.V4.App;
using MvvmCross.Droid.Support.V4;
using ContentPermission = Android.Content.PM;
namespace StriveCustomer.Android.Services
{
    public static class AndroidPermissions 
    {
        const int PERMISSION_REQUEST_CAMERA = 1;
        const int PERMISSION_REQUEST_LOCATION = 99;
        const int PERMISSION_REQUEST_EXTERNALSTORAGE = 999;
        public static Task checkLocationPermission(MvxFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != ContentPermission.Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions, PERMISSION_REQUEST_LOCATION);
            }
            return Task.CompletedTask;
        }
        public static Task checkCameraPermission(MvxFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.Camera };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != ContentPermission.Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions,PERMISSION_REQUEST_CAMERA);   
            }
            return Task.CompletedTask;
        }
        public static Task checkExternalStoragePermission(MvxFragment fragment)
        {
            var requiredPermissions = new String[] { Manifest.Permission.WriteExternalStorage };
            if (ActivityCompat.CheckSelfPermission(fragment.Context, requiredPermissions[0]) != ContentPermission.Permission.Granted)
            {
                fragment.RequestPermissions(requiredPermissions, PERMISSION_REQUEST_EXTERNALSTORAGE);
            }
            return Task.CompletedTask;
        }
    }
}
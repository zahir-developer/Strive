using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using StriveEmployee.Android.Resources.Services;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{    
    public class AddCollisions_DialogFragment : BottomSheetDialogFragment
    {
        private ImageView openGallery_ImageView;
        private ImageView openCamera_ImageView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddCollisions_BottomSheet, container, false);

            openGallery_ImageView = view.FindViewById<ImageView>(Resource.Id.openGallery_ImageView);
            openCamera_ImageView = view.FindViewById<ImageView>(Resource.Id.openCamera_ImageView);

            openGallery_ImageView.Click += OpenGallery_ImageView_Click;
            openCamera_ImageView.Click += OpenCamera_ImageView_Click;



            return view;
        }

        private void OpenCamera_ImageView_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        private async void OpenGallery_ImageView_Click(object sender, EventArgs e)
        {
            UploadPhoto();
        }

        private async void TakePhoto()
        {
            await Permission_Services.checkCameraPermission(this);
            await Permission_Services.ReadExternalStoragePermission(this);
            await Permission_Services.WriteExternalStoragePermission(this);

            if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.Camera) == Permission.Granted &&
                ContextCompat.CheckSelfPermission(Context, Manifest.Permission.ReadExternalStorage) == Permission.Granted &&
                ContextCompat.CheckSelfPermission(Context, Manifest.Permission.WriteExternalStorage) == Permission.Granted)
            {

            }
        }

        private async void UploadPhoto()
        {
          
        }
    }
}
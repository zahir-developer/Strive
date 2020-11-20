using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.Media;
using StriveEmployee.Android.Resources.Services;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    public class AddCollisions_DialogFragment : BottomSheetDialogFragment
    {
        private ImageView openGallery_ImageView;
        private ImageView openCamera_ImageView;
        readonly string[] permissionGroups =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera,
        };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            Xamarin.Essentials.Platform.Init((Activity)this.Context, savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddCollisions_BottomSheet, container, false);

            openGallery_ImageView = view.FindViewById<ImageView>(Resource.Id.openGallery_ImageView);
            openCamera_ImageView = view.FindViewById<ImageView>(Resource.Id.openCamera_ImageView);

            openGallery_ImageView.Click += OpenGallery_ImageView_Click;
            openCamera_ImageView.Click += OpenCamera_ImageView_Click;
            RequestPermissions(permissionGroups, 0);


            return view;
        }
        private void OpenCamera_ImageView_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        private void OpenGallery_ImageView_Click(object sender, EventArgs e)
        {
            UploadPhoto();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        async void TakePhoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        CompressionQuality = 40,
                        Name = "collisionandroid.jpg",
                        Directory = "sample"
                    });
                    if (file == null)
                    {
                        return;
                    }
                    byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                    Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                }               
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async void UploadPhoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if(CrossMedia.Current.IsPickPhotoSupported)
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {

                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                        CompressionQuality = 40,


                    });
                    byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                    Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                }  
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }
    }
}


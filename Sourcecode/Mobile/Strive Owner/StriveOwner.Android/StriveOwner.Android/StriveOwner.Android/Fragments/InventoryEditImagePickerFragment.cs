using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveOwner.Android.Fragments
{
    public class InventoryEditImagePickerFragment : MvxFragment<IconsViewModel>
    {
        private Button imagePickerCancelButton;
        private RecyclerView imagePickerRecyclerView;
        private InventoryEditImagePickerAdapter imagePickerAdapter;
        Context context;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.InventoryEditImagePicker_Layout, null);
            imagePickerCancelButton =rootView.FindViewById<Button>(Resource.Id.cancelButton);
            imagePickerRecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.imagePickerRecyclerView);
            context = this.Context;
            GetImages();
            imagePickerCancelButton.Click += ImagePickerCancelButton_Click;
            return rootView;
            //return inflater.Inflate(Resource.Layout.InventoryEditImagePicker_Layout, container, false);           
        }

        private void GetImages()
        {
            var layoutManager = new GridLayoutManager(context, 3, (int)GridOrientation.Vertical, false);
            imagePickerRecyclerView.SetLayoutManager(layoutManager);
            imagePickerAdapter = new InventoryEditImagePickerAdapter(new string[] { "1","2"});
            imagePickerRecyclerView.SetAdapter(imagePickerAdapter);          

        }

        private void ImagePickerCancelButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
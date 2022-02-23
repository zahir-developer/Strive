using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using StriveOwner.Android.Resources.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveOwner.Android.Fragments
{
    public class InventoryEditImagePickerFragment : MvxFragment<IconsViewModel>, View.IOnClickListener
    {
        private Button imagePickerCancelButton;
        private RecyclerView imagePickerRecyclerView;
        private InventoryEditImagePickerAdapter imagePickerAdapter;
        private InventoryEditFragment inventoryEditFragment;
        private static int[] productIcons;
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
            //imagePickerRecyclerView.SetOnClickListener(I);
            context = this.Context;
            AddProductImages();
            SetImages();
            imagePickerCancelButton.Click += ImagePickerCancelButton_Click;
            return rootView;
            //return inflater.Inflate(Resource.Layout.InventoryEditImagePicker_Layout, container, false);           
        }

        private void AddProductImages()
        {
            productIcons=new int[]
            {
              Resource.Drawable.Artboard,
              Resource.Drawable.bottle,
              Resource.Drawable.broom,
              Resource.Drawable.can,
              Resource.Drawable.can_blue,
              Resource.Drawable.can_orange,
              Resource.Drawable.can_red,
              Resource.Drawable.cap,
              Resource.Drawable.chips,
              Resource.Drawable.coffee,
              Resource.Drawable.coffeecup,
              Resource.Drawable.coffee_red,
              Resource.Drawable.glass,
              Resource.Drawable.milk,
              Resource.Drawable.shampoo,
              Resource.Drawable.shampoo_new,
              Resource.Drawable.soap,
              Resource.Drawable.trainers,
              Resource.Drawable.water,
              Resource.Drawable.Wiper,
              
            };          
           
        }       
        private void SetImages()
        {
            var layoutManager = new GridLayoutManager(context, 3, (int)GridOrientation.Vertical, false);
            imagePickerRecyclerView.SetLayoutManager(layoutManager);
            imagePickerAdapter = new InventoryEditImagePickerAdapter(productIcons);
            imagePickerAdapter.ItemClick += ImagePickerAdapter_ItemClick;           
            imagePickerRecyclerView.SetAdapter(imagePickerAdapter);
           
        }

        private void ImagePickerAdapter_ItemClick(object sender, InventoryEditImagePickerAdapterClickEventArgs e)
        {
            e.View.SetBackgroundColor(Color.Blue);
            inventoryEditFragment.edit_image.SetImageResource(productIcons[e.Position]);            
        }

        private void ImagePickerCancelButton_Click(object sender, EventArgs e)
        {
            inventoryEditFragment = new InventoryEditFragment();
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, inventoryEditFragment).Commit();
        }

        public void OnClick(View v)
        {
            
        }
    }
}
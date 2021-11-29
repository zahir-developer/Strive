using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryMainFragment : MvxFragment<InventoryViewModel>
    {
        private InventoryMainAdapter inventoryMainAdapter;
        private RecyclerView inventoryMain_RecyclerView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.InventoryMain_Fragment, null);
            this.ViewModel = new InventoryViewModel();
            inventoryMain_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.inventoryMain_RecyclerView);
            GetProducts();
            
            return rootView;
        }
        private async void GetProducts()
        {
            await this.ViewModel.GetProductsCommand();
            await this.ViewModel.GetVendorsCommand();
            await this.ViewModel.InventorySearchCommand(" ");
            InventoryAdapterData();
        }
        private void InventoryAdapterData()
        {
            inventoryMainAdapter = new InventoryMainAdapter(Context, this.ViewModel.FilteredList);
            var layoutManager = new LinearLayoutManager(Context);
            inventoryMain_RecyclerView.SetLayoutManager(layoutManager);
            inventoryMain_RecyclerView.SetAdapter(inventoryMainAdapter);
        }
    }
}
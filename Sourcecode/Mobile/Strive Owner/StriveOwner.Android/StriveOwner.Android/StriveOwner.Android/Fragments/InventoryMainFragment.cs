using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using StriveOwner.Android.Helper;
using StriveOwner.Android.Resources.Fragments;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryMainFragment : MvxFragment<InventoryViewModel> //, SwipeControllerActions
    {
        private InventoryMainAdapter inventoryMainAdapter;
        private RecyclerView inventoryMain_RecyclerView;
        private SearchView inventoryMain_SearchView;
        private Context context;
        private AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;

        public InventoryMainFragment(Context context)
        {
            this.context = context;
        }

        // private SwipeController swipeController;


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
            inventoryMain_SearchView = rootView.FindViewById<SearchView>(Resource.Id.inventory_SearchView);
            inventoryMain_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.inventoryMain_RecyclerView);
            GetProducts();
            inventoryMain_SearchView.QueryTextChange += InventoryMain_SearchView_QueryTextChange;
            return rootView;
        }

        private async void InventoryMain_SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            await ViewModel.InventorySearchCommand(e.NewText);
            InventoryAdapterData();
        }

        private async void GetProducts()
        {
            await this.ViewModel.GetProductsCommand();
            await this.ViewModel.GetVendorsCommand();
            await this.ViewModel.InventorySearchCommand(" ");
            InventoryAdapterData();
        }
        public void OnRightClicked(int position)
        {
            this.ViewModel.FilteredList.RemoveAt(position);
            inventoryMainAdapter.NotifyItemRemoved(position);
            inventoryMainAdapter.NotifyItemRangeChanged(position, inventoryMainAdapter.ItemCount);
        }
        private void InventoryAdapterData()
        {
            inventoryMainAdapter = new InventoryMainAdapter(Context, this.ViewModel.FilteredList);
            var layoutManager = new LinearLayoutManager(Context);
            inventoryMain_RecyclerView.SetLayoutManager(layoutManager);
            inventoryMain_RecyclerView.SetAdapter(inventoryMainAdapter);
            MySwipeHelper mySwipe = new MyImplementSwipeHelper(Context, inventoryMain_RecyclerView, 200, ViewModel.FilteredList);           
        }
        public void DeleteItem(InventoryDataModel selectedItem, RecyclerView inventoryMain_RecyclerView)
        {

            Builder = new AlertDialog.Builder(Context);
            Builder.SetMessage("Are you sure want to change the status to hold?");
            Builder.SetTitle("Hold");
            okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {
                DeleteFromList(selectedItem, inventoryMain_RecyclerView);
            });
            removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {

            });
            Builder.SetPositiveButton("Ok", okHandler);
            Builder.SetNegativeButton("Cancel", removePhotoHandler);
            Builder.Create();
            Builder.Show();
        }
        public async void DeleteFromList(InventoryDataModel selectedItem, RecyclerView recyclerView)
        {
            RecyclerView recyclerView1;
            ViewModel = new InventoryViewModel();
            int index = ViewModel.FilteredList.IndexOf(selectedItem);
            await ViewModel.DeleteProductCommand(index);
            inventoryMainAdapter.NotifyDataSetChanged();            
        }
        private class MyImplementSwipeHelper : MySwipeHelper
        {
            Context Context;
            RecyclerView inventoryMain_RecyclerView;
            int buttonWidth;
            ObservableCollection<InventoryDataModel> filteredlist;

            public MyImplementSwipeHelper(Context context, RecyclerView inventoryMain_RecyclerView, int buttonWidth, ObservableCollection<InventoryDataModel> filteredlist) : base(context, inventoryMain_RecyclerView, 200, filteredlist)
            {
                Context = context;
                this.inventoryMain_RecyclerView = inventoryMain_RecyclerView;
                this.buttonWidth = buttonWidth;
                this.filteredlist = filteredlist;
            }

            public override void InstantiateMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {
                int itemposition = viewHolder.AdapterPosition;
                var selectedItem = filteredlist[itemposition];
                InventoryMainFragment fragment = new InventoryMainFragment(Context);
                //button-1
                buffer.Add(new MyButton(Context,
                    "Delete",
                    35,
                    Resource.Drawable.ic_errorstatus,
                    "#FF0000",
                    new DeleteButtonClick(this, Context, selectedItem, fragment, inventoryMain_RecyclerView), selectedItem));

            }
            public override void InstantiateUpdatedMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {

            }
            public class DeleteButtonClick : DeleteButtonClickListener
            {
                private MyImplementSwipeHelper myImplementSwipeHelper;

                RecyclerView inventoryMain_RecyclerView;

                public AlertDialog.Builder Builder;
                private EventHandler<DialogClickEventArgs> okHandler;
                private EventHandler<DialogClickEventArgs> removePhotoHandler;
                Context Context;
                InventoryDataModel filteredlist;
                InventoryMainFragment fragment;

                public DeleteButtonClick(MyImplementSwipeHelper myImplementSwipeHelper, Context context, InventoryDataModel filteredlist, InventoryMainFragment fragment, RecyclerView recyclerView)
                {
                    this.myImplementSwipeHelper = myImplementSwipeHelper;
                    this.Context = context;
                    this.filteredlist = filteredlist;
                    this.fragment = fragment;
                    this.inventoryMain_RecyclerView = recyclerView;
                }

                public void OnClick(int position, InventoryDataModel selectedItem)
                {
                    fragment.DeleteItem(selectedItem, inventoryMain_RecyclerView);
                }
            }
        }        

    }
}

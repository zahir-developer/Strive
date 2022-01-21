using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
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
=======
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
using StriveOwner.Android.Resources.Fragments;
=======

>>>>>>> Stashed changes
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryMainFragment : MvxFragment<InventoryViewModel> //, SwipeControllerActions
    {
        private InventoryMainAdapter inventoryMainAdapter;
        private RecyclerView inventoryMain_RecyclerView;
        private SearchView inventoryMain_SearchView;
<<<<<<< Updated upstream
        private Context context;
        private AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;

        public InventoryMainFragment(Context context)
        {
            this.context = context;
=======
        private Context Context;
        private AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;
        private bool isSwipeCalled;

        public InventoryMainFragment(Context context)
        {
            this.Context = context;
>>>>>>> Stashed changes
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
            isSwipeCalled = false;
            GetProducts();
            inventoryMain_SearchView.QueryTextChange += InventoryMain_SearchView_QueryTextChange;
            return rootView;
        }

        private async void InventoryMain_SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
<<<<<<< Updated upstream
        {
            await ViewModel.InventorySearchCommand(e.NewText);
            InventoryAdapterData();
        }

        private async void GetProducts()
=======
>>>>>>> Stashed changes
        {
            await ViewModel.InventorySearchCommand(e.NewText);
            InventoryAdapterData();
        }
<<<<<<< Updated upstream
        public void OnRightClicked(int position)
        {
            this.ViewModel.FilteredList.RemoveAt(position);
            inventoryMainAdapter.NotifyItemRemoved(position);
            inventoryMainAdapter.NotifyItemRangeChanged(position, inventoryMainAdapter.ItemCount);
        }
        private void InventoryAdapterData()
=======

        private async void GetProducts()
        {
            var vendors = await GetVendors();
            if (vendors)
            {
                ViewModel.ClearCommand();
                await ViewModel.InventorySearchCommand("");
            }
             InventoryAdapterData();
        }

        private async Task<bool> GetVendors()
        {
            await ViewModel.GetVendorsCommand();
            return true;
        }
        private  void InventoryAdapterData()
>>>>>>> Stashed changes
        {
            inventoryMainAdapter = new InventoryMainAdapter(Context, ViewModel.FilteredList , ViewModel);
            var layoutManager = new LinearLayoutManager(Context);
            inventoryMain_RecyclerView.SetLayoutManager(layoutManager);
            inventoryMain_RecyclerView.SetAdapter(inventoryMainAdapter);
<<<<<<< Updated upstream
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
=======
            if(ViewModel.FilteredList!=null && ViewModel.FilteredList.Count > 0 && !isSwipeCalled)
            {
                MySwipeHelper mySwipe = new MyImplementSwipeHelper(Context, inventoryMain_RecyclerView, 200 ,this.ViewModel);
                isSwipeCalled = true;
            }

        }


        private  void InventoryAdapterDataAsync(RecyclerView recyclerView, InventoryViewModel viewModel)
        {
            inventoryMain_RecyclerView = recyclerView;
            inventoryMainAdapter = new InventoryMainAdapter(Context, viewModel.FilteredList, viewModel);
            var layoutManager = new LinearLayoutManager(Context);
            inventoryMain_RecyclerView.SetLayoutManager(layoutManager);
            inventoryMain_RecyclerView.SetAdapter(inventoryMainAdapter);
        }
       

        public void DeleteItem(InventoryDataModel selectedItem, RecyclerView inventoryMain_RecyclerView, InventoryViewModel viewModel)
        {

            Builder = new AlertDialog.Builder(Context);
            Builder.SetMessage("Are you sure want to delete this item?");
            Builder.SetTitle("Delete");
            okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {
                DeleteFromList(selectedItem, inventoryMain_RecyclerView,viewModel);
>>>>>>> Stashed changes
            });
            removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {

            });
<<<<<<< Updated upstream
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
=======
            Builder.SetPositiveButton("Yes", okHandler);
            Builder.SetNegativeButton("No", removePhotoHandler);
            Builder.Create();
            Builder.Show();
        }
        public async void DeleteFromList(InventoryDataModel selectedItem, RecyclerView recyclerView , InventoryViewModel viewModel)
        {
            RecyclerView recyclerView1;
            int index = viewModel.FilteredList.IndexOf(selectedItem);
            var response = await viewModel.DeleteProductCommand(index);
            if (response)
            {
                await viewModel.InventorySearchCommand(" ");
                InventoryAdapterDataAsync(recyclerView,viewModel);
            }
           
        }
        private class MyImplementSwipeHelper : MySwipeHelper
        {
            Context Context;
            RecyclerView inventoryMain_RecyclerView;
            InventoryViewModel viewModel;
            public MyImplementSwipeHelper(Context context, RecyclerView inventoryMain_RecyclerView, int buttonWidth, InventoryViewModel viewModel) : base(context, inventoryMain_RecyclerView, 200, viewModel)
            {
                Context = context;
                this.inventoryMain_RecyclerView = inventoryMain_RecyclerView;   
                this.viewModel = viewModel;
            }

            public override void InstantiateMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {
                int itemposition = viewHolder.AdapterPosition;
                var selectedItem = viewModel.FilteredList[itemposition];
                InventoryMainFragment fragment = new InventoryMainFragment(Context);
                //button-1
                buffer.Add(new MyButton(Context,
                    "Delete",
                    35,
                    Resource.Drawable.delete,
                    "#FF0000",
                    new DeleteButtonClick(fragment, inventoryMain_RecyclerView,viewModel), selectedItem));

            }
            public override void InstantiateUpdatedMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {

            }
            public class DeleteButtonClick : DeleteButtonClickListener
            {
                RecyclerView inventoryMain_RecyclerView;
                InventoryMainFragment fragment;
                InventoryViewModel viewModel;
                public DeleteButtonClick(InventoryMainFragment fragment, RecyclerView recyclerView, InventoryViewModel viewModel)
                {
                    this.fragment = fragment;
                    this.inventoryMain_RecyclerView = recyclerView;
                    this.viewModel = viewModel;
                }

                public void OnClick(InventoryDataModel selectedItem)
                {
                    if (selectedItem.Product.Quantity <= 0)
                    {
                        fragment.DeleteItem(selectedItem, inventoryMain_RecyclerView,viewModel);

                    }
                    
                }

            }
>>>>>>> Stashed changes
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

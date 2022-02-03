using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using StriveOwner.Android.Helper;
using OperationCanceledException = System.OperationCanceledException;
using SearchView = Android.Support.V7.Widget.SearchView;

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryMainFragment : MvxFragment<InventoryViewModel> //, SwipeControllerActions
    {
        private InventoryMainAdapter inventoryMainAdapter;
        private RecyclerView inventoryMain_RecyclerView;
        private SearchView inventoryMain_SearchView;
        private Context Context;
        private AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;
        private bool isSwipeCalled;

        public InventoryMainFragment(Context context)
        {
            this.Context = context;
        }



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
        {
            try
            {
                await ViewModel.InventorySearchCommand(e.NewText);
                InventoryAdapterData();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }

        private async void GetProducts()
        {
            var vendors = await GetVendors();
            if (vendors)
            {
                ViewModel.ClearCommand();
                try
                {
                    await ViewModel.InventorySearchCommand("");
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException)
                    {
                        return;
                    }
                }
            }
            InventoryAdapterData();
        }

        private async Task<bool> GetVendors()
        {
            try
            {
                await ViewModel.GetVendorsCommand();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return false;
                }
            }
            return true;
        }

        private void InventoryAdapterData()
        {
            if (ViewModel.FilteredList != null && ViewModel.FilteredList.Count > 0)
            {
                inventoryMainAdapter = new InventoryMainAdapter(Context, ViewModel.FilteredList, ViewModel);
                var layoutManager = new LinearLayoutManager(Context);
                inventoryMain_RecyclerView.SetLayoutManager(layoutManager);
                inventoryMain_RecyclerView.SetAdapter(inventoryMainAdapter);
            }
            if (ViewModel.FilteredList != null && ViewModel.FilteredList.Count > 0 && !isSwipeCalled)
            {
                MySwipeHelper mySwipe = new MyImplementSwipeHelper(Context, inventoryMain_RecyclerView, 200, this.ViewModel);
                isSwipeCalled = true;
            }
        }

        private void InventoryAdapterDataAsync(RecyclerView recyclerView, InventoryViewModel viewModel)
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
                DeleteFromList(selectedItem, inventoryMain_RecyclerView, viewModel);
            });
            removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {

            });
            Builder.SetPositiveButton("Ok", okHandler);
            Builder.SetNegativeButton("Cancel", removePhotoHandler);
            Builder.Create();
            Builder.Show();
        }

        public async void DeleteFromList(InventoryDataModel selectedItem, RecyclerView recyclerView, InventoryViewModel viewModel)
        {
            RecyclerView recyclerView1;
            int index = viewModel.FilteredList.IndexOf(selectedItem);
            try
            {
                var response = await viewModel.DeleteProductCommand(index);
                if (response)
                {
                    await viewModel.InventorySearchCommand(" ");
                    InventoryAdapterDataAsync(recyclerView, viewModel);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }

        }
    }

        public class MyImplementSwipeHelper : MySwipeHelper
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
        }

    
}

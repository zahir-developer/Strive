using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Owner;
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryEditFragment : MvxFragment<InventoryEditViewModel>
    {
        private EditText item_code;
        private EditText item_name;
        private EditText item_description;
        private EditText item_quantity;
        private EditText item_type;
        private EditText item_location;
        private EditText item_cost;
        private EditText item_price;
        private EditText supplier_name;
        private ImageView edit_image;
        private Button cancelButton;
        private Button saveButton;
        //private InventoryMainFragment inventoryMainFragment;
        private Context context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            context = this.Context;
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.InventoryEdit_Fragment, null);
            this.ViewModel = new InventoryEditViewModel();
            item_code = rootView.FindViewById<EditText>(Resource.Id.item_code);
            item_name = rootView.FindViewById<EditText>(Resource.Id.item_name);
            item_description = rootView.FindViewById<EditText>(Resource.Id.item_description);
            item_quantity = rootView.FindViewById<EditText>(Resource.Id.item_quantity);
            item_type = rootView.FindViewById<EditText>(Resource.Id.item_type);
            item_location = rootView.FindViewById<EditText>(Resource.Id.item_location);
            item_cost = rootView.FindViewById<EditText>(Resource.Id.item_cost);
            item_price = rootView.FindViewById<EditText>(Resource.Id.item_price);
            edit_image = rootView.FindViewById<ImageView>(Resource.Id.edit_Image);
            supplier_name = rootView.FindViewById<EditText>(Resource.Id.supplier_name);
            //var supplier_contact = rootView.FindViewById<EditText>(Resource.Id.supplier_contact);
            //var supplier_fax = rootView.FindViewById<EditText>(Resource.Id.supplier_fax);
            //var supplier_address = rootView.FindViewById<EditText>(Resource.Id.supplier_address);
            //var supplier_email = rootView.FindViewById<EditText>(Resource.Id.supplier_email);

            cancelButton = rootView.FindViewById<Button>(Resource.Id.edit_cancel);
            saveButton = rootView.FindViewById<Button>(Resource.Id.edit_save);

            //inventoryMainFragment = new InventoryMainFragment(context);

            item_code.Text = OwnerTempData.ItemCode;
            item_name.Text = OwnerTempData.ItemName;
            item_description.Text = OwnerTempData.ItemDescription;
            item_quantity.Text = OwnerTempData.ItemQuantity;
            supplier_name.Text = OwnerTempData.SupplierName;
            //supplier_contact.Text = OwnerTempData.SupplierContact;
            //supplier_fax.Text = OwnerTempData.SupplierFax;
            //supplier_address.Text = OwnerTempData.SupplierAddress;
            //supplier_email.Text = OwnerTempData.SupplierEmail;

            cancelButton.Click += CancelButton_Click;
            saveButton.Click += SaveButton_Click;

            return rootView;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.ViewModel.SaveItems();
            var selected_MvxFragment = new InventoryMainFragment(context);
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var selected_MvxFragment = new InventoryMainFragment(context);
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }
    }
}
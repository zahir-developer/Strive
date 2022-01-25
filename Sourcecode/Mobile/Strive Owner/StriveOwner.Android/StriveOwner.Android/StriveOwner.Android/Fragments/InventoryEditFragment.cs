using System;

using Android.App;
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
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.InventoryEdit_Fragment, null);
            this.ViewModel = new InventoryEditViewModel();
            var item_code = rootView.FindViewById<EditText>(Resource.Id.item_code);
            var item_name = rootView.FindViewById<EditText>(Resource.Id.item_name);
            var item_description = rootView.FindViewById<EditText>(Resource.Id.item_description);
            var item_quantity = rootView.FindViewById<EditText>(Resource.Id.item_quantity);

            var supplier_name = rootView.FindViewById<EditText>(Resource.Id.supplier_name);
            var supplier_contact = rootView.FindViewById<EditText>(Resource.Id.supplier_contact);
            var supplier_fax = rootView.FindViewById<EditText>(Resource.Id.supplier_fax);
            var supplier_address = rootView.FindViewById<EditText>(Resource.Id.supplier_address);
            var supplier_email = rootView.FindViewById<EditText>(Resource.Id.supplier_email);

            var cancelButton = rootView.FindViewById<Button>(Resource.Id.edit_cancel);
            var saveButton = rootView.FindViewById<Button>(Resource.Id.edit_save);


            item_code.Text = OwnerTempData.ItemCode;
            item_name.Text = OwnerTempData.ItemName;
            item_description.Text = OwnerTempData.ItemDescription;
            item_quantity.Text = OwnerTempData.ItemQuantity;
            supplier_name.Text = OwnerTempData.SupplierName;
            supplier_contact.Text = OwnerTempData.SupplierContact;
            supplier_fax.Text = OwnerTempData.SupplierFax;
            supplier_address.Text = OwnerTempData.SupplierAddress;
            supplier_email.Text = OwnerTempData.SupplierEmail;

            //cancelButton.Click += CancelButton_Click;
            //saveButton.Click += SaveButton_Click;

            return rootView;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.ViewModel.SaveItems();
            var selected_MvxFragment = new DashboardHomeFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var selected_MvxFragment = new DashboardHomeFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.Owner;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Resources.Fragments;
using static Android.App.ActionBar;

namespace StriveOwner.Android.Adapter
{
    public class InventoryMainAdapter : RecyclerView.Adapter
    {

        Context context;
        ObservableCollection<InventoryDataModel> inventorylist = new ObservableCollection<InventoryDataModel>();
        public InventoryMainAdapterViewHolder inventoryViewHolder;
        private Dialog popupRequest;
        private Dialog popupMainInvetory;
        private InventoryViewModel invVM = new InventoryViewModel();
        private EditText quantity;
        private int index;

        public InventoryMainAdapter(Context context, ObservableCollection<InventoryDataModel> inventorylist)
        {
            this.context = context;
            this.inventorylist = inventorylist;
            GetData();

        }
        public async void GetData()
        {
            await invVM.GetProductsCommand();
            await invVM.GetVendorsCommand();
            await invVM.InventorySearchCommand(" ");
        }

        public override long GetItemId(int position)
        {
            return position;
        }
       
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            inventoryViewHolder = holder as InventoryMainAdapterViewHolder;
            inventoryViewHolder.productCode.Text = inventorylist[position].Product.ProductCode;
            OwnerTempData.ItemCode = inventorylist[position].Product.ProductCode;
            inventoryViewHolder.productName.Text = inventorylist[position].Product.ProductName;
            OwnerTempData.ItemName = inventorylist[position].Product.ProductName;
            inventoryViewHolder.productDescription.Text = inventorylist[position].Product.ProductDescription;
            OwnerTempData.ItemDescription = inventorylist[position].Product.ProductDescription;
            inventoryViewHolder.productViewMore.Click += ProductViewMore_Click;
            inventoryViewHolder.productViewMore.Tag = "Tag" + position;
            inventoryViewHolder.quantityInc.Tag = "Tag" + position;
            if(!inventoryViewHolder.quantityInc.HasOnClickListeners){
                inventoryViewHolder.quantityInc.Click += QuantityInc_Click;

            }
            if (!inventoryViewHolder.quantityDec.HasOnClickListeners)
            {
                inventoryViewHolder.quantityDec.Click += QuantityDec_Click;

            }
            inventoryViewHolder.quantityDec.Tag = "Tag" + position;
            inventoryViewHolder.quantityProds.Text = inventorylist[position].Product.Quantity.ToString();
            OwnerTempData.ItemQuantity = inventorylist[position].Product.Quantity.ToString();

        }

        private async void QuantityDec_Click(object sender, EventArgs e)
        {
            var objs = (TextView)sender;
            var tagsOBJ = objs.Tag.ToString().Split('g');
            var positions = int.Parse(tagsOBJ[1]);
            invVM.DecrementCommand(positions);
            inventorylist[positions] = invVM.FilteredList[positions];
            NotifyItemChanged(positions);

        }

        private async void QuantityInc_Click(object sender, EventArgs e)
        {
            var objs = (TextView)sender;
            var tagsOBJ = objs.Tag.ToString().Split('g');
            var positions = int.Parse(tagsOBJ[1]);
            invVM.IncrementCommand(positions);
            inventorylist[positions] = invVM.FilteredList[positions];
            NotifyItemChanged(positions);
        }

        private void ProductViewMore_Click(object sender, EventArgs e)
        {
            var objs = (TextView)sender;
            var tagsOBJ = objs.Tag.ToString().Split('g');
            var positions = int.Parse(tagsOBJ[1]);
            index = int.Parse(tagsOBJ[1]);
            popupMainInvetory = new Dialog(this.context);
            popupMainInvetory.SetContentView(Resource.Layout.SuppliersPopOut);
            popupMainInvetory.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupMainInvetory.Show();
            popupMainInvetory.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);
            var close = popupMainInvetory.FindViewById<ImageView>(Resource.Id.closepopup);
            var popout_Name = popupMainInvetory.FindViewById<TextView>(Resource.Id.popout_Name);
            var popout_Contact = popupMainInvetory.FindViewById<TextView>(Resource.Id.popout_Contact);
            var popout_FAX = popupMainInvetory.FindViewById<TextView>(Resource.Id.popout_FAX);
            var popout_Address = popupMainInvetory.FindViewById<TextView>(Resource.Id.popout_Address);
            var popout_Email = popupMainInvetory.FindViewById<TextView>(Resource.Id.popout_Email);
            var popout_Request = popupMainInvetory.FindViewById<Button>(Resource.Id.popout_Request); 
            var edit_Items = popupMainInvetory.FindViewById<TextView>(Resource.Id.edit_Items);
            popout_Name.Text = inventorylist[positions].Vendor.VendorName;
            OwnerTempData.SupplierName = inventorylist[positions].Vendor.VendorName;
            popout_Contact.Text = inventorylist[positions].Vendor.PhoneNumber;
            OwnerTempData.SupplierContact = inventorylist[positions].Vendor.PhoneNumber;
            popout_FAX.Text = inventorylist[positions].Vendor.Fax;
            OwnerTempData.SupplierFax = inventorylist[positions].Vendor.Fax;
            popout_Address.Text = inventorylist[positions].Vendor.Address1;
            OwnerTempData.SupplierAddress = inventorylist[positions].Vendor.Address1;
            popout_Email.Text = inventorylist[positions].Vendor.Email;
            OwnerTempData.SupplierEmail = inventorylist[positions].Vendor.Email;
            edit_Items.Click += Edit_Items_Click;
            close.Click += Close_Click;
            popout_Request.Click += Popout_Request_Click;
        }

        private void Popout_Request_Click(object sender, EventArgs e)
        {
            popupRequest = new Dialog(this.context);
            popupRequest.SetContentView(Resource.Layout.RequestPopout);
            popupRequest.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupRequest.Show();
            popupRequest.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);
            var close = popupRequest.FindViewById<ImageView>(Resource.Id.requestclosepopup);
            var confirm = popupRequest.FindViewById<Button>(Resource.Id.confirmrequest);
            quantity = popupRequest.FindViewById<EditText>(Resource.Id.quantityamount);
            confirm.Click += Confirm_Click;
            close.Click += Close_ClickRequest;
        }

        private async void Confirm_Click(object sender, EventArgs e)
        {
            popupRequest.Dismiss();
            popupMainInvetory.Hide();
            invVM.ProductRequestCommand(int.Parse(quantity.Text), index);
        }

        private void Close_ClickRequest(object sender, EventArgs e)
        {
            popupRequest.Dismiss();
            popupMainInvetory.Hide();
        }

        private void Edit_Items_Click(object sender, EventArgs e)
        {
            popupMainInvetory.Dismiss();
            popupMainInvetory.Hide();
            InventoryEditFragment inventoryEdit = new InventoryEditFragment();
            AppCompatActivity activity = (AppCompatActivity)context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, inventoryEdit).Commit();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            popupMainInvetory.Dismiss();
            popupMainInvetory.Hide();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.InventoryMain_ItemView, parent, false);
            return new InventoryMainAdapterViewHolder(itemView);
        }

        //Fill in cound here, currently 0
        public override int ItemCount
        {
            get
            {
                return inventorylist.Count;
            }
        }

    }

    public class InventoryMainAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView productName;
        public TextView productCode;
        public TextView productDescription;
        public TextView productViewMore;
        public Button quantityInc;
        public Button quantityDec;
        public TextView quantityProds;

        public InventoryMainAdapterViewHolder(View inventoryProd) : base(inventoryProd)
        {
            productName = inventoryProd.FindViewById<TextView>(Resource.Id.productname);
            productCode = inventoryProd.FindViewById<TextView>(Resource.Id.productcode);
            productDescription = inventoryProd.FindViewById<TextView>(Resource.Id.productdescription);
            productViewMore = inventoryProd.FindViewById<TextView>(Resource.Id.productviewmore);
            quantityInc = inventoryProd.FindViewById<Button>(Resource.Id.quantityInc);
            quantityDec = inventoryProd.FindViewById<Button>(Resource.Id.quantityDec);
            quantityProds = inventoryProd.FindViewById<TextView>(Resource.Id.quantityProds);
        }

    }
}
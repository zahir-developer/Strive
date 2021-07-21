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
using StriveOwner.Android.Resources.Fragments;
using static Android.App.ActionBar;

namespace StriveOwner.Android.Adapter
{
    public class InventoryMainAdapter : RecyclerView.Adapter
    {

        Context context;
        ObservableCollection<InventoryDataModel> inventorylist = new ObservableCollection<InventoryDataModel>();
        InventoryMainAdapterViewHolder inventoryViewHolder;
        private Dialog popupMainInvetory;
        public InventoryMainAdapter(Context context, ObservableCollection<InventoryDataModel> inventorylist)
        {
            this.context = context;
            this.inventorylist = inventorylist;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
       
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            inventoryViewHolder = holder as InventoryMainAdapterViewHolder;
            inventoryViewHolder.productCode.Text = inventorylist[position].Product.ProductCode;
            inventoryViewHolder.productName.Text = inventorylist[position].Product.ProductName;
            inventoryViewHolder.productDescription.Text = inventorylist[position].Product.ProductDescription;
            inventoryViewHolder.productViewMore.Click += ProductViewMore_Click;
            inventoryViewHolder.productViewMore.Tag = "Tag" + position;
        }

        private void ProductViewMore_Click(object sender, EventArgs e)
        {
            var objs = (TextView)sender;
            var tagsOBJ = objs.Tag.ToString().Split('g');
            var positions = int.Parse(tagsOBJ[1]);
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
            popout_Contact.Text = inventorylist[positions].Vendor.PhoneNumber;
            popout_FAX.Text = inventorylist[positions].Vendor.Fax;
            popout_Address.Text = inventorylist[positions].Vendor.Address1;
            popout_Email.Text = inventorylist[positions].Vendor.Email;
            edit_Items.Click += Edit_Items_Click;
            close.Click += Close_Click;
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

        public InventoryMainAdapterViewHolder(View inventoryProd) : base(inventoryProd)
        {
            productName = inventoryProd.FindViewById<TextView>(Resource.Id.productname);
            productCode = inventoryProd.FindViewById<TextView>(Resource.Id.productcode);
            productDescription = inventoryProd.FindViewById<TextView>(Resource.Id.productdescription);
            productViewMore = inventoryProd.FindViewById<TextView>(Resource.Id.productviewmore);
        }

    }
}
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace StriveOwner.Android.Adapter
{
    class InventoryEditImagePickerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<InventoryEditImagePickerAdapterClickEventArgs> ItemClick;
        public event EventHandler<InventoryEditImagePickerAdapterClickEventArgs> ItemLongClick;
        int[] items;

        public InventoryEditImagePickerAdapter(int[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.InventoryEditImagePicker_ItemView;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new InventoryEditImagePickerAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as InventoryEditImagePickerAdapterViewHolder;
            holder.ProductImages.SetImageResource(item);
        }

        public override int ItemCount => items.Length;

        void OnClick(InventoryEditImagePickerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(InventoryEditImagePickerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class InventoryEditImagePickerAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ProductImages { get; set; }


        public InventoryEditImagePickerAdapterViewHolder(View itemView, Action<InventoryEditImagePickerAdapterClickEventArgs> clickListener,
                            Action<InventoryEditImagePickerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            ProductImages = ItemView.FindViewById<ImageView>(Resource.Id.inventoryProductImages);
            itemView.Click += (sender, e) => clickListener(new InventoryEditImagePickerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new InventoryEditImagePickerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class InventoryEditImagePickerAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
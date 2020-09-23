using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace StriveCustomer.Android.Adapter
{
    public class VehicleRecyclerHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public IItemClickListener vehicleItemClickListener;
        public TextView vehicleReg;
        public VehicleRecyclerHolder(View itemVehicle) : base(itemVehicle)
        {
            vehicleReg = itemVehicle.FindViewById<TextView>(Resource.Id.regNumber);
            itemVehicle.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.vehicleItemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            vehicleItemClickListener.OnClick(view, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            return true;
        }
    }

    class VehicleDetailsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        public List<string> listData = new List<string>();
        private VehicleRecyclerHolder vehicleRecyclerHolder;
        public override int ItemCount
        {
            get
            {
                return listData.Count;
            }
        }
        public VehicleDetailsAdapter(Context context, List<string> data)
        {
            this.context = context;
            this.listData = data;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            vehicleRecyclerHolder = holder as VehicleRecyclerHolder;
            vehicleRecyclerHolder.vehicleReg.Text = "Reg"+position;
            vehicleRecyclerHolder.SetItemClickListener(this);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.VehicleInfoList, parent, false);
            return new VehicleRecyclerHolder(itemView);
        }
    }
}
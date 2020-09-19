using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.TimInventory;

namespace StriveCustomer.Android.Adapter
{
    class AdditionalServicesAdapter : BaseAdapter
    {

        Context context;
        List<ServiceDetail> services = new List<ServiceDetail>();
        public AdditionalServicesAdapter(Context context, List<ServiceDetail> services)
        {
            this.context = context;
            foreach (var data in services)
            {
                if (string.Equals(data.ServiceTypeName, "Additional Services"))
                {
                    this.services.Add(data);
                }
            }
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AdditionalServicesAdapterViewHolder holder = null;

            //if (view != null)
            //    holder = view.Tag as AdditionalServicesAdapterViewHolder;

            //if (holder == null)
            //{
            //    holder = new AdditionalServicesAdapterViewHolder();
            //    var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                
            //    view.Tag = holder;
            //}
            if(view == null)
            {
                holder = new AdditionalServicesAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.AdditionalServiceList,null);
                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as AdditionalServicesAdapterViewHolder;
            }
        
            view.FindViewById<TextView>(Resource.Id.additionalServiceName).Text = services[position].ServiceName;
            CheckBox additionalService = view.FindViewById<CheckBox>(Resource.Id.additionalServiceCheck);
            additionalService.SetTypeface(null,TypefaceStyle.Bold);
            additionalService.CheckedChange += AdditionalService_CheckedChange;

            return view;
        }

        private void AdditionalService_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
           
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return services.Count;
            }
        }

    }

    class AdditionalServicesAdapterViewHolder : Java.Lang.Object
    {
        private CheckBox additionalCheck;
        private TextView additionalServiceName;
    }
}
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
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using static Android.Widget.CompoundButton;

namespace StriveCustomer.Android.Adapter
{
    class AdditionalServicesAdapter : BaseAdapter, IOnCheckedChangeListener 
    {

        Context context;
        private List<ServiceDetail> services = new List<ServiceDetail>();
        private IItemClickListener itemClickListener;
        private Dictionary<int,string> checkedCheck = new Dictionary<int,string>();
        public AdditionalServicesAdapter(Context context, List<ServiceDetail> services)
        {
            this.context = context;
            if(MembershipDetails.selectedAdditionalServices == null || MembershipDetails.selectedAdditionalServices.Count == 0)
            {
                MembershipDetails.selectedAdditionalServices = new List<int>();
            }
            
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
            additionalService.Tag = "Check" + position;
            if(!checkedCheck.ContainsKey(services[position].ServiceId))
            {
                checkedCheck.Add(services[position].ServiceId, additionalService.Tag.ToString());
            }
            if(MembershipDetails.selectedAdditionalServices.Contains(services[position].ServiceId))
            {
                additionalService.Checked = true;
            }
            additionalService.SetOnCheckedChangeListener(this);
            return view;
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            foreach(var data in checkedCheck)
            {
                if(isChecked && string.Equals(data.Value.ToString(),buttonView.Tag.ToString()))
                {
                    MembershipDetails.selectedAdditionalServices.Add(data.Key);
                }
 
                if(!isChecked && string.Equals(data.Value.ToString(), buttonView.Tag.ToString()))
                {
                    MembershipDetails.selectedAdditionalServices.Remove(data.Key);
                }
            }
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
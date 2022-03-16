using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;

namespace StriveCustomer.Android.Adapter
{
    class AdditionalServicesAdapter : BaseAdapter, View.IOnClickListener
    {

        Context context;
        private ObservableCollection<AllServiceDetail> services = new ObservableCollection<AllServiceDetail>();
        private IItemClickListener itemClickListener;
        private Dictionary<int, string> checkedCheck = new Dictionary<int, string>();
        AdditionalServicesAdapterViewHolder holder;
        public AdditionalServicesAdapter(Context context, ObservableCollection<AllServiceDetail> services)
        {
            this.context = context;
            if (MembershipDetails.selectedAdditionalServices == null || MembershipDetails.selectedAdditionalServices.Count == 0)
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

            if (view == null)
            {
                holder = new AdditionalServicesAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.AdditionalServiceList, null);
                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as AdditionalServicesAdapterViewHolder;
            }

            holder.additionalServiceName = view.FindViewById<TextView>(Resource.Id.additionalServiceName);
            holder.additionalServiceName.Text = services[position].ServiceName.TrimEnd() + " - $" + services[position].Price + "/mo."; ;
            holder.linearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearlayout);
            holder.additionalCheck = view.FindViewById<CheckBox>(Resource.Id.additionalServiceCheck);
            holder.additionalCheck.SetTypeface(null, TypefaceStyle.Bold);
            holder.linearLayout.Tag = position;

            setServicesData(services[position], holder);
            holder.linearLayout.SetOnClickListener(this);
            holder.additionalCheck.Clickable = false;
            return view;
        }

        public void setServicesData(AllServiceDetail allServiceDetail, AdditionalServicesAdapterViewHolder holder)
        {
            string service = allServiceDetail.ServiceName.Replace(" ", "");//holder.additionalServiceName.Text.Replace(" ", "");

            if (MembershipDetails.selectedMembershipDetail.Services != null)
            {
                string[] selectedServices = MembershipDetails.selectedMembershipDetail.Services.Split(",");

                if (selectedServices.Any(x => x.Replace(" ", "") == service))
                {
                    holder.additionalCheck.Checked = true;
                    holder.linearLayout.SetBackgroundResource(Resource.Color.additionalServiceColor);

                }

                else
                {
                    holder.additionalCheck.Checked = false;
                    holder.linearLayout.SetBackgroundResource(Resource.Color.defaultLabelColor);

                }

            }
            if (MembershipDetails.selectedAdditionalServices.Count > 0)
            {
                if (MembershipDetails.selectedAdditionalServices.Any(x => x == allServiceDetail.ServiceId))
                {
                    holder.additionalCheck.Checked = true;
                }
            }

        }

        public void CheckedChanged(object sender, EventArgs e)
        {
            var compoundBtn = (CompoundButton)sender;
            int position = (int)compoundBtn.Tag;
            if (MembershipDetails.selectedAdditionalServices.Any(x => x == services[position].ServiceId))
            {
                foreach (var data in MembershipDetails.selectedAdditionalServices.ToList())
                {
                    if (data == services[position].ServiceId)
                    {
                        MembershipDetails.selectedAdditionalServices.Remove(services[position].ServiceId);
                    }
                }

            }
            else
            {
                MembershipDetails.selectedAdditionalServices.Add(services[position].ServiceId);
                holder.additionalCheck.Checked = true;
            }
            //if (MembershipDetails.selectedAdditionalServices.Count != 0)
            //{
            //    if (MembershipDetails.selectedAdditionalServices.Contains(services[position].ServiceId))
            //    {
            //        MembershipDetails.selectedAdditionalServices.Remove(services[position].ServiceId);
            //    }
            //    else
            //    {
            //        MembershipDetails.selectedAdditionalServices.Add(services[position].ServiceId);
            //    }
            //}
            //else
            //{
            //    MembershipDetails.selectedAdditionalServices.Add(services[position].ServiceId);
            //}


        }

        public void OnClick(View v)
        {
            int position = (int)v.Tag;
            if (MembershipDetails.selectedAdditionalServices.Any(x => x == services[position].ServiceId))
            {
                foreach (var data in MembershipDetails.selectedAdditionalServices.ToList())
                {
                    if (data == services[position].ServiceId)
                    {
                        MembershipDetails.selectedAdditionalServices.Remove(services[position].ServiceId);
                        holder.additionalCheck.Checked = false;

                    }
                }
            }
            else
            {
                MembershipDetails.selectedAdditionalServices.Add(services[position].ServiceId);
                holder.additionalCheck.Checked = true;
            }
            NotifyDataSetChanged();
        }

        //Fill in count here, currently 0
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
        public CheckBox additionalCheck;
        public TextView additionalServiceName;
        public LinearLayout linearLayout;
    }
}
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
using StriveEmployee.Android.Fragments;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerContactsRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button contact_Button;
        public TextView contactName_TextView;
        public MessengerContactsRecycleHolder(View contact) : base(contact)
        {
            contact_Button = contact.FindViewById<Button>(Resource.Id.contact_ImageView);
            contactName_TextView = contact.FindViewById<TextView>(Resource.Id.contactName_TextView);
        }

        public void OnClick(View v)
        {
            
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    } 


    public class MessengerContactsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerContactsRecycleHolder contactsRecycleHolder;
        private List<RecentContactsSampleData> contactsSampleDatas = new List<RecentContactsSampleData>();
        public MessengerContactsAdapter(Context context, List<RecentContactsSampleData> sampleData)
        {
            this.context = context;
            this.contactsSampleDatas = sampleData;
        }

        public override int ItemCount
        {
            get
            {
                return contactsSampleDatas.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            contactsRecycleHolder = holder as MessengerContactsRecycleHolder;
            var avatarInitials = contactsSampleDatas[position].ContactName.Split(" ");
            char[] firstInitial = avatarInitials[0].ToCharArray();
            char[] secondInitial = avatarInitials[1].ToCharArray();

            contactsRecycleHolder.contact_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
            contactsRecycleHolder.contactName_TextView.Text = contactsSampleDatas[position].ContactName;
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerContactItemView, parent, false);
            return new MessengerContactsRecycleHolder(itemView);
        }
    }
}
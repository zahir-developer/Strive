using System;
using System.Collections.Generic;
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
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using StriveEmployee.Android.Fragments;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerContactsRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button contact_Button;
        public TextView contactName_TextView;
        public IItemClickListener itemClickListener;
        public MessengerContactsRecycleHolder(View contact) : base(contact)
        {
            
            contact_Button = contact.FindViewById<Button>(Resource.Id.contact_ImageView);
            contactName_TextView = contact.FindViewById<TextView>(Resource.Id.contactName_TextView);
            contact.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }

        public void OnClick(View view)
        {
            itemClickListener.OnClick(view, AdapterPosition, false);
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
        private List<EmployeeList> contacts = new List<EmployeeList>();
        private char[] firstInitial;
        private char[] secondInitial;
        public MessengerContactsAdapter(Context context, List<EmployeeList> contacts)
        {
            this.context = context;
            this.contacts = contacts;
        }

        public override int ItemCount
        {
            get
            {
                return contacts.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            contactsRecycleHolder = holder as MessengerContactsRecycleHolder;
            if (!String.IsNullOrEmpty(contacts[position].FirstName))
            {
                firstInitial = contacts[position].FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(contacts[position].LastName))
            {
                secondInitial = contacts[position].LastName.ToCharArray();
            }
            if (firstInitial.Length != 0 || secondInitial.Length != 0)
            {
                contactsRecycleHolder.contact_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                contactsRecycleHolder.contactName_TextView.Text = contacts[position].FirstName + " " + contacts[position].LastName;
            }
            contactsRecycleHolder.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            AppCompatActivity activity = (AppCompatActivity)itemView.Context;
            MessengerPersonalChatFragment messengerPersonalChatFragment = new MessengerPersonalChatFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerPersonalChatFragment).Commit();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerContactItemView, parent, false);
            return new MessengerContactsRecycleHolder(itemView);
        }
    }
}
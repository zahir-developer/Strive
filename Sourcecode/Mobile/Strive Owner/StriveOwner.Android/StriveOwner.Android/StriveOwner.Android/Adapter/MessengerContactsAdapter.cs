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
using MvvmCross;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils.Employee;
using StriveOwner.Android.Fragments;
using StriveEmployee.Android.Listeners;

namespace StriveOwner.Android.Adapter
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


    public class MessengerContactsAdapter : RecyclerView.Adapter, IItemClickListener //, IFilterable
    {

        Context context;
        private MessengerContactsRecycleHolder contactsRecycleHolder;
        private List<Employee> contacts = new List<Employee>();
        private char[] firstInitial;
        private char[] secondInitial;
        public IMessengerService MessengerService = Mvx.IoCProvider.Resolve<IMessengerService>();
        public MessengerContactsAdapter(Context context, List<Employee> contacts)
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
            if (firstInitial != null && secondInitial != null)
            {
                contactsRecycleHolder.contact_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                contactsRecycleHolder.contactName_TextView.Text = contacts[position].FirstName + " " + contacts[position].LastName;
            }
            else if(firstInitial != null)
            {
                if(firstInitial.Length > 0)
                {
                    contactsRecycleHolder.contact_Button.Text = firstInitial.ElementAt(0).ToString() + firstInitial.ElementAt(1).ToString();
                }
            }
            else
            {
                if (secondInitial.Length > 0)
                {
                    contactsRecycleHolder.contact_Button.Text = secondInitial.ElementAt(0).ToString() + secondInitial.ElementAt(1).ToString();
                }
            }
            contactsRecycleHolder.SetItemClickListener(this);
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            MessengerTempData.resetChatData();
            MessengerTempData.GroupID = 0;


            MessengerTempData.IsGroup = false;

            MessengerTempData.RecipientName = MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(position).FirstName + " " + MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(position).LastName;
            MessengerTempData.GroupUniqueID = null;
            MessengerTempData.RecipientID = MessengerTempData.employeeList_Contact.EmployeeList.Employee.ElementAt(position).EmployeeId;

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
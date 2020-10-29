using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee;
using StriveEmployee.Android.Fragments;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerRecentContactsRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button recentContact_Button;
        public TextView recentContactName_TextView;
        public TextView recentContactLastText_TextView;
        public TextView recentContactMessageTime_TextView;
        public MessengerRecentContactsRecycleHolder(View recentContact) : base(recentContact)
        {

            recentContact_Button = recentContact.FindViewById<Button>(Resource.Id.recentContact_ImageView);
            recentContactName_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactName_TextView);
            recentContactLastText_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactLastText_TextView);
            recentContactMessageTime_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactMessageTime_TextView);

        }
        public void OnClick(View v)
        {
            
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    }
    public class MessengerRecentContactsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerRecentContactsRecycleHolder recentContactsRecycleHolder;
        private List<ChatEmployeeList> recentContacts = new List<ChatEmployeeList>();
        private char[] firstInitial;
        private char[] secondInitial;
        public MessengerRecentContactsAdapter(Context context, List<ChatEmployeeList> recentContacts)
        {
            this.context = context;
            this.recentContacts = recentContacts;
        }

        public override int ItemCount
        {
            get
            {
                return recentContacts.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            recentContactsRecycleHolder = holder as MessengerRecentContactsRecycleHolder;
            if(!String.IsNullOrEmpty(recentContacts[position].FirstName))
            {
                firstInitial = recentContacts[position].FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(recentContacts[position].LastName))
            {
               secondInitial = recentContacts[position].LastName.ToCharArray();
            }           

            if(firstInitial.Length != 0 || secondInitial.Length != 0)
            {
                recentContactsRecycleHolder.recentContact_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                recentContactsRecycleHolder.recentContactName_TextView.Text = recentContacts[position].FirstName + " " + recentContacts[position].LastName;
            }
           
            if(!String.IsNullOrEmpty(recentContacts[position].RecentChatMessage))
            {
                var lastMessage = recentContacts[position].RecentChatMessage.Split(",");
                DateTime localDateTime = DateTime.Parse(lastMessage[0]);
                var localDate = localDateTime.ToString().Split(" ");
                if(String.Equals(DateTime.Now.Date.ToString(), localDateTime.Date.ToString()))
                {
                    recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = localDateTime.ToString("HH:mm tt", CultureInfo.CurrentCulture);
                }
                else
                {
                    recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = localDate[0];
                }
                recentContactsRecycleHolder.recentContactLastText_TextView.Text = lastMessage[1];
            }
           // recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = recentContactsSampleDatas[position].MessageTime;
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerRecentContactItemView, parent, false);
            return new MessengerRecentContactsRecycleHolder(itemView);
        }
    }
}
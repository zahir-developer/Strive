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
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerCreateGroupRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button createGroup_Button;
        public CheckBox createGroup_CheckBox;
        public TextView createGroupName_TextView;
        public IItemClickListener itemClickListener;
        public MessengerCreateGroupRecycleHolder(View entry) : base(entry)
        {
            entry.SetOnClickListener(this);
            createGroup_Button = entry.FindViewById<Button>(Resource.Id.createGroup_ImageView);
            createGroupName_TextView = entry.FindViewById<TextView>(Resource.Id.createGroupName_TextView);
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

    public class MessengerCreateGroupAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerCreateGroupRecycleHolder messengerCreateGroup;
        private List<EmployeeList> contacts = new List<EmployeeList>();
        private char[] firstInitial;
        private char[] secondInitial;
        public MessengerCreateGroupAdapter(Context context, List<EmployeeList> contacts)
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
        public void OnClick(View itemView, int position, bool isLongClick)
        {
            messengerCreateGroup.createGroupName_TextView.SetCompoundDrawablesWithIntrinsicBounds(0, 0, Resource.Drawable.Check, 0);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            messengerCreateGroup = holder as MessengerCreateGroupRecycleHolder;
            messengerCreateGroup.SetItemClickListener(this);
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
                messengerCreateGroup.createGroup_Button.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                messengerCreateGroup.createGroupName_TextView.Text = contacts[position].FirstName + " " + contacts[position].LastName;
            }
        }
       
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerCreateGroupItemView, parent, false);
            return new MessengerCreateGroupRecycleHolder(itemView);
        }

       
    }    
}
using System;
using System.Collections.Generic;
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
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Utils.Employee;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerCreateGroupRecycleHolder : RecyclerView.ViewHolder
    {
        public Button createGroup_Button;
        public CheckBox createGroup_CheckBox;
        public TextView createGroupName_TextView;
        public IItemClickListener itemClickListener;
        public MessengerCreateGroupRecycleHolder(View entry) : base(entry)
        {
            createGroup_Button = entry.FindViewById<Button>(Resource.Id.createGroup_ImageView); 
            createGroup_CheckBox = entry.FindViewById<CheckBox>(Resource.Id.createGroup_Checkbox);
            createGroupName_TextView = entry.FindViewById<TextView>(Resource.Id.createGroupName_TextView);
        }
    }

    public class MessengerCreateGroupAdapter : RecyclerView.Adapter, View.IOnClickListener, View.IOnLongClickListener
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
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            messengerCreateGroup = holder as MessengerCreateGroupRecycleHolder;
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
                messengerCreateGroup.createGroup_CheckBox.Visibility = ViewStates.Gone;
            }

            messengerCreateGroup.ItemView.Tag = position;
            if(!MessengerTempData.ChatParticipants.ContainsKey(contacts[position].EmployeeId))
            {
                MessengerTempData.ChatParticipants.Add(contacts[position].EmployeeId, position);
            }
            
            messengerCreateGroup.ItemView.SetOnClickListener(this);
            
        }

        public void OnClick(View v)
        {
            var position = (int)v.Tag;
            if(MessengerTempData.SelectedParticipants.EmployeeList.Contains(contacts[position]))
            {
                var index = MessengerTempData.SelectedParticipants.EmployeeList.IndexOf(contacts[position]);
                MessengerTempData.SelectedParticipants.EmployeeList.RemoveAt(index);
                v.SetBackgroundColor(Color.Transparent);
            }
            else
            {
                MessengerTempData.SelectedParticipants.EmployeeList.Add(contacts[position]);
                v.SetBackgroundColor(Color.LightCyan);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerCreateGroupItemView, parent, false);
            return new MessengerCreateGroupRecycleHolder(itemView);
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    }    
}
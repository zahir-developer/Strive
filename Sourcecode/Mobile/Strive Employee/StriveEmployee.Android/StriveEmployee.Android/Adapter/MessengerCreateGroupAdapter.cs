using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerCreateGroupRecycleHolder : RecyclerView.ViewHolder
    {
        public Button createGroup_Button;
        public CheckBox createGroup_CheckBox;
        public TextView createGroupName_TextView;
        public LinearLayout createGroupEntry_LinearLayout;
        public IItemClickListener itemClickListener;

        public MessengerCreateGroupRecycleHolder(View entry) : base(entry)
        {
            createGroup_Button = entry.FindViewById<Button>(Resource.Id.createGroup_ImageView); 
            createGroup_CheckBox = entry.FindViewById<CheckBox>(Resource.Id.createGroup_Checkbox);
            createGroupName_TextView = entry.FindViewById<TextView>(Resource.Id.createGroupName_TextView);
            createGroupEntry_LinearLayout = entry.FindViewById<LinearLayout>(Resource.Id.createGroupEntry_LinearLayout);
        }
    }

    public class MessengerCreateGroupAdapter : RecyclerView.Adapter, View.IOnClickListener, View.IOnLongClickListener
    {

        Context context;
        private MessengerCreateGroupRecycleHolder messengerCreateGroup;
        private List<Employee> contacts = new List<Employee>();
        private char[] firstInitial;
        private char[] secondInitial;
        public List<Employee> sortedContacts { get; set; }
        private string queryWordString { get; set; }
        private MessengerCreateGroupViewModel messengerCreateGroupViewModel;
        public MessengerCreateGroupAdapter(Context context, List<Employee> contacts,MessengerCreateGroupViewModel viewModel)
        {
            this.context = context;
            this.contacts = contacts;
            messengerCreateGroupViewModel = viewModel;
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
                messengerCreateGroup.createGroup_CheckBox.Visibility = ViewStates.Visible;
            }

            messengerCreateGroup.ItemView.Tag = position;
            if(MessengerTempData.ChatParticipants != null)
            {
                if (!MessengerTempData.ChatParticipants.ContainsKey(contacts[position].EmployeeId))
                {
                    MessengerTempData.ChatParticipants.Add(contacts[position].EmployeeId, position);
                }
            }
            SelectedData(contacts[position], MessengerTempData.createGroup_Contact.EmployeeList.Employee);
            messengerCreateGroup.createGroup_CheckBox.Clickable = false;
            messengerCreateGroup.ItemView.SetOnClickListener(this);
            
        }
        private void SelectedData(Employee employee, List<Employee> Rowselections)
        {
            if (Rowselections.Any(x => x.EmployeeId == employee.EmployeeId))
            {
                //messengerCreateGroup.createGroupEntry_LinearLayout.SetBackgroundColor(Color.LightCyan);
                messengerCreateGroup.createGroup_CheckBox.Checked = true;
            }
            else
            {
                //messengerCreateGroup.createGroupEntry_LinearLayout.SetBackgroundColor(Color.Transparent);
                messengerCreateGroup.createGroup_CheckBox.Checked = false;
            }
        }
        public void OnClick(View v)
        {
            var position = (int)v.Tag;
            var temp = new chatUserGroup();
            temp.CommunicationId = contacts[position].CommunicationId;
            temp.createdBy = 0;
            temp.createdDate = (System.DateTime.Now).ToString("yyy/MM/dd HH:mm:ss").ToString();
            temp.isActive = true;
            temp.isDeleted = false;
            temp.userId = contacts[position].EmployeeId;
            temp.chatGroupUserId = 0;
            temp.chatGroupId = 0;
            if (MessengerTempData.createGroup_Contact.EmployeeList.Employee.Any(x=>x.EmployeeId == contacts[position].EmployeeId))
            {
                foreach(var data in MessengerTempData.createGroup_Contact.EmployeeList.Employee.ToList())
                {
                   if(data.EmployeeId == contacts[position].EmployeeId)
                    {
                        MessengerTempData.createGroup_Contact.EmployeeList.Employee.Remove(data);
                        MessengerFinalizeGroupViewModel.chatUserGroups.Remove(temp);

                    }
                }
                messengerCreateGroup.createGroup_CheckBox.Checked = false;
            }
            else
            {
                MessengerTempData.createGroup_Contact.EmployeeList.Employee.Add(contacts[position]);
                messengerCreateGroup.createGroup_CheckBox.Checked = true;
                if (contacts[position].EmployeeId != EmployeeTempData.EmployeeID)
                {
                    MessengerFinalizeGroupViewModel.chatUserGroups.Add(temp);

                }
            }
            NotifyDataSetChanged();

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
        public List<Employee> SearchContacts(List<Employee> contacts, string queryString)
        {
            sortedContacts = new List<Employee>();
            queryWordString = queryString.TrimEnd();

            var AllSmall = queryWordString.ToLower();

            foreach (var data in contacts)
            {
                var firstName = data.FirstName.ToLower();
                var lastName = data.LastName.ToLower();
                var fullName = data.FirstName.ToLower().TrimEnd() + " " + data.LastName.ToLower();
                if (firstName.Contains(AllSmall) || lastName.Contains(AllSmall) || fullName.Contains(AllSmall))
                {
                    sortedContacts.Add(data);
                }
            }

            return sortedContacts;

        }
    }    
}
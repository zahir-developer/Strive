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
using Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts;
using Strive.Core.Utils.Employee;
using StriveOwner.Android.Listeners;

namespace StriveOwner.Android.Adapter
{
     public class MessengerFinalizeGroupViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public IItemClickListener itemClickListener;
        public Button FinalizeGroupProfile_ImageButton;
        public ImageButton FinalizeGroupDelete_ImageButton;
        public TextView ParticipantName_TextView;
        public MessengerFinalizeGroupViewHolder(View itemView) : base(itemView)
        {
            FinalizeGroupProfile_ImageButton = itemView.FindViewById<Button>(Resource.Id.FinalizeGroupProfile_ImageButton);
            FinalizeGroupDelete_ImageButton = itemView.FindViewById<ImageButton>(Resource.Id.FinalizeGroupDelete_ImageButton);
            ParticipantName_TextView = itemView.FindViewById<TextView>(Resource.Id.FinalizeGroup_TextView);
            itemView.SetOnClickListener(this);
            FinalizeGroupDelete_ImageButton.Click += FinalizeGroupDelete_ImageButton_Click;
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        private void FinalizeGroupDelete_ImageButton_Click(object sender, EventArgs e)
        {
            MessengerTempData.ClickAction = 1;
            itemClickListener.OnClick(null, AdapterPosition, false);
        }

        public void OnClick(View v)
        {
            itemClickListener.OnClick(null, AdapterPosition, false);
        }
    }
    class MessengerFinalizeGroupAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerFinalizeGroupViewHolder MessengerFinalizeGroup;
        private char[] firstInitial;
        private char[] secondInitial;
        private EmployeeMessengerContacts selectedParticipants = new EmployeeMessengerContacts();
        public MessengerFinalizeGroupAdapter(Context context, EmployeeMessengerContacts selectedParticipants)
        {
            this.context = context;
            this.selectedParticipants.EmployeeList = new Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
            this.selectedParticipants.EmployeeList.Employee = new List<Employee>();
            this.selectedParticipants = selectedParticipants;
        }

        public override int ItemCount 
        {
            get
            {
                return this.selectedParticipants.EmployeeList.Employee.Count;
            }        
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MessengerFinalizeGroup = holder as MessengerFinalizeGroupViewHolder;

            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList.Employee[position].FirstName))
            {
                firstInitial = selectedParticipants.EmployeeList.Employee[position].FirstName.ToCharArray();
            }
            else
            {
                firstInitial = null;

            }
            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList.Employee[position].LastName))
            {
                secondInitial = selectedParticipants.EmployeeList.Employee[position].LastName.ToCharArray();
            }
            else
            {
                secondInitial = null;
            }
            if (firstInitial != null && secondInitial != null)
            {
                MessengerFinalizeGroup.FinalizeGroupProfile_ImageButton.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                MessengerFinalizeGroup.ParticipantName_TextView.Text = selectedParticipants.EmployeeList.Employee[position].FirstName + " " + selectedParticipants.EmployeeList.Employee[position].LastName;
            }
            else if(firstInitial != null)
            {
                MessengerFinalizeGroup.FinalizeGroupProfile_ImageButton.Text = firstInitial.ElementAt(0).ToString();
                MessengerFinalizeGroup.ParticipantName_TextView.Text = selectedParticipants.EmployeeList.Employee[position].FirstName;
            }
            else
            {
                MessengerFinalizeGroup.FinalizeGroupProfile_ImageButton.Text = secondInitial.ElementAt(0).ToString();
                MessengerFinalizeGroup.ParticipantName_TextView.Text = selectedParticipants.EmployeeList.Employee[position].LastName;
            }
            MessengerFinalizeGroup.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            if(MessengerTempData.ClickAction == 1)
            {
                MessengerTempData.SelectedParticipants.EmployeeList.Employee.RemoveAt(position);
                NotifyItemRemoved(position);
                NotifyItemRangeChanged(position, selectedParticipants.EmployeeList.Employee.Count);
                MessengerTempData.ClickAction = 0;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerFinalizeGroupItemView, parent, false);
            return new MessengerFinalizeGroupViewHolder(itemView);
        }
    }

    
}
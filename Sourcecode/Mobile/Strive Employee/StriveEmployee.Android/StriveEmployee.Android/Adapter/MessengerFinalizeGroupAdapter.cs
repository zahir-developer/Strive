﻿using System;
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
using Strive.Core.Utils.Employee;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
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
        private EmployeeLists selectedParticipants = new EmployeeLists();
        public MessengerFinalizeGroupAdapter(Context context, EmployeeLists selectedParticipants)
        {
            this.context = context;
            this.selectedParticipants.EmployeeList = new List<EmployeeList>();
            this.selectedParticipants = selectedParticipants;
        }

        public override int ItemCount 
        {
            get
            {
                return this.selectedParticipants.EmployeeList.Count;
            }        
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MessengerFinalizeGroup = holder as MessengerFinalizeGroupViewHolder;

            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList[position].FirstName))
            {
                firstInitial = selectedParticipants.EmployeeList[position].FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList[position].LastName))
            {
                secondInitial = selectedParticipants.EmployeeList[position].LastName.ToCharArray();
            }
            if (firstInitial.Length != 0 || secondInitial.Length != 0)
            {
                MessengerFinalizeGroup.FinalizeGroupProfile_ImageButton.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                MessengerFinalizeGroup.ParticipantName_TextView.Text = selectedParticipants.EmployeeList[position].FirstName + " " + selectedParticipants.EmployeeList[position].LastName;
            }
            MessengerFinalizeGroup.SetItemClickListener(this);
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            if(MessengerTempData.ClickAction == 1)
            {
                MessengerTempData.SelectedParticipants.EmployeeList.RemoveAt(position);
                NotifyItemRemoved(position);
                NotifyItemRangeChanged(position, selectedParticipants.EmployeeList.Count);
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
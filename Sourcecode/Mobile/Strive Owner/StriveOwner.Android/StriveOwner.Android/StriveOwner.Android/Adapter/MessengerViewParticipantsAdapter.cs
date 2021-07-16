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
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee;
using StriveOwner.Android.Listeners;

namespace StriveOwner.Android.Adapter
{
    public class MessengerViewParticipantsViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public IItemClickListener itemClickListener;
        public Button ParticipantsProfile_ImageButton;
        public ImageButton ParticipantsGroupDelete_ImageButton;
        public TextView ParticipantName_TextView;

        public MessengerViewParticipantsViewHolder(View itemView) : base(itemView)
        {
            ParticipantsProfile_ImageButton = itemView.FindViewById<Button>(Resource.Id.ParticipantGroupProfile_ImageButton);
            ParticipantsGroupDelete_ImageButton = itemView.FindViewById<ImageButton>(Resource.Id.ParticipantGroupDelete_ImageButton);
            ParticipantName_TextView = itemView.FindViewById<TextView>(Resource.Id.ParticipantGroup_TextView);
            
            itemView.SetOnClickListener(this);
            ParticipantsGroupDelete_ImageButton.Click += FinalizeGroupDelete_ImageButton_Click;
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
    class MessengerViewParticipantsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerViewParticipantsViewHolder MessengerParticipantGroup;
        private char[] firstInitial;
        private char[] secondInitial;
        private EmployeeLists selectedParticipants = new EmployeeLists();
        public MessengerViewParticipantsAdapter(Context context, EmployeeLists selectedParticipants)
        {
            this.context = context;
            this.selectedParticipants.EmployeeList = new EmployeeList();
            this.selectedParticipants = selectedParticipants;
        }

        public override int ItemCount
        {
            get
            {
                return this.selectedParticipants.EmployeeList.ChatEmployeeList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MessengerParticipantGroup = holder as MessengerViewParticipantsViewHolder;
            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList.ChatEmployeeList[position].FirstName))
            {
                firstInitial = selectedParticipants.EmployeeList.ChatEmployeeList[position].FirstName.ToCharArray();
            }
            if (!String.IsNullOrEmpty(selectedParticipants.EmployeeList.ChatEmployeeList[position].LastName))
            {
                secondInitial = selectedParticipants.EmployeeList.ChatEmployeeList[position].LastName.ToCharArray();
            }
            if (firstInitial.Length != 0 || secondInitial.Length != 0)
            {
                MessengerParticipantGroup.ParticipantsProfile_ImageButton.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
                MessengerParticipantGroup.ParticipantName_TextView.Text = selectedParticipants.EmployeeList.ChatEmployeeList[position].FirstName + " " + selectedParticipants.EmployeeList.ChatEmployeeList[position].LastName;
            }

            MessengerParticipantGroup.SetItemClickListener(this);
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            if (MessengerTempData.ClickAction == 1)
            {
                //MessengerTempData.ExistingParticipants.ChatEmployeeList.RemoveAt(position);
                var model =  new MessengerViewParticipantsViewModel();
                model.GroupUserId = selectedParticipants.EmployeeList.ChatEmployeeList[position].ChatGroupUserId;
                await model.DeleteGroupUser();
                selectedParticipants.EmployeeList.ChatEmployeeList.RemoveAt(position);
                NotifyItemRemoved(position);
                NotifyItemRangeChanged(position, selectedParticipants.EmployeeList.ChatEmployeeList.Count);
                MessengerTempData.ClickAction = 0;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerViewParticiapantsItemView, parent, false);
            return new MessengerViewParticipantsViewHolder(itemView);
        }
    }

}
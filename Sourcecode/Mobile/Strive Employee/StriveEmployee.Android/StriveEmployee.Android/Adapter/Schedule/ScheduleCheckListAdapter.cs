using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckList;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using System;

namespace StriveEmployee.Android.Adapter.Schedule
{
    class ScheduleCheckListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemLongClick;
        Checklist Checklist;
        int CheckListPosition;
        public ScheduleCheckListAdapter(Checklist checkList)
        {
            Checklist = checkList;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.Schedule_ChecklistItemView;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new ScheduleCheckListAdapterViewHolder(itemView, OnClick, OnLongClick);            
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Checklist;
            CheckListPosition = position;
            // Replace the contents of the view with that element
            var holder = viewHolder as ScheduleCheckListAdapterViewHolder;
            holder.taskName.Text = item.ChecklistNotification[position].Name;
            holder.checklist_Time.Text = item.ChecklistNotification[position].NotificationTime;
            holder.completedTask_Checkbox.CheckedChange += CompletedTask_Checkbox_CheckedChange;
        }

        private void CompletedTask_Checkbox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (e.IsChecked)
            {
                var item = Checklist;
                var checklist = new checklistupdate();
                checklist.CheckListEmployeeId = item.ChecklistNotification[CheckListPosition].CheckListEmployeeId;
                //checklist.CheckListId = item.ChecklistNotification[indexPath.Row].CheckListId;
                checklist.IsCompleted = true;
                checklist.NotificationDate = "2022-02-16T08:02:01.028Z";
                checklist.UserId = EmployeeTempData.EmployeeID;
                ScheduleCheckListViewModel.SelectedChecklist.Add(checklist);
            }
            //else 
            //{
            //    var item = Checklist;
            //    var checklist = new checklistupdate();
            //    checklist.CheckListEmployeeId = item.ChecklistNotification[CheckListPosition].CheckListEmployeeId;
            //    //checklist.CheckListId = item.ChecklistNotification[indexPath.Row].CheckListId;
            //    checklist.IsCompleted = false;
            //    checklist.NotificationDate = "2022-02-16T08:02:01.028Z";
            //    checklist.UserId = EmployeeTempData.EmployeeID;
            //    ScheduleCheckListViewModel.SelectedChecklist.Add(checklist);
            //}

        }

        public override int ItemCount => Checklist.ChecklistNotification.Count;

        void OnClick(ScheduleCheckListAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScheduleCheckListAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class ScheduleCheckListAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView taskName { get; set; }
        public TextView checklist_Time { get; set; }
        public CheckBox completedTask_Checkbox { get; set; }
        public ScheduleCheckListAdapterViewHolder(View itemView, Action<ScheduleCheckListAdapterClickEventArgs> clickListener,
                            Action<ScheduleCheckListAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            taskName = itemView.FindViewById<TextView>(Resource.Id.taskName);
            checklist_Time = itemView.FindViewById<TextView>(Resource.Id.checklist_time);
            completedTask_Checkbox = itemView.FindViewById<CheckBox>(Resource.Id.completed_CheckBox);
            itemView.Click += (sender, e) => clickListener(new ScheduleCheckListAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new ScheduleCheckListAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class ScheduleCheckListAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
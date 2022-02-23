using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.CheckList;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using System;
using System.Linq;

namespace StriveEmployee.Android.Adapter.Schedule
{
    class ScheduleCheckListAdapter : RecyclerView.Adapter, View.IOnClickListener
    {
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemLongClick;
        Checklist Checklist = new Checklist();
        int CheckListPosition;
        ScheduleCheckListAdapterViewHolder holder;
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
           
            // Replace the contents of the view with that element
            holder = viewHolder as ScheduleCheckListAdapterViewHolder;
            holder.taskName.Text = item.ChecklistNotification[position].Name;
            holder.checklist_Time.Text = "Time: " + item.ChecklistNotification[position].NotificationTime;
            holder.checkList_ItemView.Tag = position;
            holder.checkList_ItemView.SetOnClickListener(this);
            holder.completedTask_Checkbox.Clickable = false;
            
            if (ScheduleCheckListViewModel.SelectedChecklist.Count != 0)
            {
                var test = ScheduleCheckListViewModel.SelectedChecklist.Find(x => x.CheckListEmployeeId == item.ChecklistNotification[position].CheckListEmployeeId);
                if (test != null)
                {
                    holder.completedTask_Checkbox.Checked = true;

                }
                else
                {
                    holder.completedTask_Checkbox.Checked = false;
                }
            }
        }       

        public override int ItemCount => Checklist.ChecklistNotification.Count;

        void OnClick(ScheduleCheckListAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScheduleCheckListAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        public void OnClick(View v)
        {
            var item = Checklist; 
            int CheckListPosition = (int)v.Tag;
            if (ScheduleCheckListViewModel.SelectedChecklist.Any(x => x.CheckListEmployeeId == item.ChecklistNotification[CheckListPosition].CheckListEmployeeId))
            {                
                var element = ScheduleCheckListViewModel.SelectedChecklist.Find(x => x.CheckListEmployeeId == item.ChecklistNotification[CheckListPosition].CheckListEmployeeId);
                ScheduleCheckListViewModel.SelectedChecklist.Remove(element);
                holder.completedTask_Checkbox.Checked = false;             

            }
            else
            {
                
                var checklist = new checklistupdate();
                checklist.CheckListEmployeeId = item.ChecklistNotification[CheckListPosition].CheckListEmployeeId;
                //checklist.CheckListId = item.ChecklistNotification[indexPath.Row].CheckListId;
                checklist.IsCompleted = true;
                checklist.NotificationDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff") + "Z";
                checklist.UserId = EmployeeTempData.EmployeeID;
                //checklist.CheckListNotificationId = item.ChecklistNotification[CheckListPosition].ChecklistNotificationId;
                ScheduleCheckListViewModel.SelectedChecklist.Add(checklist);
                holder.completedTask_Checkbox.Checked = true;
            }
            NotifyDataSetChanged();
        }
    }

    public class ScheduleCheckListAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView taskName { get; set; }
        public TextView checklist_Time { get; set; }
        public CheckBox completedTask_Checkbox { get; set; }
        public RelativeLayout checkList_ItemView { get; set; }
        public ScheduleCheckListAdapterViewHolder(View itemView, Action<ScheduleCheckListAdapterClickEventArgs> clickListener,
                            Action<ScheduleCheckListAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            taskName = itemView.FindViewById<TextView>(Resource.Id.taskName);
            checklist_Time = itemView.FindViewById<TextView>(Resource.Id.checklist_time);
            completedTask_Checkbox = itemView.FindViewById<CheckBox>(Resource.Id.completed_CheckBox);
            checkList_ItemView = itemView.FindViewById<RelativeLayout>(Resource.Id.checkList_ItemView);
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



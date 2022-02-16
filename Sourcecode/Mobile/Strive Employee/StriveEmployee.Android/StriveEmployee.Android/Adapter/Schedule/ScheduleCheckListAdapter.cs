using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace StriveEmployee.Android.Adapter.Schedule
{
    class ScheduleCheckListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScheduleCheckListAdapterClickEventArgs> ItemLongClick;
        string[] items;

        public ScheduleCheckListAdapter(string[] data)
        {
            items = data;
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
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as ScheduleCheckListAdapterViewHolder;
            holder.taskName.Text = items[position-1];
            holder.checklist_Time.Text = items[position];
            holder.finishButton.Click += FinishButton_Click;
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
        }

        public override int ItemCount => items.Length;

        void OnClick(ScheduleCheckListAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScheduleCheckListAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class ScheduleCheckListAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView taskName { get; set; }
        public TextView checklist_Time { get; set; }
        public Button finishButton { get; set; }
        public ScheduleCheckListAdapterViewHolder(View itemView, Action<ScheduleCheckListAdapterClickEventArgs> clickListener,
                            Action<ScheduleCheckListAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            taskName = itemView.FindViewById<TextView>(Resource.Id.taskName);
            checklist_Time = itemView.FindViewById<TextView>(Resource.Id.checklist_time);
            finishButton = itemView.FindViewById<Button>(Resource.Id.finishButton);
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
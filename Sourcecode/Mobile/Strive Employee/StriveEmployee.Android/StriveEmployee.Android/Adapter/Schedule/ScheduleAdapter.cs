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
using Strive.Core.Models.Owner;

namespace StriveEmployee.Android.Adapter.Schedule
{
    public class ScheduleAdapter : RecyclerView.Adapter
    {

        Context context;
        ScheduleDetail scheduleDetail;
        public ScheduleAdapterViewHolder scheduleViewHolder;

        public ScheduleAdapter(Context context, ScheduleDetail scheduleDetail)
        {
            this.context = context;
            this.scheduleDetail = new ScheduleDetail();
            this.scheduleDetail.ScheduleDetailViewModel = new List<ScheduleDetailViewModel>();
            this.scheduleDetail.ScheduleEmployeeViewModel = new ScheduleEmployeeViewModel();
            this.scheduleDetail.ScheduleHoursViewModel = new ScheduleHoursViewModel();
            this.scheduleDetail = scheduleDetail;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            scheduleViewHolder = holder as ScheduleAdapterViewHolder;
            scheduleViewHolder.locationNameSchedule.Text = scheduleDetail.ScheduleDetailViewModel[position].LocationName;
            scheduleViewHolder.locationstartandend.Text = "Start Time ="+ scheduleDetail.ScheduleDetailViewModel[position].StartTime + "\t\t\tEnd Time =" + scheduleDetail.ScheduleDetailViewModel[position].EndTime;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.ScheduleItemView, parent, false);
            return new ScheduleAdapterViewHolder(itemView);
        }

        //Fill in cound here, currently 0
        public override int ItemCount
        {
            get
            {
                return scheduleDetail.ScheduleDetailViewModel.Count;
            }
        }
    }

    public class ScheduleAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView locationNameSchedule;
        public TextView locationstartandend;
        public ScheduleAdapterViewHolder(View schedule) : base(schedule)
        {
            locationNameSchedule = schedule.FindViewById<Button>(Resource.Id.locationNameSchedule);
            locationstartandend = schedule.FindViewById<TextView>(Resource.Id.locationstartandend);
        }
    }
}
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
        private List<ScheduleDetailViewModel> scheduleDetailViewModels = new List<ScheduleDetailViewModel>();

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
            scheduleViewHolder.locationNameSchedule.Text = scheduleDetailViewModels[position].LocationName;
            if(!string.IsNullOrEmpty(scheduleDetailViewModels[position].StartTime) && !string.IsNullOrEmpty(scheduleDetailViewModels[position].EndTime))
            {
                var start = scheduleDetailViewModels[position].StartTime.Split('T');
                var startTime = start[1].Split(":");
                var end = scheduleDetailViewModels[position].StartTime.Split('T');
                var endTime = end[1].Split(":");
                if(int.Parse(startTime[0]) > 12)
                {
                    scheduleViewHolder.locationstartandend.Text = "Start Time =" + startTime[0] + "PM"+ "\tEnd Time =" + endTime[0] + "PM";
                }
                else
                {
                    scheduleViewHolder.locationstartandend.Text = "Start Time =" + startTime[0] + "AM" + "\tEnd Time =" + endTime[0] + "PM";
                }
            }
           
            
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
                return scheduleDetailViewModels.Count;
            }
        }
    }

    public class ScheduleAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView locationNameSchedule;
        public TextView locationstartandend;
        public ScheduleAdapterViewHolder(View schedule) : base(schedule)
        {
            locationNameSchedule = schedule.FindViewById<TextView>(Resource.Id.locationNameSchedule);
            locationstartandend = schedule.FindViewById<TextView>(Resource.Id.locationstartandend);
        }
    }
}
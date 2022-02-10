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
    public class ScheduleDetailerAdapter : RecyclerView.Adapter
    {

        Context context;
        ScheduleDetail scheduleDetail;
        public ScheduleDetailerAdapterViewHolder scheduleViewHolder;
        private List<ScheduleDetailViewModel> scheduleDetailViewModels = new List<ScheduleDetailViewModel>();
        private string getStartTime;
        private string getEndTime;
        public ScheduleDetailerAdapter(Context context, ScheduleDetail scheduleDetail)
        {
            this.context = context;
            this.scheduleDetail = new ScheduleDetail();
            this.scheduleDetail.ScheduleDetailViewModel = new List<ScheduleDetailViewModel>();
            this.scheduleDetail.ScheduleEmployeeViewModel = new ScheduleEmployeeViewModel();
            this.scheduleDetail.ScheduleHoursViewModel = new ScheduleHoursViewModel();
            
            this.scheduleDetail = scheduleDetail;
            scheduleDetailViewModels = scheduleDetail.ScheduleDetailViewModel;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            scheduleViewHolder = holder as ScheduleDetailerAdapterViewHolder;
            scheduleViewHolder.locationNameSchedule.Text = scheduleDetailViewModels[position].LocationName;
            if(!string.IsNullOrEmpty(scheduleDetailViewModels[position].StartTime) && !string.IsNullOrEmpty(scheduleDetailViewModels[position].EndTime))
            {
                var start = scheduleDetailViewModels[position].StartTime.Split('T');
                var startTime = start[1].Split(":");
                var end = scheduleDetailViewModels[position].EndTime.Split('T');
                var endTime = end[1].Split(":");
                if(int.Parse(startTime[0]) >= 12)
                {
                   getStartTime = startTime[0] + ":" + startTime[1] + " " + "PM";
                    if (int.Parse(endTime[0]) >= 12)
                    {
                        getEndTime = endTime[0] + ":" + endTime[1] + " " + "PM";
                    }
                    //scheduleViewHolder.locationstartandend.Text = "Start Time = " + startTime[0] + ":" + startTime[1] + " " + "PM"+ "\nEnd Time = " + endTime[0] + ":" + endTime[1] + " " + "PM";
                }
                
                else 
                {
                    getStartTime = startTime[0] + ":" + startTime[1] + " " + "AM";
                    if (int.Parse(endTime[0]) < 12)
                    {
                        getEndTime = endTime[0] + ":" + endTime[1] + " " + "AM";
                    }
                    else if (int.Parse(endTime[0]) >= 12)
                    {
                        getEndTime = endTime[0] + ":" + endTime[1] + " " + "PM";
                    }
                    //scheduleViewHolder.locationstartandend.Text = "Start Time = " + startTime[0] + ":" + startTime[1] + " " + "AM" + "\nEnd Time = " + endTime[0] + ":" + endTime[1] + " " + "PM";
                }
                scheduleViewHolder.locationstartandend.Text = "Start Time = " + getStartTime + "\nEnd Time = " + getEndTime;

            }


        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.ScheduleDetailer_ItemView, parent, false);
            return new ScheduleDetailerAdapterViewHolder(itemView);
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

    public class ScheduleDetailerAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView locationNameSchedule;
        public TextView locationstartandend;
        public ScheduleDetailerAdapterViewHolder(View schedule) : base(schedule)
        {
            locationNameSchedule = schedule.FindViewById<TextView>(Resource.Id.locationNameScheduleDetail);
            locationstartandend = schedule.FindViewById<TextView>(Resource.Id.locationstartandendDetail);
        }
    }
}
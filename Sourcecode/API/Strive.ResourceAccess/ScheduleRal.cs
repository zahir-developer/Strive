using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Schedule;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class ScheduleRal : RalBase
    {
        public ScheduleRal(ITenantHelper tenant) : base(tenant) { }

        public bool SaveSchedule(ScheduleDto schedule)
        {
            return dbRepo.InsertPc(schedule, "ScheduleId");
        }
        public bool DeleteSchedule(int scheduleId)

        {
            _prm.Add("@tblScheduleId", scheduleId);
            db.Save(EnumSP.Schedule.uspDeleteSchedule.ToString(), _prm);
            return true;
        }
        public ScheduleListViewModel GetSchedule(ScheduleDetailDto scheduleDetail)
        {
            _prm.Add("@ScheduledStartDate", Convert.ToDateTime(scheduleDetail.startDate));
            _prm.Add("@ScheduledEndDate", Convert.ToDateTime(scheduleDetail.endDate));
            _prm.Add("@LocationId", scheduleDetail.locationId);
            _prm.Add("@EmployeeId", scheduleDetail.EmployeeId);
            return db.FetchMultiResult<ScheduleListViewModel>(EnumSP.Schedule.USPGETSCHEDULE.ToString(), _prm);
        }
        public List<ScheduleViewModel> GetScheduleById(int scheduleId)
        {
            _prm.Add("ScheduleId", scheduleId);
            var result = db.Fetch<ScheduleViewModel>(EnumSP.Schedule.USPGETSCHEDULEBYSCHEDULEID.ToString(), _prm);
            return result;
        }
        public ScheduleForcastedListViewModel GetScheduleAndForcasted (ScheduleDetailDto scheduleDetail)
        {
            
           _prm.Add("@ScheduledStartDate", scheduleDetail.startDate);
            _prm.Add("@ScheduledEndDate", scheduleDetail.endDate);
            _prm.Add("@LocationId", scheduleDetail.locationId);
            return db.FetchMultiResult<ScheduleForcastedListViewModel>(EnumSP.Schedule.USPGETSCHEDULEANDFORCASTED.ToString(), _prm);
        }
    }
}

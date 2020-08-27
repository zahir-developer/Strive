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
            db.Save(SPEnum.uspDeleteSchedule.ToString(), _prm);
            return true;
        }
        public List<ScheduleViewModel> GetSchedule(ScheduleDetailDto scheduleDetail)
        {
            _prm.Add("@ScheduledStartDate", Convert.ToDateTime(scheduleDetail.startDate));
            _prm.Add("@ScheduledEndDate", Convert.ToDateTime(scheduleDetail.endDate));
            _prm.Add("@LocationId", scheduleDetail.locationId);
            return db.Fetch<ScheduleViewModel>(SPEnum.USPGETSCHEDULE.ToString(), _prm);
        }
        public List<ScheduleViewModel> GetScheduleById(int scheduleId)
        {
            _prm.Add("ScheduleId", scheduleId);
            var result = db.Fetch<ScheduleViewModel>(SPEnum.USPGETSCHEDULEBYSCHEDULEID.ToString(), _prm);
            return result;
        }
    }
}

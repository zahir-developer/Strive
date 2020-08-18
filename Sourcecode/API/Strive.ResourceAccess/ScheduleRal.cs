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
        public List<ScheduleViewModel> GetSchedule(DateTime? StartDate, DateTime? EndDate)
        {
            _prm.Add("@ScheduledStartDate", Convert.ToDateTime(StartDate));
            _prm.Add("@ScheduledendDate", Convert.ToDateTime(EndDate));
            return db.Fetch<ScheduleViewModel>(SPEnum.uspGetSchedule.ToString(), _prm);
        }
        public List<ScheduleViewModel> GetScheduleById(int scheduleId)
        {
            _prm.Add("ScheduleId", scheduleId);
            var result = db.Fetch<ScheduleViewModel>(SPEnum.uspGetSchedule.ToString(), _prm);
            return result;
        }
    }
}

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
        public EmployeeScheduleDto SaveSchedule(ScheduleDto schedule)
        {
            EmployeeScheduleDto esto = new EmployeeScheduleDto();
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@ScheduleId", schedule.ScheduleId);
            dynParams.Add("@EmployeeId", schedule.EmployeeId);
            dynParams.Add("@LocationId", schedule.LocationId);
            dynParams.Add("@RoleId", schedule.RoleId);
            dynParams.Add("@ScheduledDate", schedule.ScheduledDate);
            dynParams.Add("@StartTime", schedule.StartTime);
            dynParams.Add("@EndTime", schedule.EndTime);
            dynParams.Add("@ScheduleType", schedule.ScheduleType);
            dynParams.Add("@Comments", schedule.Comments);
            dynParams.Add("@IsActive", schedule.IsActive);
            var result = db.Fetch<EmployeeScheduleDto>(SPEnum.USPSAVESCHEDULE.ToString(), dynParams);
            foreach (var item in result)
            {
                esto.Result = item.Result;
            }
            if (esto.Result != null)
            {
                CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVESCHEDULE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
                db.Save(cmd);
            }

            return esto;
                
                
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
            var result =  db.Fetch<ScheduleViewModel>(SPEnum.uspGetSchedule.ToString(), _prm);
            return result;
        }
    }
}

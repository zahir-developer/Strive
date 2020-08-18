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
        //public EmployeeScheduleDto SaveSchedule(List<ScheduleDto> schedule)
        //{
        //    EmployeeScheduleDto esto = new EmployeeScheduleDto();
        //    DynamicParameters dynParams = new DynamicParameters();
        //    foreach (var item in schedule)
        //    {
        //        dynParams.Add("@ScheduleId", item.ScheduleId);
        //        dynParams.Add("@EmployeeId", item.EmployeeId);
        //        dynParams.Add("@LocationId", item.LocationId);
        //        dynParams.Add("@RoleId", item.RoleId);
        //        dynParams.Add("@ScheduledDate", item.ScheduledDate);
        //        dynParams.Add("@StartTime", item.StartTime);
        //        dynParams.Add("@EndTime", item.EndTime);
        //        dynParams.Add("@ScheduleType", item.ScheduleType);
        //        dynParams.Add("@Comments", item.Comments);
        //        dynParams.Add("@IsActive", item.IsActive);

        //        var result = db.Fetch<EmployeeScheduleDto>(SPEnum.USPSAVESCHEDULE.ToString(), dynParams);
        //        foreach (var res in result)
        //        {
        //            esto.Result = res.Result;
        //        }
        //        if (esto.Result != null)
        //        {
        //            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVESCHEDULE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
        //            db.Save(cmd);
        //        }
        //    }


        //    return esto;


        //}
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

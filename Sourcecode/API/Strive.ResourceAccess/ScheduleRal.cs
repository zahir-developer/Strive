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
        public BoolResultDto SaveSchedule(ScheduleDto schedule)
        {
                BoolResultDto brdto = new BoolResultDto();
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
                var result = db.Fetch<BoolResultDto>(SPEnum.USPSAVESCHEDULE.ToString(), dynParams);
                foreach (var item in result)
                {
                    brdto.Result =  item.Result;
                }
                if(brdto.Result != null)
                {
                    CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVESCHEDULE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
                    db.Save(cmd);
                }
                return brdto;
                
                
        }
        public bool DeleteSchedule(int scheduleId)

        {
            _prm.Add("tblScheduleId", scheduleId);
            db.Save(SPEnum.uspDeleteSchedule.ToString(), _prm);
            return true;
        }
        public List<ScheduleViewModel> GetSchedule()
        {
            return db.Fetch<ScheduleViewModel>(SPEnum.uspGetSchedule.ToString(), null);
        }
        public List<ScheduleViewModel> GetScheduleById(int scheduleId)
        {
            _prm.Add("ScheduleId", scheduleId);
            var result =  db.Fetch<ScheduleViewModel>(SPEnum.uspGetSchedule.ToString(), _prm);
            return result;
        }
    }
}

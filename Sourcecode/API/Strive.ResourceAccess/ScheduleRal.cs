using Strive.BusinessEntities;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class ScheduleRal : RalBase
    {
        public ScheduleRal(ITenantHelper tenant) : base(tenant) { }
        public bool SaveSchedule(ScheduleModel schedule)
        {
            try
            {
                return dbRepo.Save<BusinessEntities.Model.ScheduleModel>(schedule, "ScheduleId") > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateSchedule(ScheduleModel schedule)
        {
            try
            {
                return dbRepo.UpdatePc(schedule);
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

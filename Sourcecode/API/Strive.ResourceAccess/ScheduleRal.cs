using Strive.BusinessEntities.Model;
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
                //return dbRepo.UpdatePc(location);
                return dbRepo.UpdatePc(schedule);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

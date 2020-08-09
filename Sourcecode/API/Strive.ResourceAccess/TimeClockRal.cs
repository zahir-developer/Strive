using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.TimeClock;
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
    public class TimeClockRal : RalBase
    {
        public TimeClockRal(ITenantHelper tenant) : base(tenant) { }


        public bool SaveTimeClock(TimeClockModel timeClock)
        {
            try
            {
                return dbRepo.Save<BusinessEntities.Model.TimeClockModel>(timeClock, "TimeClockId") > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<TimeClockViewModel> GetTimeClock(TimeClockDto timeClock)
        {
            _prm.Add("LocationId", timeClock.LocationId);
            _prm.Add("EmployeeId", timeClock.EmployeeId);
            _prm.Add("RoleId", timeClock.RoleId);
            _prm.Add("Date", timeClock.Date);

            return db.Fetch<TimeClockViewModel>(SPEnum.USPCLOCKTIMEDETAILS.ToString(), _prm);
        }

    }
}

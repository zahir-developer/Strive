using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.TimeClock;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class TimeClockRal : RalBase
    {
        public TimeClockRal(ITenantHelper tenant) : base(tenant) { }


        public bool SaveTimeClock(TimeClockListModel timeClock)
        {
            try
            {
                return dbRepo.SaveAll(timeClock, "TimeClockId");
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

            return db.Fetch<TimeClockViewModel>(EnumSP.ClockTime.USPGETTIMECLOCK.ToString(), _prm);
        }

        public TimeClockDetailViewModel TimeClockEmployeeDetails(TimeClockEmployeeDetailDto timeClockEmployeeDetailDto)
        {
            _prm.Add("LocationId", timeClockEmployeeDetailDto.LocationId);
            _prm.Add("StartDate", timeClockEmployeeDetailDto.StartDate);
            _prm.Add("EndDate", timeClockEmployeeDetailDto.EndDate);

            return db.FetchMultiResult<TimeClockDetailViewModel>(EnumSP.ClockTime.USPGETTIMECLOCKEMPLOYEEDETAILS.ToString(), _prm);
        }

        public TimeClockWeekDetailViewModel TimeClockWeekDetails(TimeClockWeekDetailDto timeClockWeekDetailDto)
        {
            _prm.Add("EmployeeId", timeClockWeekDetailDto.EmployeeId);
            _prm.Add("LocationId", timeClockWeekDetailDto.LocationId);
            _prm.Add("StartDate", timeClockWeekDetailDto.StartDate);
            _prm.Add("EndDate", timeClockWeekDetailDto.EndDate);

            return db.FetchMultiResult<TimeClockWeekDetailViewModel>(EnumSP.ClockTime.USPGETTIMECLOCKWEEKDETAILS.ToString(), _prm);
        }

        public bool DeleteTimeClockEmployee(TimeClockDeleteDto timeClockDeleteDto)
        {
            _prm.Add("EmployeeId", timeClockDeleteDto.EmployeeId);
            _prm.Add("LocationId", timeClockDeleteDto.LocationId);

            CommandDefinition cmd = new CommandDefinition(EnumSP.ClockTime.USPDELETETIMECLOCKEMPLOYEE.ToString(), _prm, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public TimeClockEmployeeHourViewModel TimeClockEmployeeHourDetail(TimeClockLocationDto timeClockLocationDto)
        {
            _prm.Add("locationId", timeClockLocationDto.LocationId);
            _prm.Add("date", timeClockLocationDto.Date);
            _prm.Add("currentDate", timeClockLocationDto.CurrentDate);

            return db.FetchMultiResult<TimeClockEmployeeHourViewModel>(EnumSP.ClockTime.USPGETTIMECLOCKEMPLOYEEHOURDETAIL.ToString(), _prm);
        }

        public EmployeeThresholdHourViewModel GetEmployeeWeeklyTimeClockHour (TimeClockWeekDetailDto timeClockWeekDetailDto)
        {
            _prm.Add("EmployeeId", timeClockWeekDetailDto.EmployeeId);
            _prm.Add("LocationId", timeClockWeekDetailDto.LocationId);
            _prm.Add("StartDate", timeClockWeekDetailDto.StartDate);
            _prm.Add("EndDate", timeClockWeekDetailDto.EndDate);

            return db.FetchSingle<EmployeeThresholdHourViewModel>(EnumSP.ClockTime.USPGETEMPLOYEEWEEKLYTIMECLOCKHOUR.ToString(), _prm);


        }

        public List<TimeClockEmployeeDetailViewModel> GetClockedInDetailer(TimeClockLocationDto timeclock)
        {
            _prm.Add("DateTime", timeclock.Date);
            _prm.Add("LocationId", timeclock.LocationId);
            return db.Fetch<TimeClockEmployeeDetailViewModel>(EnumSP.ClockTime.USPGETCLOCKEDINDETAILER.ToString(), _prm);
        }                                                     
        
    }
}

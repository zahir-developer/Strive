using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.TimeClock;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.TimeClock
{
    public class TimeClockBpl : Strivebase, ITimeClockBpl
    {
        public TimeClockBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper)
        {
        }

        public Result GetTimeClock(TimeClockDto timeClock)
        {

            return ResultWrap(new TimeClockRal(_tenant).GetTimeClock, timeClock, "TimeClock");
        }

        public Result SaveTimeClock(Strive.BusinessEntities.Model.TimeClockModel timeClock)
        {
            foreach (var oTimeClock in timeClock.TimeClock.TimeClock)
            {
                if (oTimeClock.InTime != null)
                    oTimeClock.InTime = new DateTimeOffset(oTimeClock.InTime.Value.DateTime, new TimeSpan(0, 0, 0));
                if (oTimeClock.OutTime != null)
                    oTimeClock.OutTime = new DateTimeOffset(oTimeClock.OutTime.Value.DateTime, new TimeSpan(0, 0, 0));
            }
            var result = new TimeClockRal(_tenant).SaveTimeClock(timeClock.TimeClock);

            if (timeClock.TimeClockWeekDetailDto != null)
            {
                var thresholdHours = new TimeClockRal(_tenant).GetEmployeeWeeklyTimeClockHour(timeClock.TimeClockWeekDetailDto);
                if (thresholdHours != null)
                {
                    if (thresholdHours.LocationWorkHourThreshold != 0 && thresholdHours.LocationWorkHourThreshold < thresholdHours.EmployeeWorkMinutes.toDecimal())
                    {
                        var emailId = new CommonRal(_tenant).GetEmailIdByRole(thresholdHours.LocationId.ToString(), timeClock.TimeClockWeekDetailDto.StartDate, timeClock.TimeClockWeekDetailDto.EndDate);
                        foreach (var item in emailId)
                        {
                            string subject = "Threshold Work Limit";
                            Dictionary<string, string> keyValues = new Dictionary<string, string>();
                            keyValues.Add("{{Manager/Operator}}", item.FirstName);
                            keyValues.Add("{{employeeName}}", timeClock.TimeClockWeekDetailDto.EmployeeName);
                            keyValues.Add("{{totalHours}}", thresholdHours.EmployeeWorkMinutes.ToString());
                            keyValues.Add("{{locationName}}", thresholdHours.LocationName);
                            new CommonBpl(_cache, _tenant).SendEmail(HtmlTemplate.EmployeeThreshold, item.Email, keyValues, subject);
                        }
                    }
                }
            }

            return ResultWrap(result, "Status");
        }

        public Result TimeClockEmployeeDetails(TimeClockEmployeeDetailDto timeClockEmployeeDetailDto)
        {
            return ResultWrap(new TimeClockRal(_tenant).TimeClockEmployeeDetails, timeClockEmployeeDetailDto, "Result");
        }

        public Result TimeClockWeekDetails(TimeClockWeekDetailDto timeClockWeekDetailDto)
        {
            return ResultWrap(new TimeClockRal(_tenant).TimeClockWeekDetails, timeClockWeekDetailDto, "Result");
        }

        public Result DeleteTimeClockEmployee(TimeClockDeleteDto timeClockDeleteDto)
        {
            return ResultWrap(new TimeClockRal(_tenant).DeleteTimeClockEmployee, timeClockDeleteDto, "Result");
        }

        public Result TimeClockEmployeeHourDetail(TimeClockLocationDto timeClockLocationDto)
        {
            return ResultWrap(new TimeClockRal(_tenant).TimeClockEmployeeHourDetail, timeClockLocationDto, "Result");
        }

        public Result GetClockedInDetailer(TimeClockLocationDto timeclock)
        {
            return ResultWrap(new TimeClockRal(_tenant).GetClockedInDetailer, timeclock, "result");
        }
    }
}

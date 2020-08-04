using Strive.BusinessEntities.TimeClock;
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


        public bool SaveTimeClock(TimeClock clockTime)
        {
            try
            {
                if (clockTime.id == 0)
                    return db.Insert<TimeClock>(clockTime) > 0;
                else
                    return db.Update<TimeClock>(clockTime);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public TimeClock GetTimeClock(int userId, DateTime dateTime)
        {
            var allTimeClock = db.GetAll<TimeClock>();

            var clockTime = allTimeClock.FirstOrDefault(s => s.UserId == userId && s.EventDate.Date == dateTime.Date);

            return clockTime;
        }

    }
}

using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Details
{
    public class DetailsBpl : Strivebase, IDetailsBpl
    {
        public DetailsBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result GetDetailsById(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).GetDetailsById, id, "DetailsForDetailId");
        }
        public Result AddDetails(DetailsDto details)
        {
            return ResultWrap(new DetailsRal(_tenant).AddDetails, details, "Status");
        }

        public Result UpdateDetails(DetailsDto details)
        {
            return ResultWrap(new DetailsRal(_tenant).UpdateDetails, details, "Status");
        }
        public Result GetAllBayById(int id)
        {
            return ResultWrap(new DetailsRal(_tenant).GetAllBayById, id, "BayDetailsForLocationId");
        }
        public Result GetScheduleDetailsByDate(DateTime date)
        {
            return ResultWrap(new DetailsRal(_tenant).GetScheduleDetailsByDate, date, "ScheduleDetailsForDate");
        }
        public Result GetJobType()
        {
            return ResultWrap(new DetailsRal(_tenant).GetJobType, "GetJobType");
        }
    }
}

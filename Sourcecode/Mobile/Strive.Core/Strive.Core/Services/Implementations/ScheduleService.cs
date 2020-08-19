using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();
        public async Task<CustomerPastSchedule> GetPastSchedules()
        {
            return await _restClient.MakeApiCall<CustomerPastSchedule>(ApiUtils.URL_GET_PAST_SCHEDULE, HttpMethod.Get);
        }
    }
}

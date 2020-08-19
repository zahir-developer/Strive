using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.Services.Interfaces
{
    interface IScheduleService
    {
        Task<CustomerPastSchedule> GetPastSchedules();
    }
}

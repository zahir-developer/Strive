using Strive.Core.Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.Services.Interfaces
{
    public interface IMessengerService
    {
        Task<EmployeeLists> GetRecentContacts(int employeeId);
    }
}

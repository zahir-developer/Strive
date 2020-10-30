using Strive.Core.Models.Employee;
using Strive.Core.Models.Employee.Messenger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using employeeLists = Strive.Core.Models.Employee.Messenger.MessengerContacts.EmployeeLists;

namespace Strive.Core.Services.Interfaces
{
    public interface IMessengerService
    {
        Task<EmployeeLists> GetRecentContacts(int employeeId);
        Task<employeeLists> GetContacts(string contactName);
    }
}

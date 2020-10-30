using MvvmCross;
using Strive.Core.Models.Employee;
using Strive.Core.Models.Employee.Messenger;
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
    public class MessengerService : IMessengerService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public async Task<Models.Employee.Messenger.MessengerContacts.EmployeeLists> GetContacts(string contactName)
        {
            return await _restClient.MakeApiCall<Models.Employee.Messenger.MessengerContacts.EmployeeLists>(string.Format(ApiUtils.URL_MESSENGER_CONTACTS, contactName), HttpMethod.Get);
        }

        public async Task<Models.Employee.Messenger.EmployeeLists> GetRecentContacts(int employeeId)
        {
            return await _restClient.MakeApiCall<Models.Employee.Messenger.EmployeeLists>(string.Format(ApiUtils.URL_MESSENGER_RECENT_CONTACTS, employeeId), HttpMethod.Get);
        }
    }
}

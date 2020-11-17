
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerViewParticipantsViewModel : BaseViewModel
    {

        #region Properties

        public EmployeeLists EmployeeList { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetParticipants()
        {
            var result = await MessengerService.GetParticipants(MessengerTempData.GroupID);
            if(result == null)
            {
                EmployeeList = null;
            }
            else
            {
                EmployeeList = new EmployeeLists();
                EmployeeList.EmployeeList = new EmployeeList();
                EmployeeList.EmployeeList.ChatEmployeeList = new List<ChatEmployeeList>();
                EmployeeList = result;

                MessengerTempData.ExistingParticipants = new EmployeeList();
                MessengerTempData.ExistingParticipants.ChatEmployeeList = new List<ChatEmployeeList>();
                MessengerTempData.ExistingParticipants = result.EmployeeList;
            }
        }

        #endregion Commands

    }
}

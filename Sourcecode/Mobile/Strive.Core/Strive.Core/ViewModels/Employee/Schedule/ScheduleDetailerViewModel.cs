using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.Detailer;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Employee.Schedule
{
    public class ScheduleDetailerViewModel : BaseViewModel
    {       
        public status DetailerList { get; set; }  
       
        #region Commands
        
        public async Task GetDetailer(int employeeid,string jobdate)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            DetailerList = await AdminService.GetEmployeeDetailer(employeeid,jobdate);            
            _userDialog.HideLoading();
        }      

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

        #endregion Commands
    }
}

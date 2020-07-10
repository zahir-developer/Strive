using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using MvvmCross.Plugin.Messenger;
using MvvmCross;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockInViewModel : BaseViewModel
    {
        public ObservableCollection<EmployeeRole> RolesList { get; set; }

       

        public ClockInViewModel()
        {
            InitList();
        }

        private void InitList()
        {
            RolesList = new ObservableCollection<EmployeeRole>
            {
                new EmployeeRole("Washer",""),
                new EmployeeRole("Detailer",""),
                new EmployeeRole("Runner",""),
                new EmployeeRole("Cashier",""),
                new EmployeeRole("Manager",""),
                new EmployeeRole("Greet Bay",""),
                new EmployeeRole("Finish Bay",""),
                new EmployeeRole("Unknown","")
            };
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockedInCommand()
        {
            await _navigationService.Navigate<ClockedInViewModel>();
        }
    }
}

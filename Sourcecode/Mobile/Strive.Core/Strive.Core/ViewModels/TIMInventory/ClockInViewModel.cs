using System;
using System.Collections.ObjectModel;
using Strive.Core.Models.TimInventory;

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
    }
}

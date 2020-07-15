using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using MvvmCross.Plugin.Messenger;
using MvvmCross;
using Strive.Core.Utils;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockInViewModel : BaseViewModel
    {
        private List<EmployeeRole> _RolesList;

        public List<EmployeeRole> RolesList {
            get
            {
                return _RolesList;
            }
            private set
            {
                SetProperty(ref _RolesList, value);
            }
        }

        private EmployeeRole FirstSelectedRole { get; set; }

        private EmployeeRole SecondSelectedRole { get; set; }

        public ClockInViewModel()
        {
            InitList();
        }

        private void InitList()
        {
            _RolesList = new List<EmployeeRole>
            {
                new EmployeeRole("Washer","icon-washer",0,UIImage.FromBundle("icon-washer")),
                new EmployeeRole("Detailer","icon-cashier",1,UIImage.FromBundle("icon-cashier")),
                new EmployeeRole("Runner","icon-detailer",2,UIImage.FromBundle("icon-detailer")),
                new EmployeeRole("Cashier","icon-finish-bay",3,UIImage.FromBundle("icon-finish-bay")),
                new EmployeeRole("Manager","icon-greetbay",4,UIImage.FromBundle("icon-greetbay")),
                new EmployeeRole("Greet Bay","icon-manager",5,UIImage.FromBundle("icon-manager")),
                new EmployeeRole("Finish Bay","icon-runner",6,UIImage.FromBundle("icon-runner")),
                new EmployeeRole("Unknown","icon-unknown",7,UIImage.FromBundle("icon-unknown"))
            };
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockedInCommand()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<ClockedInViewModel>();
        }

        public void RoleDecisionCommand(int index)
        {
            if (FirstSelectedRole == null)
            {
                FirstSelectedRole = RolesList[index];
                RoleSelectedCommand(FirstSelectedRole);
            }
            else
            {
                SecondSelectedRole = RolesList[index];
                if(FirstSelectedRole == SecondSelectedRole)
                {
                    RoleDeSelectedCommand(FirstSelectedRole);
                    FirstSelectedRole = null;
                    SecondSelectedRole = null;
                }
                else
                {
                    RoleDeSelectedCommand(FirstSelectedRole);
                    RoleSelectedCommand(SecondSelectedRole);
                    FirstSelectedRole = SecondSelectedRole;
                    SecondSelectedRole = null;
                }
            }
           
            RaiseAllPropertiesChanged();
        }

        void RoleSelectedCommand(EmployeeRole role)
        {
            string imagepath = role.ImageUri;
            imagepath += "-hover";
            role.image = UIImage.FromBundle(imagepath);
        }

        public void RoleDeSelectedCommand(EmployeeRole role)
        {
            string imagepath = role.ImageUri;
            role.image = UIImage.FromBundle(imagepath);
        }
    }
}

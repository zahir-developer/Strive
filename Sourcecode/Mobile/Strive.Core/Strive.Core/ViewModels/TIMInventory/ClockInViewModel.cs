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

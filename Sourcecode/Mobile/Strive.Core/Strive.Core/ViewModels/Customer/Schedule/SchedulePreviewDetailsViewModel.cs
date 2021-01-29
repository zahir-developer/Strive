using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class SchedulePreviewDetailsViewModel : BaseViewModel
    {

        public async Task BookNow()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

           

            _userDialog.HideLoading();
        }

    }
}

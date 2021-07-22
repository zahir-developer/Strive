using Acr.UserDialogs;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Owner
{
    public class InventoryEditViewModel : BaseViewModel
    {

        public void SaveItems()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            _userDialog.Toast("Save Successful");
            _userDialog.HideLoading();
        }
    }
}

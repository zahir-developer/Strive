using System;
using System.Threading.Tasks;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ChooseImageViewModel : BaseViewModel
    {
        public ChooseImageViewModel()
        {
        }

        public async Task NavigateCameraCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 2, ""));
        }

        public async Task NavigatePhotoLibraryCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 3, ""));
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}

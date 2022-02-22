using System;
using System.Threading.Tasks;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Owner
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

        public async Task NavigateIconViewCommand()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<IconsViewModel>();
        }

        public async Task NavigateBackCommand()
        {
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 0, ""));
            await _navigationService.Close(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Owner
{
    public class IconsViewModel : BaseViewModel
    {

        public List<string> IconList;

        public string SelectedIcon;

        public IconsViewModel()
        {
            IconList = new List<string>()
            {
                "Bottle","Broom","Can-Blue","Can-Orange","Can-Red",
                "Can","Cap","Chips","Coffee-Red","Glass","Milk","Shampoo-new",
                "Soap","Trainers","Water","Wiper","coffee","coffeeCup","icon-artboard",
                "shampoo"
            };
        }

        public void SelectedIconCommand(int index)
        {
            SelectedIcon = IconList[index];
            SaveCommand();
        }

        public async Task NavigateBackCommand()
        {
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 0, ""));
            await _navigationService.Close(this);
        }

        public async Task SaveCommand()
        {
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 4, SelectedIcon));
            await _navigationService.Close(this);
        }
    }
}

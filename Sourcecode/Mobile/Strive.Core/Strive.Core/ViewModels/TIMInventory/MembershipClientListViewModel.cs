using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class MembershipClientListViewModel : BaseViewModel
    {

        public ObservableCollection<string> FilteredList { get; set; } = new ObservableCollection<string>();
        public MembershipClientListViewModel()
        {
            for(int i =0; i< 10; i++)
            {
                FilteredList.Add("Name" + i);
            }
            RaiseAllPropertiesChanged();
        }

        public void ClientSearchCommand(string SearchText)
        {
            FilteredList = new ObservableCollection<string>(FilteredList.
                Where(s => s.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            RaiseAllPropertiesChanged();
        }
    }
}

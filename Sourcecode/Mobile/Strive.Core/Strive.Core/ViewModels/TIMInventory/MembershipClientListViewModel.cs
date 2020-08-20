using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class MembershipClientListViewModel : BaseViewModel
    {

        public ObservableCollection<ClientDetail> FilteredList { get; set; } = new ObservableCollection<ClientDetail>();

        private ObservableCollection<ClientDetail> ClientList = new ObservableCollection<ClientDetail>();


        public MembershipClientListViewModel()
        {
            RaiseAllPropertiesChanged();
        }

        public void ClientSearchCommand(string SearchText)
        {
            FilteredList = new ObservableCollection<ClientDetail>(ClientList.
                Where(s => s.FirstName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            RaiseAllPropertiesChanged();
        }

        public async Task GetAllClientsCommand()
        {
            //_userDialog.Loading(Strings.Loading);
            var Clients = await AdminService.GetAllClient();
            foreach(var client in Clients.Client)
            {
                ClientList.Add(client);
            }
            _userDialog.HideLoading();
        }

        public void ClearCommand()
        {
            FilteredList.Clear();
            ClientList.Clear();
        }

        public async void NavigateToDetailCommand(ClientDetail client)
        {
            await _navigationService.Navigate<MembershipClientDetailViewModel>();
        }
    }
}

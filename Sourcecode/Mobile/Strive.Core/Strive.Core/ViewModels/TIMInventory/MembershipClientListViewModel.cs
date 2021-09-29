using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory.Membership;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class MembershipClientListViewModel : BaseViewModel
    {

        public List<ClientInfo> FilteredList { get; set; } = new List<ClientInfo>();

        private ObservableCollection<ClientInfo> ClientList = new ObservableCollection<ClientInfo>();

        public ObservableCollection<ClientViewModel> Clients { get; set; } = new ObservableCollection<ClientViewModel>();


        public MembershipClientListViewModel()
        {
            RaiseAllPropertiesChanged();
        }

        public async Task ClientSearchCommand(string SearchText)
        {           
            if(FilteredList.Count <= 0 || SearchText != "")
            {
                _userDialog.ShowLoading("Fetching Clients");
                var result = await AdminService.SearchClient(SearchText);
                if (result != null)
                {
                    FilteredList = result.ClientSearch;
                }
                //FilteredList = new ObservableCollection<ClientInfo>(ClientList.
                //    Where(s => s.FirstName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
                await RaiseAllPropertiesChanged();
            }            
        }

        public async Task GetAllClientsCommand(String query)
        {
            _userDialog.ShowLoading(Strings.Loading);            
            var clientReq = PrepareClientReuest(query);
            var result = await AdminService.GetAllClient(clientReq);
            if (result != null)
            {
                Clients.Clear();
                if (result.Client.Count.Count != 0)
                {
                    foreach (var item in result.Client.clientViewModel)
                    {
                        Clients.Add(item);
                    }
                }                
            }
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
            //if(Clients != null)
            //{
            //    foreach (var client in Clients.Client)
            //    {
            //        ClientList.Add(client);
            //    }
            //}  
        }

        private ClientRequest PrepareClientReuest(string searchQuery)
        {
            ClientRequest client = new ClientRequest()
            {
                locationId = EmployeeData.selectedLocationId,
                pageNo = 1,
                pageSize = 100,
                query = searchQuery,
                sortOrder = "ASC",
                sortBy = "FirstName",
                status = true
            };
            return client;
        }

        public void ClearCommand()
        {
            FilteredList.Clear();
            ClientList.Clear();
            Clients.Clear();
        }

        public async void NavigateToDetailCommand(ClientViewModel client)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var clientDetail = await AdminService.GetClientDetail(client.ClientId);
            if(clientDetail == null)
            {
                return;
            }
            MembershipData.SelectedClient = clientDetail.Status[0];
            await _navigationService.Navigate<MembershipClientDetailViewModel>();
        }

        public async void AddProductCommand()
        {
            await _navigationService.Navigate<SelectMembershipViewModel>();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}

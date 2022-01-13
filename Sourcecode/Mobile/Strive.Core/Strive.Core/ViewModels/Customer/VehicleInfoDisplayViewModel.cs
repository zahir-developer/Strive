using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleInfoDisplayViewModel : BaseViewModel
    {
        #region Properties

        public VehicleList selectedVehicleInfo { get; set; }
        public ClientVehicleRootView clientVehicleRoot { get; set; }

        public CustomerCompleteDetails clientVehicleDetail { get; set; }
        public bool noData { get; set; }
        public bool isData { get; set; }
        public CardDetailsResponse response { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails;
        #endregion Properties


        #region Commands 

        public async Task GetSelectedVehicleInfo()
        {
            _userDialog.ShowLoading();

            selectedVehicleInfo = new VehicleList();
            selectedVehicleInfo.Status = new List<VehicleDetail>();
            foreach (var data in CustomerVehiclesInformation.vehiclesList.Status)
            {
                if(data.VehicleId == CustomerVehiclesInformation.selectedVehicleInfo)
                {
                    selectedVehicleInfo.Status.Add(data);
                }
            }
            CustomerVehiclesInformation.completeVehicleDetails = new ClientVehicleRootView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails = new VehicleMembershipDetailsView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicle = new ClientVehicleView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicleMembership = new ClientVehicleMembershipView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicleMembershipService = new List<ClientVehicleMembershipServiceView>();

            CustomerVehiclesInformation.completeVehicleDetails = await AdminService.GetVehicleMembership(CustomerVehiclesInformation.selectedVehicleInfo);
            if(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
            {
                MembershipDetails.selectedMembership = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.MembershipId;
            }

            _userDialog.HideLoading();
        }

        public async Task GetCompleteVehicleDetails()
        {
            _userDialog.ShowLoading();

            clientVehicleDetail = new CustomerCompleteDetails();

            clientVehicleDetail = await AdminService.GetVehicleCompleteDetails(CustomerVehiclesInformation.selectedVehicleInfo);

            _userDialog.HideLoading();
        }

        public async Task<bool> MembershipExists()
        {
            var confirm = await _userDialog.ConfirmAsync("Do you want to edit your membership ?");
            var confirmed = false;
            if(confirm)
            {
                return confirmed = true;
            }
            else
            {
                return confirmed = false;
            }
        }

        public async Task GetCustomerCardList()
        {
            _userDialog.ShowLoading();
            response = new CardDetailsResponse();
            response = await AdminService.GetCardDetails(CustomerInfo.ClientID);
            CustomerCardInfo.cardDetailsResponse = response;
            if (response != null)
            {
                foreach (var card in response.Status)
                {
                    if (int.Parse(card.VehicleId) == CustomerVehiclesInformation.selectedVehicleInfo)
                    {
                        CustomerCardInfo.SelectedCard = card;
                        response.Status = new List<status>();
                        response.Status.Add(card);
                        isData = true;
                    }
                    else
                    {
                        noData = true;
                    }

                }
            }
            _userDialog.HideLoading();
        }
        public async void NavToVehicleMembership()
        {
            await _navigationService.Navigate<VehicleMembershipViewModel>();
        }

        public async void NavToMembershipDetail()
        {
            await _navigationService.Navigate<VehicleMembershipDetailsViewModel>();
        }
        #endregion Commands

        public async void NavigatetoAddCard()
        {
            CustomerCardInfo.isAddCard = true;
            if (CustomerCardInfo.cardNumber != null && CustomerCardInfo.expiryDate != null)
            {
                CustomerCardInfo.cardNumber = string.Empty;
                CustomerCardInfo.expiryDate = string.Empty;
            }
            await _navigationService.Navigate<CardModifyViewModel>();
        }
        public async void NavToEditCard()
        {
            await _navigationService.Navigate<CardModifyViewModel>();
        }
        public async void DeleteCard()
        {

            var confirm = await _userDialog.ConfirmAsync("Are you sure you want to delete this Card ?");
            _userDialog.ShowLoading();
            if (confirm)
            {
                card = new CardDetails();
                card.CardNumber = CustomerCardInfo.cardNumber;
                card.ExpiryDate = CustomerCardInfo.expiryDate;
                card.CardType = "Credit";
                card.ClientId = CustomerInfo.ClientID;
                if (CustomerCardInfo.cardid != 0)
                {
                    card.Id = CustomerCardInfo.cardid;
                }
                else
                {
                    card.Id = 0;
                }

                card.IsActive = false;
                card.IsDeleted = true;
                card.CreatedDate = DateTime.Now;
                card.UpdatedDate = DateTime.Now;
                card.CreatedBy = CustomerInfo.ClientID;
                card.UpdatedBy = CustomerInfo.ClientID;
                card.MembershipId = null;
                card.VehicleId = null;
                ClientCardDetails = new AddCardRequest();
                ClientCardDetails.ClientCardDetails = new List<CardDetails>();
                ClientCardDetails.ClientCardDetails.Add(card);

                ClientCardDetails.client = new client();

                ClientCardDetails.client.clientId = CustomerInfo.customerPersonalInfo.Status[0].ClientId;
                ClientCardDetails.client.firstName = CustomerInfo.customerPersonalInfo.Status[0].FirstName;
                ClientCardDetails.client.middleName = CustomerInfo.customerPersonalInfo.Status[0].MiddleName;
                ClientCardDetails.client.lastName = CustomerInfo.customerPersonalInfo.Status[0].LastName;
                ClientCardDetails.client.gender = null;
                ClientCardDetails.client.maritalStatus = null;
                ClientCardDetails.client.birthDate = CustomerInfo.customerPersonalInfo.Status[0].BirthDate.ToString();
                ClientCardDetails.client.recNotes = CustomerInfo.customerPersonalInfo.Status[0].RecNotes;
                ClientCardDetails.client.score = CustomerInfo.customerPersonalInfo.Status[0].Score;
                ClientCardDetails.client.noEmail = CustomerInfo.customerPersonalInfo.Status[0].NoEmail;
                ClientCardDetails.client.notes = CustomerInfo.customerPersonalInfo.Status[0].Notes;
                ClientCardDetails.client.clientType = CustomerInfo.customerPersonalInfo.Status[0].ClientType;
                ClientCardDetails.client.authId = CustomerInfo.AuthID;
                ClientCardDetails.client.isActive = true;
                ClientCardDetails.client.isDeleted = false;
                ClientCardDetails.client.createdDate = DateTime.Now.ToString();
                ClientCardDetails.client.updatedDate = DateTime.Now.ToString();
                ClientCardDetails.client.createdBy = CustomerInfo.customerPersonalInfo.Status[0].ClientId;
                ClientCardDetails.client.updatedBy = CustomerInfo.customerPersonalInfo.Status[0].ClientId;

                ClientCardDetails.token = null;
                ClientCardDetails.password = null;

                var result = await AdminService.AddClientCard(ClientCardDetails);
                if (result.Status == true)
                {

                    await _navigationService.Navigate<MyProfileInfoViewModel>();

                }
                else
                {
                    _userDialog.Alert("Card Not Deleted");

                }

            }
            else
            {
                _userDialog.HideLoading();
                await _navigationService.Navigate<MyProfileInfoViewModel>();
            }

        }
    }
}

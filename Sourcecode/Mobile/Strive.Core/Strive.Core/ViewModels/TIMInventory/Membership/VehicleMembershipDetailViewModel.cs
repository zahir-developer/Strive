using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class VehicleMembershipDetailViewModel : BaseViewModel
    {

        private MvxSubscriptionToken _messageToken;

        public VehicleMembershipDetailViewModel()
        {
            GetDetails();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }
        public bool noData { get; set; }
        public bool isData { get; set; }
        public CardDetailsResponse response { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails;


        void GetDetails()
        {
            MembershipName = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipDetail.MembershipId).Select(m => m.MembershipName).FirstOrDefault();
        }

        public ClientVehicleMembershipView MembershipDetail { get; set; } = MembershipData.MembershipDetailView.ClientVehicleMembership;

        public string MembershipName { get; set; }

        public string ActivatedDate { get { return MembershipDetail.StartDate.ToShortDateString(); } set { } }

        public string CancelledDate { get { return MembershipDetail.EndDate.ToShortDateString(); } set { } }

        public string Status { get { return (bool)MembershipDetail.Status ? "Active" : "Inactive"; } set { } }

        public async Task NavigateBackCommand()
        {
            MembershipData.MembershipDetailView = null;
            await _navigationService.Close(this);
        }

        public async Task ChangeMembershipCommand()
        {
            
            await _navigationService.Navigate<SelectMembershipViewModel>();
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }
        public async Task CancelMembershipCommand()
        {
            var affirmative = await _userDialog.ConfirmAsync("Are you sure want to cancel the membership?", "Cancel", "Yes", "No");
            if(!affirmative)
            {
                return;
            }
            var VehicleMembership = PrepareVehicleMembership();
            _userDialog.ShowLoading("Cancelling the membership");
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);
            if (result == null)
            {
                await _userDialog.AlertAsync("Error occured");
                return;
            }
            if (result.Status)
            {
                await _userDialog.AlertAsync("Membership Cancelled");
            }
            else
            {
                await _userDialog.AlertAsync("Could not cancel membership");
            }
            await _navigationService.Close(this);
            MembershipData.MembershipDetailView = null;

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
                card.CreatedBy = MembershipData.SelectedClient.ClientId;
                card.UpdatedBy = MembershipData.SelectedClient.ClientId;
                card.MembershipId = null;
                card.VehicleId = null;
                ClientCardDetails = new AddCardRequest();
                ClientCardDetails.ClientCardDetails = new List<CardDetails>();
                ClientCardDetails.ClientCardDetails.Add(card);

                ClientCardDetails.client = new client();

                ClientCardDetails.client.clientId = MembershipData.SelectedClient.ClientId;
                ClientCardDetails.client.firstName = MembershipData.SelectedClient.FirstName;
                ClientCardDetails.client.middleName = MembershipData.SelectedClient.MiddleName;
                ClientCardDetails.client.lastName = MembershipData.SelectedClient.LastName;
                ClientCardDetails.client.gender = null;
                ClientCardDetails.client.maritalStatus = null;
                ClientCardDetails.client.birthDate = MembershipData.SelectedClient.BirthDate.ToString();
                ClientCardDetails.client.recNotes = MembershipData.SelectedClient.RecNotes;
                ClientCardDetails.client.score = MembershipData.SelectedClient.Score;
                ClientCardDetails.client.noEmail = MembershipData.SelectedClient.NoEmail;
                ClientCardDetails.client.notes = MembershipData.SelectedClient.Notes;
                ClientCardDetails.client.clientType = MembershipData.SelectedClient.ClientType;
                ClientCardDetails.client.authId = 0;
                ClientCardDetails.client.isActive = false;
                ClientCardDetails.client.isDeleted = true;
                ClientCardDetails.client.createdDate = DateTime.Now.ToString();
                ClientCardDetails.client.updatedDate = DateTime.Now.ToString();
                ClientCardDetails.client.createdBy = MembershipData.SelectedClient.ClientId;
                ClientCardDetails.client.updatedBy = MembershipData.SelectedClient.ClientId;

                ClientCardDetails.token = null;
                ClientCardDetails.password = null;

                var result = await AdminService.AddClientCard(ClientCardDetails);
                if (result.Status == true)
                {
                    await _navigationService.Navigate<MembershipClientDetailViewModel>();
                }
                else
                {
                    _userDialog.Alert("Card Not Deleted");
                }

            }
            else
            {
                _userDialog.HideLoading();
                await _navigationService.Navigate<MembershipClientDetailViewModel>();
            }

        }
        ClientVehicleRoot PrepareVehicleMembership()
        {
            int ClientMembership = 0;
            if (MembershipData.MembershipDetailView != null)
            {
                ClientMembership = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
            }
            var model = new ClientVehicleRoot()
            {
                clientVehicle = new ClientVehicle() { clientVehicle = null },
                clientVehicleMembershipModel = new ClientVehicleMembershipModel()
                {
                    clientVehicleMembershipDetails = new ClientVehicleMembershipDetails()
                    {
                        clientMembershipId = ClientMembership,
                        clientVehicleId = MembershipData.SelectedVehicle.VehicleId,
                        locationId = EmployeeData.selectedLocationId,
                        membershipId = MembershipData.MembershipDetailView.ClientVehicleMembership.MembershipId,
                        startDate = DateUtils.GetTodayDateString(),
                        endDate = DateUtils.GetTodayDateString(),
                        status = true,
                        notes = "",
                        isActive = false,
                        isDeleted = true
                    },
                    clientVehicleMembershipService = new List<ClientVehicleMembershipService>()
                    {
                        new ClientVehicleMembershipService()
                        {
                            clientVehicleMembershipServiceId = 0,
                            clientMembershipId = ClientMembership,
                            serviceId = MembershipData.MembershipDetailView.ClientVehicleMembershipService[0].ServiceId,
                            serviceTypeId = 0,
                            isActive = true,
                            isDeleted = false
                        }
                    },
                }
            };
            return model;
        }



    }
}

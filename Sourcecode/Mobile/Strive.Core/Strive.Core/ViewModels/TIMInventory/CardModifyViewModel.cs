using System;
using System.Collections.Generic;
using Strive.Core.Models.Customer;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class CardModifyViewModel: BaseViewModel
    {

        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails;

        public async void Navigatetoperosnalinfo()
        {
            _userDialog.ShowLoading();

            if (cardNumber != null && expiryDate != null)
            {
                string month = expiryDate.Substring(0, 2);
                string year = "20" + expiryDate.Substring(3, 2);
                int parsedmnth = int.Parse(month);
                if (parsedmnth > 12)
                {
                    _userDialog.Alert("Enter Valid Month");
                    await _navigationService.Navigate<CardModifyViewModel>();

                }
                else
                {
                    card = new CardDetails();
                    card.CardNumber = cardNumber;
                    card.ExpiryDate = "01/" + month + "/" + year;
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

                    card.IsActive = true;
                    card.IsDeleted = false;
                    card.CreatedDate = DateTime.Now;
                    card.UpdatedDate = DateTime.Now;
                    card.CreatedBy = MembershipData.SelectedClient.ClientId;
                    card.UpdatedBy = MembershipData.SelectedClient.ClientId;
                    card.MembershipId = null;
                    card.VehicleId = MembershipData.SelectedVehicle.VehicleId.ToString();  //CustomerVehiclesInformation.selectedVehicleInfo.ToString();
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
                    ClientCardDetails.client.isActive = true;
                    ClientCardDetails.client.isDeleted = false;
                    ClientCardDetails.client.createdDate = DateTime.Now.ToString();
                    ClientCardDetails.client.updatedDate = DateTime.Now.ToString();
                    ClientCardDetails.client.createdBy = MembershipData.SelectedClient.ClientId;
                    ClientCardDetails.client.updatedBy = MembershipData.SelectedClient.ClientId;

                    ClientCardDetails.token = null;
                    ClientCardDetails.password = null;

                    var result = await AdminService.AddClientCard(ClientCardDetails);
                }



            }
            _userDialog.HideLoading();
            await _navigationService.Navigate<MembershipClientDetailViewModel>();
        }



    }
}

using System;
using System.Collections.Generic;
using Strive.Core.Models.Customer;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Customer
{
    public class CardModifyViewModel : BaseViewModel
    {
        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails ;

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
                    card.ExpiryDate = "01/"+ month + "/"+ year;
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
                    card.CreatedBy = CustomerInfo.ClientID;
                    card.UpdatedBy = CustomerInfo.ClientID;
                    card.MembershipId = null;
                    card.VehicleId = CustomerVehiclesInformation.selectedVehicleInfo.ToString() ;
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
                }
                
                

            }
            _userDialog.HideLoading();
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }

    }
}

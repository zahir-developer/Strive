using System;
using System.Collections.Generic;
using System.Linq;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory.Membership;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class PaymentViewModel : BaseViewModel
    {
        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails;
        public float finalmonthlycharge;
        private double MonthlyCharge;

        private void GetTotal()
        {
            MonthlyCharge = MembershipData.CalculatedPrice;

        }

        public async void MembershipAgree()
        {
            if (MembershipData.MembershipDetailView != null)
            {
                if (MembershipData.MembershipDetailView.ClientVehicleMembership != null)
                {
                    deleteMembership Membershipdelete = new deleteMembership();
                    Membershipdelete.clientId = MembershipData.MembershipDetailView.ClientVehicle.ClientId;
                    Membershipdelete.clientMembershipId = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
                    Membershipdelete.vehicleId = MembershipData.MembershipDetailView.ClientVehicle.VehicleId;

                    var isDeleted = await AdminService.DeleteVehicleMembership(Membershipdelete);
                }

            }
            var VehicleMembership = PrepareVehicleMembership();
            //AddClientCard();
            _userDialog.ShowLoading("Assigning membership to the vehicle...");
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);

            if (result == null)
            {
                await _userDialog.AlertAsync("Membership not Assigned. Please try again.");

                _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
                return;
            }
            if (result.Status)
            {
                var codeByCategory = await AdminService.GetCodesByCategory();

                var membershipAgreement = codeByCategory.Codes.Find(x => x.CodeValue == "MembershipAgreement");

                var termsDocument = new Document()
                {
                    DocumentId = membershipAgreement.CategoryId,
                    DocumentName = "MembershipAgreement",
                    DocumentType = membershipAgreement.CodeId,
                    DocumentSubType = null,
                    Base64 =  SignatureViewModel.Base64ContractString,
                    CreatedBy = CustomerInfo.ClientID,
                    CreatedDate = DateTime.Now,
                    FileName = "MembershipAgreement -" + CustomerInfo.custName + ".jpeg",
                    FilePath = "",
                    OriginalFileName = "MembershipAgreement -" + CustomerInfo.custName + ".jpeg",
                    IsActive = true,
                    IsDeleted = false,
                    RoleId = null,
                    UpdatedBy = CustomerInfo.ClientID,
                    UpdatedDate = DateTime.Now

                };

                var addDocument = new AddDocument()
                {
                    Document = termsDocument,
                    DocumentType = membershipAgreement.CodeValue,

                };

                var document = await AdminService.AddDocumentDetails(addDocument);
                if (document != null)
                {
                    await _userDialog.AlertAsync("Amount will charge from 1st of next month.");

                    MembershipDetails.clearMembershipData(); 
                    await _navigationService.Navigate<MembershipClientDetailViewModel>();
                    
                }

            }
            else
            {
                await _userDialog.AlertAsync("Membership not Assigned. Please try again.");
            }



            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
        }


        ClientVehicleRoot PrepareVehicleMembership()
        {
            int ClientMembership = 0;
            GetTotal();
            if (MembershipData.MembershipDetailView != null)
            {
                ClientMembership = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
            }
            List<AllServiceDetail> serviceDetails = new List<AllServiceDetail>();
            var SelectedMembership = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipData.SelectedMembership.MembershipId).FirstOrDefault();
            string[] selectedServices = SelectedMembership.Services.Split(",");

            foreach (var SelectedService in selectedServices)
            {
                var defaultservices = MembershipData.AllAdditionalServices.ToList().Find(x => x.ServiceName.Replace(" ", "") == SelectedService.Replace(" ", ""));
                serviceDetails.Add(defaultservices);
            }

            foreach (var item in serviceDetails)
            {
                if (item != null)
                {
                    MembershipData.ExtraServices.Add(new ClientVehicleMembershipService()
                    {
                        clientVehicleMembershipServiceId = 0,
                        clientMembershipId = ClientMembership,
                        serviceId = item.ServiceId,
                        serviceTypeId = item.ServiceTypeId,
                        isActive = true,
                        isDeleted = false,

                    });
                }

            }
            var model = new ClientVehicleRoot()
            {
                clientVehicle = new ClientVehicle()
                {
                    //clientVehicle = null

                },
                clientVehicleMembershipModel = new ClientVehicleMembershipModel()
                {
                    clientVehicleMembershipDetails = new ClientVehicleMembershipDetails()
                    {
                        clientMembershipId = ClientMembership,
                        clientVehicleId = MembershipData.SelectedVehicle.VehicleId,
                        locationId = EmployeeData.selectedLocationId,
                        membershipId = MembershipData.SelectedMembership.MembershipId,
                        startDate = DateUtils.GetTodayDateString(),
                        endDate = DateUtils.GetTodayDateString(),
                        status = true,
                        notes = "",
                        isActive = true,
                        totalPrice = (float)Convert.ToDouble(MonthlyCharge),
                        isDeleted = false
                    },
                    clientVehicleMembershipService = MembershipData.ExtraServices
                }
            };
            return model;
        }
        public async void AddClientCard()
        {
            if (cardNumber != null && expiryDate != null)
            {
                string month = expiryDate.Substring(0, 2);
                string year = "20" + expiryDate.Substring(3, 2);
                int parsedmnth = int.Parse(month);
                if (parsedmnth > 12)
                {
                    _userDialog.Alert("Enter Valid Month");

                }
                else
                {
                    card = new CardDetails();
                    card.CardNumber = cardNumber;
                    card.ExpiryDate = "01/" + month + "/" + year;
                    card.CardType = "Credit";
                    card.ClientId = MembershipData.SelectedClient.ClientId;
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


        }




    }
}

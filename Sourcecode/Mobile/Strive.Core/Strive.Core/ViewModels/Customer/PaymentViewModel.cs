using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Xamarin.Essentials;

namespace Strive.Core.ViewModels.Customer
{
    public class PaymentViewModel : BaseViewModel
    {
        public List<ClientVehicleMembershipService> selectedmembershipServices = new List<ClientVehicleMembershipService>();
        ClientVehicle ClientVehicless;
        public static string Base64ContractString;
        public float finalmonthlycharge;
        private List<ServiceDetail> servicedetails;
       // public bool isAndroid = false;
        public bool membershipStatus = false;

        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public string profileId { get; set; }
        public string accountId { get; set; }
        private int documentId { get; set; }
        public CardDetails card;
        public AddCardRequest ClientCardDetails;
        public PaymentViewModel()
        {

        }

        
        public async Task MembershipAgree()
        {

            _userDialog.ShowLoading();
            if (CustomerVehiclesInformation.completeVehicleDetails != null)
            {
                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    deleteMembership Membershipdelete = new deleteMembership();
                    Membershipdelete.clientId =CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicle.ClientId;
                    Membershipdelete.clientMembershipId =CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId;
                    Membershipdelete.vehicleId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicle.VehicleId;

                    var isDeleted = await AdminService.DeleteVehicleMembership(Membershipdelete);
                }
            }

           
            var codeByCategory = await AdminService.GetCodesByCategory();

            var membershipAgreement = codeByCategory.Codes.Find(x => x.CodeValue == "MembershipAgreement");

            var termsDocument = new Document()
            {
                DocumentId = 0,//membershipAgreement.CategoryId,
                DocumentName = "MembershipAgreement",
                DocumentType = membershipAgreement.CodeId,
                DocumentSubType = null,
                Base64 = Base64ContractString,
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
                documentId = document.Result;
                PrepareAdditionalServices();
                var data = await AdminService.SaveVehicleMembership(MembershipDetails.customerVehicleDetails);
                if (data.Status == true)
                {
                    membershipStatus = true;
                    if (platform == DevicePlatform.iOS)
                    { 
                        await _userDialog.AlertAsync("Amount will charge from 1st of next month."); 
                    }
                    
                }
                else
                {
                    if (platform == DevicePlatform.iOS)
                    {
                        _userDialog.Alert("Error membership not created"); 
                    }
                   
                }

                MembershipDetails.clearMembershipData();
                if (platform == DevicePlatform.iOS)
                {
                    await _navigationService.Navigate<MyProfileInfoViewModel>();
                }


            }            
            else
            {
                _userDialog.Alert("Error membership not created");
            }
            _userDialog.HideLoading();
        }

        private void PrepareAdditionalServices()
        {
            
            if (MembershipDetails.selectedMembershipDetail.Services != null)
            {
                string[] selectedServices = MembershipDetails.selectedMembershipDetail.Services.Split(",");
                List<ServiceDetail> serviceDetails = new List<ServiceDetail>();
                foreach (var SelectedService in selectedServices)
                {
                    var defaultservices = MembershipDetails.completeList.ServicesWithPrice.Find(x => x.ServiceName.Replace(" ", "") == SelectedService.Replace(" ", ""));
                    serviceDetails.Add(defaultservices);
                }
                foreach (var item in serviceDetails)
                {
                    ClientVehicleMembershipService clientVehicleMembershipService = new ClientVehicleMembershipService();
                    clientVehicleMembershipService.serviceId = item.ServiceId;
                    clientVehicleMembershipService.serviceTypeId = item.ServiceTypeId;
                    clientVehicleMembershipService.createdDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.updatedDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.clientMembershipId = item.MembershipId;
                    clientVehicleMembershipService.clientVehicleMembershipServiceId = item.MembershipServiceId;
                    selectedmembershipServices.Add(clientVehicleMembershipService);
                }
                var servicesDetails = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
                foreach (var service in servicesDetails)
                {
                    ClientVehicleMembershipService clientVehicleMembershipService = new ClientVehicleMembershipService();
                    clientVehicleMembershipService.serviceId = service.ServiceId;
                    clientVehicleMembershipService.serviceTypeId = service.ServiceTypeId;
                    clientVehicleMembershipService.createdDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.updatedDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.clientMembershipId = service.MembershipId;
                    clientVehicleMembershipService.clientVehicleMembershipServiceId = service.MembershipServiceId;
                    selectedmembershipServices.Add(clientVehicleMembershipService);
                }
                if (MembershipDetails.modelUpcharge.upcharge.Count > 0) 
                {
                    ClientVehicleMembershipService clientVehicleMembershipService = new ClientVehicleMembershipService();
                    clientVehicleMembershipService.serviceId = MembershipDetails.modelUpcharge.upcharge[0].ServiceId;
                    clientVehicleMembershipService.serviceTypeId = MembershipDetails.modelUpcharge.upcharge[0].ServiceTypeId;
                    clientVehicleMembershipService.createdDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.updatedDate = DateTime.Now.ToString("yyyy-MM-d");
                    clientVehicleMembershipService.clientMembershipId = 0;
                    clientVehicleMembershipService.clientVehicleMembershipServiceId = 0;
                    clientVehicleMembershipService.services = MembershipDetails.modelUpcharge.upcharge[0].Upcharges;
                    selectedmembershipServices.Add(clientVehicleMembershipService);
                }

                MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipService = selectedmembershipServices;
            }
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipService = selectedmembershipServices;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.totalPrice = finalmonthlycharge;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.cardNumber = cardNumber;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.expiryDate = expiryDate;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.profileId = profileId;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.accountId = accountId;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.documentId = documentId;
            
            ClientVehicless = new ClientVehicle();
            ClientVehicless.clientVehicle = new ClientVehicleDetail();
            ClientVehicless.clientVehicle.monthlyCharge = finalmonthlycharge;
            ClientVehicless.clientVehicle.isActive = true;
            ClientVehicless.clientVehicle.isDeleted = false;
            ClientVehicless.clientVehicle.clientId = CustomerInfo.ClientID;
            ClientVehicless.clientVehicle.createdDate = DateTime.Now.ToString("yyyy-MM-d");
            ClientVehicless.clientVehicle.updatedDate = DateTime.Now.ToString("yyyy-MM-d");
            ClientVehicless.clientVehicle.vehicleId = MembershipDetails.clientVehicleID;
            ClientVehicless.clientVehicle.vehicleColor = MembershipDetails.colorNumber;
            ClientVehicless.clientVehicle.vehicleNumber = MembershipDetails.vehicleNumber;
            ClientVehicless.clientVehicle.vehicleModel = MembershipDetails.modelNumber;
            ClientVehicless.clientVehicle.vehicleMfr = MembershipDetails.vehicleMakeNumber;
            
            
            if (MembershipDetails.modelUpcharge.upcharge.Count > 0)
            {
                ClientVehicless.clientVehicle.upcharge = (int)MembershipDetails.modelUpcharge.upcharge[0].Price;
            }
            else 
            {
                ClientVehicless.clientVehicle.upcharge = 0;  
            }           

            ClientVehicless.clientVehicle.barcode = MembershipDetails.barCode;
            ClientVehicless.clientVehicle.locationId = null;
            MembershipDetails.customerVehicleDetails.clientVehicle = ClientVehicless;
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
                    card.VehicleId = CustomerVehiclesInformation.selectedVehicleInfo.ToString();
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


        }   
        public enum PaymentStatus
        {
            Success
        }

        public enum PaymentType
        {
            Card,
            Account,
            Tips
        }
    }
}

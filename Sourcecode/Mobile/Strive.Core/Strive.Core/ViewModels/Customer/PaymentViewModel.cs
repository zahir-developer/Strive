using System;
using System.Collections.Generic;
using System.Linq;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.ViewModels.Customer
{
    public class PaymentViewModel : BaseViewModel
    {
        public List<ClientVehicleMembershipService> selectedmembershipServices = new List<ClientVehicleMembershipService>();
        ClientVehicle ClientVehicless;
        public static string Base64ContractString;
        public float finalmonthlycharge;
        private List<ServiceDetail> servicedetails;
        public bool isAndroid = false;
        public PaymentViewModel()
        {

        }

        
        public async void MembershipAgree()
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

            PrepareAdditionalServices();
           
            var data = await AdminService.SaveVehicleMembership(MembershipDetails.customerVehicleDetails);
            if (data.Status == true)
            {
                var codeByCategory = await AdminService.GetCodesByCategory();

                var membershipAgreement = codeByCategory.Codes.Find(x => x.CodeValue == "MembershipAgreement");
               
                var termsDocument = new Document()
                {
                    DocumentId = membershipAgreement.CategoryId,
                    DocumentName = "MembershipAgreement",
                    DocumentType = membershipAgreement.CodeId,
                    DocumentSubType = null,
                    Base64 = Base64ContractString,
                    CreatedBy = CustomerInfo.ClientID,
                    CreatedDate = DateTime.Now,
                    FileName = "MembershipAgreement -"+ CustomerInfo.custName + ".jpeg",
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
                    _userDialog.Toast("Membership has been created successfully");
                    MembershipDetails.clearMembershipData();
                    if (!isAndroid) 
                    { 
                        await _navigationService.Navigate<MyProfileInfoViewModel>(); 
                    }                   
                  
                }
                else
                {
                    _userDialog.Alert("Error membership not created");
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

                MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipService = selectedmembershipServices;
            }
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipService = selectedmembershipServices;
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipDetails.totalPrice = finalmonthlycharge;
            //ClientVehicless = new ClientVehicle();
            //ClientVehicless.clientVehicle = new ClientVehicleDetail();
            //ClientVehicless.clientVehicle.monthlyCharge = finalmonthlycharge;
            //ClientVehicless.clientVehicle.isActive = true;
            //ClientVehicless.clientVehicle.isDeleted = false;
            //ClientVehicless.clientVehicle.clientId = CustomerInfo.ClientID;
            //ClientVehicless.clientVehicle.createdDate = DateTime.Now.ToString("yyyy-MM-d");
            //ClientVehicless.clientVehicle.updatedDate = DateTime.Now.ToString("yyyy-MM-d");
            //ClientVehicless.clientVehicle.vehicleId = MembershipDetails.clientVehicleID;
            //ClientVehicless.clientVehicle.vehicleColor = MembershipDetails.colorNumber;
            //ClientVehicless.clientVehicle.vehicleNumber = MembershipDetails.vehicleNumber;
            //ClientVehicless.clientVehicle.vehicleModelNo = MembershipDetails.modelNumber;
            //ClientVehicless.clientVehicle.upcharge = 0;
            //ClientVehicless.clientVehicle.barcode = "";
            //ClientVehicless.clientVehicle.locationId = 1;
            //MembershipDetails.customerVehicleDetails.clientVehicle = ClientVehicless;
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

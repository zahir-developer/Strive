using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;
using System.Linq;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SignatureViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;
        private double MonthlyCharge;
        public  string Base64ContractString;
        public SignatureViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                //_messageToken.Dispose();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
        private void GetTotal()
        {
             MonthlyCharge = MembershipData.CalculatedPrice;
             
        }
      

        public async void NextCommand()
        {
            if (MembershipData.MembershipDetailView != null)
            {
                if (MembershipData.MembershipDetailView.ClientVehicleMembership!=null)
                {
                    deleteMembership Membershipdelete = new deleteMembership();
                    Membershipdelete.clientId = MembershipData.MembershipDetailView.ClientVehicle.ClientId;
                    Membershipdelete.clientMembershipId = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
                    Membershipdelete.vehicleId = MembershipData.MembershipDetailView.ClientVehicle.VehicleId;

                    var isDeleted = await AdminService.DeleteVehicleMembership(Membershipdelete);
                }
                    
            }
            var VehicleMembership = PrepareVehicleMembership();
            _userDialog.ShowLoading("Assigning membership to the vehicle...");
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);

            if(result == null)
            {
                await _userDialog.AlertAsync("Membership not Assigned. Please try again.");

                _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
                return;
            }
            if(result.Status)
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
                    await _userDialog.AlertAsync("Membership Assigned");
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
                var defaultservices =MembershipData.AllAdditionalServices.ToList().Find(x => x.ServiceName.Replace(" ", "") == SelectedService.Replace(" ", ""));
                serviceDetails.Add(defaultservices);
            }

            foreach (var item in serviceDetails)
            {
                if(item!=null)
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
                        totalPrice = (float)Convert.ToDouble(MonthlyCharge) ,
                        isDeleted = false
                    },
                    clientVehicleMembershipService = MembershipData.ExtraServices
                }
            };
            return model;
        }
    }
}

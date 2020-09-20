﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class MyProfileInfoViewModel : BaseViewModel
    {

        #region Properties
       
        public int ClientId { get; set; } = 122;
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string SecondaryContactNumber { get; set; }
        public string Email { get; set; }
        public VehicleCodes vehicleCodes { get; set; }
        public Dictionary<int, string> UpCharges = new Dictionary<int, string>();
        public Dictionary<int, string> manufacturerName = new Dictionary<int, string>();
        public Dictionary<int, string> modelName = new Dictionary<int, string>();
        public Dictionary<int, string> colorName = new Dictionary<int, string>();
        public CustomerPersonalInfo customerInfo { get; set; }
        public CustomerUpdateInfo InfoModel = new CustomerUpdateInfo();
        public SelectedServiceList selectedMembership = new SelectedServiceList();

        #endregion Properties

        #region Commands

        public async Task<CustomerPersonalInfo> getClientById()
        {
            _userDialog.ShowLoading(Strings.Loading, Acr.UserDialogs.MaskType.Gradient);
            customerInfo = await AdminService.GetClientById(ClientId);
            if (customerInfo == null)
            {

            }
            _userDialog.HideLoading();
            return customerInfo;
        }

        public async Task<CustomerResponse> saveClientInfoCommand()
        {
            _userDialog.ShowLoading(Strings.Loading, Acr.UserDialogs.MaskType.Gradient);
            InfoModel.client = new client();
            InfoModel.clientAddress = new List<clientAddress>();
            var clientVehicle = new clientVehicle();
            var clientAddress = new clientAddress();
            var names = FullName.Split(" ");

            if (names.Length == 1)
            {
                FirstName = names[0];
                MiddleName = "";
                LastName = "";
            }
            else if (names.Length == 2)
            {
                FirstName = names[0];
                MiddleName = names[1];
                LastName = "";
            }
            else
            {
                FirstName = names[0];
                MiddleName = names[1];
                LastName = names[2];
            }

            if (String.IsNullOrEmpty(Email))
            {
                InfoModel.client.noEmail = true;
                clientAddress.email = "";
            }
            else
            {
                InfoModel.client.noEmail = false;
                clientAddress.email = Email;
            }
            InfoModel.client.clientId = ClientId;
            clientAddress.clientId = ClientId;
            clientVehicle.clientId = ClientId;
            InfoModel.client.firstName = FirstName;
            InfoModel.client.lastName = LastName;
            InfoModel.client.middleName = MiddleName;
            clientAddress.address1 = Address;
            clientAddress.address2 = Address;
            clientAddress.phoneNumber = ContactNumber;
            clientAddress.phoneNumber2 = SecondaryContactNumber;
            clientAddress.zip = ZipCode;
            InfoModel.clientAddress.Add(clientAddress);
            var infoUploadSuccess = await AdminService.SaveClientInfo(InfoModel);
            if (infoUploadSuccess == null)
            {

            }
            if (infoUploadSuccess.Status == "true")
            {
                _userDialog.HideLoading();
            }

            return infoUploadSuccess;


        }

       
        public async Task getAllServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var completeList = await AdminService.GetVehicleServices();
            if (completeList == null)
            {
                _userDialog.HideLoading();
                
            }
            CustomerInfo.filteredList = new ServiceList();
            CustomerInfo.filteredList.ServicesWithPrice = new List<ServiceDetail>();
            foreach (var upcharges in selectedMembership.MembershipDetail)
            {
                CustomerInfo.filteredList.
                    ServicesWithPrice.
                    Add(
                     completeList.
                     ServicesWithPrice.
                     Find(c => c.ServiceId == upcharges.ServiceId)
                    );
                 
            }
            _userDialog.HideLoading();
           
        }
        public async Task getServiceList(int membershipId)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var data = await AdminService.GetSelectedMembershipServices(membershipId);
            if(data == null)
            {
                _userDialog.HideLoading();
                _userDialog.Alert("Error !");
                return;
            }
            selectedMembership.MembershipDetail = new List<ServiceDetail>();
            foreach (var result in data.MembershipDetail)
            {
                if(!String.Equals(result.ServiceType.ToString(), "Washes"))
                {
                    selectedMembership.MembershipDetail.Add(result);
                }
            }
            _userDialog.HideLoading();           
        }

        #endregion Commands
    }
}

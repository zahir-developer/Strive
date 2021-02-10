﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class PersonalInfoEditViewModel : BaseViewModel
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

        public CustomerUpdateInfo InfoModel = new CustomerUpdateInfo();
        public SelectedServiceList selectedMembership = new SelectedServiceList();

        #endregion Properties


        #region Commands

        public async Task<bool> saveClientInfoCommand()
        {
            _userDialog.ShowLoading(Strings.Loading, Acr.UserDialogs.MaskType.Gradient);
            bool proceed = true;
            if (validateData())
            {
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
                InfoModel.client.clientId = CustomerInfo.ClientID;
                clientAddress.clientId = CustomerInfo.ClientID;
                clientVehicle.clientId = CustomerInfo.ClientID;
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
                    _userDialog.Alert("Infomation was not saved.");
                    return proceed = false;
                }
                if (infoUploadSuccess.Status == "true")
                {
                    _userDialog.HideLoading();
                    return proceed = true;
                }
                return proceed;
            }
            else
            {
                _userDialog.HideLoading();
                return proceed = false;
            }


        }

        public bool validateData()
        {
            var proceed = true;
            if(string.IsNullOrEmpty(FullName))
            {
                _userDialog.Alert("Please enter your name.");
                return proceed = false;
            }
            if(string.IsNullOrEmpty(ContactNumber))
            {
                _userDialog.Alert("Please enter your contact number.");
                return proceed = false;
            }
            if (string.IsNullOrEmpty(Address))
            {
                _userDialog.Alert("Please enter your address.");
                return proceed = false;
            }
            if (string.IsNullOrEmpty(Email))
            {
                _userDialog.Alert("Please enter your email Id.");
                return proceed = false;
            }
            return proceed;
        }

        public async void NavigateToProfile()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }

        #endregion Commands
    }
}

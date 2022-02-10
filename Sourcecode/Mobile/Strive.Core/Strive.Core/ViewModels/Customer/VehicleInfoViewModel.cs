﻿using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleInfoViewModel : BaseViewModel
    {

        #region Properties

        public VehicleList vehicleLists { get; set; }
        public ClientVehicleRootView membershipDetails { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetCustomerVehicleList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            vehicleLists = new VehicleList();
            vehicleLists.Status = new List<VehicleDetail>();
            CustomerVehiclesInformation.vehiclesList = new VehicleList();
            CustomerVehiclesInformation.vehiclesList.Status = new List<VehicleDetail>();
            vehicleLists = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            CustomerVehiclesInformation.vehiclesList = vehicleLists;
            if (vehicleLists == null || vehicleLists.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }

            _userDialog.HideLoading();
        }

        public async Task<bool> DeleteCustomerVehicle(int vehicleID)
        {            
            bool deleted = false;
            var confirm = await _userDialog.ConfirmAsync("Are you sure you want to delete this vehicle ?");
            _userDialog.ShowLoading(Strings.Loading);
            if (confirm)
            {
                var data = await AdminService.DeleteCustomerVehicle(vehicleID);
                if (data.Status)
                {
                    _userDialog.HideLoading();
                    _userDialog.Toast("Vehicle details deleted successfully");
                    deleted = true;
                }
                else
                {
                    _userDialog.HideLoading();
                    _userDialog.Toast("Vehicle deletion unsuccessful");
                }
            }
            else
            {
                _userDialog.HideLoading();
                // closes dialog box
            }
            
            return deleted;
        }
        public async Task<string> DownloadTerms(int documentId)
        {
            //var codeByCategory = await AdminService.GetCodesByCategory();

            //var membershipAgreement = codeByCategory.Codes.Find(x => x.CodeValue == "MembershipAgreement");
            _userDialog.ShowLoading(Strings.Loading);

            if (documentId != 0)
            {
                TermsDocument document = await AdminService.TermsDocuments(documentId, "MEMBERSHIPAGREEMENT");

                _userDialog.HideLoading();

                return document.Document.Document.Base64;

            }
            else
            {
                _userDialog.HideLoading();

                _userDialog.Toast("This membeship dosen't have any terms document atached");

            }


            return "";

        }



        public async void NavToAddVehicle()
        {
            CustomerVehiclesInformation.completeVehicleDetails = null;
            await _navigationService.Navigate<VehicleInfoEditViewModel>();
        }

        public async void NavToEditVehicle()
        {
            await _navigationService.Navigate<VehicleInfoDisplayViewModel>();
        }

        public async void NavToProfile()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }
        #endregion Commands


    }
}

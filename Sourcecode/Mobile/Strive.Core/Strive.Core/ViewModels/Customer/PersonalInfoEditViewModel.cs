using Strive.Core.Models.Customer;
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

        public async Task saveClientInfoCommand()
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

            return;


        }


        #endregion Commands
    }
}

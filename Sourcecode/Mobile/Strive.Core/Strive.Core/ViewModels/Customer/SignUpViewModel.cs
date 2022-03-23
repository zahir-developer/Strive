using Acr.UserDialogs;
using Newtonsoft.Json;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Strive.Core.Models.Customer.CustomerSignUp;

namespace Strive.Core.ViewModels.Customer
{
    public class SignUpViewModel : BaseViewModel
    {
        public VehicleCodes vehicleCodes { get; set; }
        public Dictionary<int, string> manufacturerName = new Dictionary<int, string>();
        public Dictionary<int, string> modelName = new Dictionary<int, string>();
        public Dictionary<int, string> ColorList = new Dictionary<int, string>();
        public MakeList makeList { get; set; }
        public ModelList modelList { get; set; }
        public ColorList colorList { get; set; }
        SignUpRequest signUpRequest = new SignUpRequest();
        ClientAddress clientAddress = new ClientAddress();
        ClientVehicle clientVehicle = new ClientVehicle();
        #region Commands

        public async void SignUpCommand()
        {
            if (string.IsNullOrEmpty(FirstName)&& string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(EmailAddress) && string.IsNullOrEmpty(PhoneNumber) &&
                string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword) && string.IsNullOrEmpty(VehicleColor) && string.IsNullOrEmpty(VehicleMake) && string.IsNullOrEmpty(VehicleModel))
            {
                _userDialog.Alert("Please Fill All The Details");
            }
            else if (!Validations.validateEmail(EmailAddress) || String.IsNullOrEmpty(EmailAddress))
            {
                _userDialog.Alert(Strings.ValidEmail);
            }
            //else if (!Validations.validatePhone(PhoneNumber) || String.IsNullOrEmpty(PhoneNumber))
            //{
            //    _userDialog.Alert(Strings.ValidMobile);
            //}
            else if (String.IsNullOrEmpty(FirstName))
            {
                _userDialog.Alert(Strings.ValidName);
            }
            else if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                _userDialog.Alert(Strings.PasswordEmpty);
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                _userDialog.Alert(Strings.ValidLastName);
            }
            else if (!string.Equals(Password, ConfirmPassword))
            {
                _userDialog.Alert(Strings.PasswordsNotSame);
            }
            else if (string.IsNullOrEmpty(VehicleMake))
            {
                _userDialog.Alert("Please Select Vehicle Make");
            }
            else if (string.IsNullOrEmpty(VehicleModel))
            {
                _userDialog.Alert("Please Select Vehicle Model");
            }
            else if (string.IsNullOrEmpty(VehicleColor))
            {
                _userDialog.Alert("Please Select Vehicle Color");
            }
            else
            {
                //CustomerSignUp customerSignUp = new CustomerSignUp();

                //customerSignUp.emailId = signUpEmail;
                //customerSignUp.mobileNumber = signUpMobile;
                //customerSignUp.passwordHash = signUpPassword;
                //customerSignUp.createdDate = createdDate ;
                
                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

                var result = await AdminService.CheckMailId(EmailAddress);
                if (result.EmailIdExist)
                {
                    _userDialog.Alert("EmailId Already Exists");
                }
                else
                {
                    
                    signUpRequest.client = new Client();
                    signUpRequest.clientAddress = new List<ClientAddress>();
                    signUpRequest.clientVehicle = new List<ClientVehicle>();
                    signUpRequest.client.clientId = 0;
                    signUpRequest.client.firstName = FirstName;
                    signUpRequest.client.middleName = null;
                    signUpRequest.client.lastName = LastName;
                    signUpRequest.client.gender = null;
                    signUpRequest.client.maritalStatus = null;
                    signUpRequest.client.birthDate = null;
                    signUpRequest.client.notes = string.Empty;
                    signUpRequest.client.recNotes = string.Empty;
                    signUpRequest.client.clientType = 82;
                    signUpRequest.client.isActive = true;
                    signUpRequest.client.isDeleted = false;
                    signUpRequest.client.createdDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    signUpRequest.client.updatedDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    signUpRequest.client.createdBy = 0;
                    signUpRequest.client.updatedBy = 0;

                    clientAddress.clientAddressId = 0;
                    clientAddress.clientId = 0;
                    clientAddress.address1 = null;
                    clientAddress.address2 = null;
                    clientAddress.phoneNumber = PhoneNumber;
                    clientAddress.phoneNumber2 = null;
                    clientAddress.email = EmailAddress;
                    clientAddress.state = null;
                    clientAddress.country = null;
                    clientAddress.zip = null;
                    clientAddress.isActive = true;
                    clientAddress.isDeleted = false;
                    clientAddress.createdDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    clientAddress.updatedDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    clientAddress.createdBy = 0;
                    clientAddress.updatedBy = 0;
                    signUpRequest.clientAddress.Add(clientAddress);

                    clientVehicle.vehicleId = 0;
                    clientVehicle.locationId = null;
                    clientVehicle.vehicleNumber = string.Empty;
                    clientVehicle.vehicleMfr = MembershipDetails.vehicleMakeNumber;
                    clientVehicle.vehicleModel = (int)MembershipDetails.modelNumber;
                    clientVehicle.vehicleModelNo = null;
                    clientVehicle.vehicleYear = null;
                    clientVehicle.vehicleColor = MembershipDetails.colorNumber;
                    clientVehicle.upcharge = null;
                    clientVehicle.barcode = null;
                    clientVehicle.notes = null;
                    clientVehicle.isActive = true;
                    clientVehicle.isDeleted = false;
                    clientVehicle.createdDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    clientVehicle.updatedDate = (DateTime.Now).ToString("MM/dd/yyy hh:mm:ss tt");
                    clientVehicle.createdBy = 0;
                    clientVehicle.updatedBy = 0;
                    signUpRequest.clientVehicle.Add(clientVehicle);

                    signUpRequest.password = Password;
                    signUpRequest.token = "0A7E0CAA-DA62-4BF8-B83A-3F6625CDD6DE";


                    Console.WriteLine(signUpRequest);

                    //_userDialog.Alert(JsonConvert.SerializeObject(signUpRequest));
                    try
                    {
                        _userDialog.ShowLoading("Creating Account");
                        var signUpResponse = await AdminService.CustomerSignUp(signUpRequest);

                        _userDialog.HideLoading();
                        if (signUpResponse.Status.Count > 0)
                        {
                            if (platform == DevicePlatform.iOS)
                            {
                                _userDialog.Toast(Strings.SignUpSuccessful);
                                await _navigationService.Close(this);
                            }
                            else
                            {
                                await _navigationService.Close(this);
                                await Task.Delay(300);
                                _userDialog.Toast(Strings.SignUpSuccessful);
                            }
                        }
                        else
                        {
                            _userDialog.Toast(Strings.SignUpUnSuccessful);
                        }
                    }
                    catch(Exception e)
                    {
                        _userDialog.Alert("Exception - " + e.Message);
                    }

                    
                }
               
            }
        }
        public async Task NavigateToLogin()
        {
            await _navigationService.Navigate<LoginViewModel>();
            //await _navigationService.Close(this);
        }
        public async Task getVehicleDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);

            vehicleCodes = await AdminService.GetVehicleCodes();
            foreach (var data in vehicleCodes.VehicleDetails)
            {
                if (data.Category == "VehicleManufacturer")
                {
                    manufacturerName.Add(data.CodeId, data.CodeValue);
                }
                else if (data.Category == "VehicleModel")
                {
                    modelName.Add(data.CodeId, data.CodeValue);
                }
                else if (data.Category == "VehicleColor")
                {
                    ColorList.Add(data.CodeId, data.CodeValue);
                }
                else
                {
                    break;
                }
            }
            if (vehicleCodes == null)
            {
                _userDialog.HideLoading();
            }
            _userDialog.HideLoading();
        }
        public async Task GetMakeList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            makeList = await AdminService.GetMakeListCommon();
            colorList = await AdminService.GetColorListCommon();
            foreach (var color in colorList.Color)
            {
                ColorList.Add(color.ColorId, color.ColorValue);
            }
            _userDialog.HideLoading();
        }
        public async Task GetModelList(string selectedMake)
        {
            _userDialog.ShowLoading(Strings.Loading);

            foreach (var item in makeList.Make)
            {
                string selectedMake1 = selectedMake.Replace(" ", "");
                if (selectedMake1 == item.MakeValue)
                {
                    var result = await AdminService.GetModelListCommon(item.MakeId);
                    if (result.Model != null)
                    {
                        modelList = result;
                    }
                }
            }
           
            
            _userDialog.HideLoading();
        }

        #endregion Commands

        #region Properties

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LastName { get; set; } 
        public string VehicleMake { get; set; } 
        public string VehicleModel { get; set; } 
        public string VehicleColor { get; set; }
       
        public string createdDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
        public string userGuid { get; set; }
        public int emailVerified { get; set; } 
        public string SignUp
        {
            get 
            {
                return Strings.SignUp;
            }
            set { }
        }

        #endregion Properties

    }
}

using Acr.UserDialogs;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Rest.Implementations;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class LoginViewModel : BaseViewModel
    {
       
        public LoginViewModel()
        {

        }
        

        #region Commands

        public async void ForgotPasswordCommand()
        {
            await _navigationService.Navigate<ForgotPasswordViewModel>();
        }

        public async Task DoLoginCommand()
        {
            isExitApp = false;
            if (validateCommand())
            {

                _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
                var loginResponse = await AdminService.EmployeeLogin( new EmployeeLoginRequest(loginEmailPhone, loginPassword));
                if (loginResponse != null)
                {
                    ApiUtils.Token = loginResponse.Token;
                    MessengerTempData.FirstName = loginResponse.EmployeeDetails.EmployeeLogin.Firstname;
                    MessengerTempData.LastName = loginResponse.EmployeeDetails.EmployeeLogin.LastName;
                    EmployeeTempData.EmployeeID = loginResponse.EmployeeDetails.EmployeeLogin.EmployeeId;
                    EmployeeTempData.LocationId = loginResponse.EmployeeDetails.EmployeeLocations[0].LocationId;
                    EmployeeTempData.LocationName = loginResponse.EmployeeDetails.EmployeeLocations[0].LocationName;
                    EmployeeTempData.employeeLocationdata = loginResponse.EmployeeDetails.EmployeeLocations;                    
                    if (!string.IsNullOrEmpty(loginResponse.Token))
                    {
                        await _navigationService.Navigate<DashboardViewModel>();
                    }
                }
                else
                {
                    _userDialog.Alert(Strings.UsernamePasswordIncorrect);
                }
                _userDialog.HideLoading();
            }
            //else
            //{
            //    _userDialog.Alert(Strings.UsernamePasswordIncorrect);
            //}


        }
        public bool validateCommand()
        {
            bool isValid;

            if (Validations.validateEmail(loginEmailPhone)
                || Validations.validatePhone(loginEmailPhone))
            {
                isValid = true;
            }
            if (String.IsNullOrEmpty(loginEmailPhone)
                || String.IsNullOrEmpty(loginPassword))
            {
                isValid = false;
                _userDialog.Alert("Please provide all the details");
            }
            else 
            {
                isValid = true;
               // _userDialog.Alert("Please enter a valid UserName ");
            }
            return isValid;
        }

        public void RememberMeButtonCommand()
        {
            rememberMe = !rememberMe;
        }

        #endregion Commands



        #region Properties

        public string loginEmailPhone { get; set; }
        public string loginPassword { get; set; }
        public bool rememberMe { get; set; }
        public bool terms { get; set; }
        public static string ConnectionID;
        public string Title
        {
            get
            {
                return Strings.CUSTOMER_APP_TITLE;
            }
            set { }
        }

        public string Login
        {
            get
            {
                return Strings.Login;
            }
            set { }
        }

        public string RememberPassword
        {
            get
            {
                return Strings.RememberPassword;
            }
            set { }
        }

        public string ForgotPassword
        {
            get
            {
                return Strings.ForgotPassword_loginScreen;
            }
            set { }
        }

        public string NewAccount
        {
            get
            {
                return Strings.NewAccount;
            }
            set { }
        }

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


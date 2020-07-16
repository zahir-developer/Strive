using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class OTPViewModel : BaseViewModel
    {
        #region Commands
        #endregion Commands

        #region Properties

        public string EnterOTP
        {
            get
            {
                return Strings.EnterOTP;
            }
            set { }
        }
        public string SentOTP
        {
            get
            {
                return Strings.OTPSent;
            }
            set { }
        }
        public string NotReceiveOTP
        {
            get 
            {
                return Strings.notReceiveOTP;
            }
            set { }
        }
        public string ResendOTP
        {
            get
            {
                return Strings.ResendOTP;
            }
            set { }
        }
        public string VerifyOTP
        {
            get
            {
                return Strings.OTPVerify;
            }
            set { }
        }

        #endregion Properties



    }
}

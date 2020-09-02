﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public static class CustomerInfo
    {
        #region properties

        public static string resetEmail { get; set; }
        public static string OTP { get; set; }
        public static string selectedMilesOption { get; set; }
        public static int notifyRadius { get; set; }
        public static DateTime notifyWashTime { get; set; }

        #endregion properties


        #region commands

        public static void Clear()
        {
            resetEmail = "";
            OTP = "";
        }
        public static void setMapInfo()
        {
            switch(selectedMilesOption)
            {
                case "A":
                    notifyRadius = 1609;
                    break;

                case "B":
                    notifyRadius = 1609;
                    break;

                case "C":
                    notifyRadius = 804;
                    break;
            }
        }

        #endregion commands
    }
}

using Strive.Core.Models.TimInventory;
using System;
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
        public static int selectedDeal { get; set; } = -1;
        public static int selectedMemberShip { get; set; }
        public static int selectedUpCharge { get; set; }
        public static CustomerPersonalInfo customerPersonalInfo {get; set;}
        public static int vehicleMakeNumber { get; set; }
        public static string vehicleMakeName { get; set; }
        public static int colorNumber { get; set; }
        public static string colorName { get; set; }
        public static int modelNumber { get; set; }
        public static string modelName { get; set; }
        public static SelectedServiceList membershipType ; 
        public static ServiceList filteredList { get; set; }
        #endregion properties


        #region commands

        public static void Clear()
        {
            resetEmail = "";
            OTP = "";
            selectedMilesOption = "";
            notifyRadius = 0;
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

                default:
                    notifyRadius = 1609;
                    break;
            }
        }

        #endregion commands
    }
}

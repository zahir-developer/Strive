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
        public static CustomerPersonalInfo customerPersonalInfo { get; set; }
        public static int ClientID { get; set; } = 82;
        public static int vehicleMakeNumber { get; set; }
        public static string vehicleMakeName { get; set; }
        public static int colorNumber { get; set; }
        public static string colorName { get; set; }
        public static int modelNumber { get; set; }
        public static string modelName { get; set; }
        public static SelectedServiceList membershipType;
        public static int actionType { get; set; } = 0;
        public static PastClientServices pastClientServices { get; set; }
        public static int SelectedVehiclePastDetails {get; set;}
        public static List<float> TotalCost { get; set; }

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
            switch (selectedMilesOption)
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
    public static class MembershipDetails
    {
        #region MembershipProperties

        public static int selectedColor { get; set; }
        public static int selectedModel { get; set; }
        public static int selectedMake { get; set; }
        public static int selectedMembership { get; set; }
        public static int selectedUpCharge { get; set; }
        public static int vehicleMakeNumber { get; set; }
        public static string vehicleMakeName { get; set; }
        public static int colorNumber { get; set; }
        public static string colorName { get; set; }
        public static int modelNumber { get; set; }
        public static string modelName { get; set; }
        public static List<int> selectedAdditionalServices;
        public static ServiceList filteredList { get; set; }
        public static ClientVehicleRoot customerVehicleDetails { get; set; }
        public static List<ClientVehicleMembershipService> selectedMembershipServices { get; set; }
        public static int clientVehicleID { get; set; }

        #endregion MembershipProperties

        #region MembershipCommands

        public static void clearMembershipData()
        {
            selectedColor = 0;
            selectedModel = 0;
            selectedMake = 0;
            selectedUpCharge = 0;
            selectedMembership = 0;
            vehicleMakeNumber = 0;
            modelNumber = 0;
            vehicleMakeName = null;
            colorNumber = 0;
            modelName = null;
            colorName = null;
            filteredList = null;
            customerVehicleDetails = null;
            selectedAdditionalServices = null;
            customerVehicleDetails = null;
            selectedMembershipServices = null;
        }

        #endregion MembershipCommands

    }

    public static class CustomerVehiclesInformation
    {
        #region Properties

        public static VehicleList vehiclesList { get; set; }
        public static int selectedVehicleInfo { get; set; }
        public static ClientVehicleRootView completeVehicleDetails { get; set; }
        public static ClientVehicleRoot membershipDetails { get; set; }

        #endregion Properties

        #region Commands

        #endregion Commands
    }
}

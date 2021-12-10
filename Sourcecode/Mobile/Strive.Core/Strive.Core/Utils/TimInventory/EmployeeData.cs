using System;
using Strive.Core.Models.TimInventory;
using System.Collections.Generic;
using Strive.Core.Models.Customer.Schedule;

namespace Strive.Core.Utils.TimInventory
{
    public static class EmployeeData
    {
        public static EmployeeDetails EmployeeDetails { get; set; }

        public static string CurrentRole { get; set; }

        public static int SelectedRoleId { get; set; }

        public static TimeClockRoot ClockInStatus { get; set; }

        public static string ClockInTime { get; set; }

        public static InventoryDataModel EditableProduct { get; set; }

        public static Vendors Vendors { get; set; }

        public static string ViewSourceUrl { get; set; }

        public static bool load { get; set; }

        public static int selectedLocationId { get; set; }
    }

    public static class MembershipData
    {
        public static MembershipServiceList MembershipServiceList { get; set; }

        public static MembershipServices SelectedMembership { get; set; }

        public static ClientDetail SelectedClient { get; set; }

        public static VehicleMembershipDetailsView MembershipDetailView { get; set; }

        public static VehicleDetail SelectedVehicle { get; set; }

        public static List<ClientVehicleMembershipService> ExtraServices { get; set; }

        public static List<AllServiceDetail> AllAdditionalServices { get; set; }
    }
}

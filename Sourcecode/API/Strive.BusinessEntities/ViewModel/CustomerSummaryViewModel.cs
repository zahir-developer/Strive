using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CustomerSummaryViewModel
    {
        public int Month { get; set; }
        public int? NumberOfMembershipAccounts { get; set; }
        public int? CustomerCount { get; set; }
        public int? VehicleCount { get; set; }
        public int? WashesCompletedCount { get; set; }
        public decimal? AverageNumberOfWashesPerCustomer { get; set; }
        public decimal? AverageNumberOfWashesPerVehicle { get; set; }
        public decimal? TotalNumberOfWashesPerCustomer { get; set; }
        public decimal? TotalNumberOfWashesPerVehicle { get; set; }
        public decimal? PercentageOfCustomersThatTurnedUp { get; set; }
        public decimal? PercentageOfVehicleThatTurnedUp { get; set; }


    }
}

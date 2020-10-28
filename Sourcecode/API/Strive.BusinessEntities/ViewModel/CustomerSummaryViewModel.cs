using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CustomerSummaryViewModel
    {
        public int? NumberOfMembershipAccounts { get; set; }
        public int? CustomerCount { get; set; }
        public int? NumberOfCompletedWashes { get; set; }
        public int? WashesCompletedCount { get; set; }
        public float? AverageNumberOfWashesPerCustomer { get; set; }
        public float? TotalNumberOfWashesPerCustomer { get; set; }
        public float? PercentageOfCustomersThatTurnedUp { get; set; }


    }
}

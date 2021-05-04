using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CuatomerHistoryViewModel
    {
        public int JobId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string TicketNumber { get; set; }
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VehicleId { get; set; }
        public string MembershipName { get; set; }
        public string BarCode { get; set; }
        public string AdditionalServices { get; set; }
        public string Services { get; set; }
        

    }
}

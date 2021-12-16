using Strive.BusinessEntities.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientEmailBlastViewModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ClientType { get; set; }
        public string Type { get; set; }
        public int ClientMembershipId { get; set; }
        public int VehicleId { get; set; } 
        public bool IsActive { get; set; }
        public bool IsMembership { get; set; }
        public string barcode { get; set; }
    }
}

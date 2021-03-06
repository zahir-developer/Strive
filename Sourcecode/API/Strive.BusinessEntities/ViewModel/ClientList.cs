using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientList
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }
        public string RecNotes { get; set; }
        public int Score { get; set; }
        public bool NoEmail { get; set; }
        public int ClientType { get; set; }
        public int ClientAddressId { get; set; }
        public int ClientRelatioshipId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
    }
}

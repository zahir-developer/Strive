using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Client
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string RecNotes { get; set; }
        public int Score { get; set; }
        public bool NoEmail { get; set; }
        public int ClientType { get; set; }
        //public List<ClientVehicle> ClientVehicle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientNameViewModel
    {
        public int ClientId { get; set; }
        public int? VehicleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }
}

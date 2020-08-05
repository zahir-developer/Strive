using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Location
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public int LocationAddressId { get; set; }
        public int LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        public string LocationName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WorkhourThreshold { get; set; }
        public bool IsActive { get; set; }

    }
}

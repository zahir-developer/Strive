using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class LocationWashTimeDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int WashtimeMinutes { get; set; }
        public string StoreStatus { get; set; }
        public DateTimeOffset StoreTimeIn { get; set; }
        public DateTimeOffset StoreTimeOut { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        

    }
}

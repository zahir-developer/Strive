using Strive.BusinessEntities.DTO.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class LocationDescriptionViewModel
    {
        public LocationDetailViewModel Location { get; set; }
        public LocationAddressViewModel LocationAddress { get; set; }
        public List<LocationEmailViewModel> LocationEmail { get; set; }
        public DrawerViewModel Drawer { get; set; }
        public LocationOffsetViewModel LocationOffset { get; set; }
        public List<MerchantDetailViewModel> MerchantDetail { get; set; }
    }
}

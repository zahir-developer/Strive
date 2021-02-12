using Strive.BusinessEntities.DTO.ServiceSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ServiceListViewModel
    {
        public List<GetAllServiceViewModel> getAllServiceViewModel { get; set; }

        public CountViewModel Count { get; set; }

    }
}

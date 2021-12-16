using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailViewModel
    {
        public DetailsJobViewModel Details { get; set; }
        public List<DetailsJobItemViewModel> DetailsItem { get; set; }
        public List<DetailsJobServiceEmployeeViewModel> DetailsJobServiceEmployee { get; set; }
        public EmailViewModel Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class AdSetupDetailViewModel
    {
        public int AdSetupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? DocumentId { get; set; }
        public string Base64 { get; set; }
    }
}

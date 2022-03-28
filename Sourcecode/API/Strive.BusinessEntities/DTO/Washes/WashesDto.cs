using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Washes
{
    public class WashesDto
    {
        public Model.Job Job { get; set; }
        public List<JobItem> JobItem { get; set; }
        public string DeletedJobItemId { get; set; }
        public bool? isMobileApp { get; set; }
    }
}

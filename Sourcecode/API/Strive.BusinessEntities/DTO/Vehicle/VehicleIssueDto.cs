using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Vehicle
{
    public class VehicleIssueDto
    {
        public VehicleIssue VehicleIssue { get; set; }

        public List<VehicleIssueImage> VehicleIssueImage { get; set; }
    }
}

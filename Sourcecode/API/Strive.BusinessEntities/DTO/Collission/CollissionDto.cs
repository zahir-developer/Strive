using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Collision
{
    public class CollissionDto
    {
        public Model.EmployeeLiability EmployeeLiability { get; set; }
        public List<EmployeeLiabilityDetail> EmployeeLiabilityDetail { get; set; }
    }
}

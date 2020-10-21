using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PayRoll
{
    public class PayRollUpdateDto
    {
        public int LiabilityId { get; set; }
        public decimal Amount { get; set; }
    }
}

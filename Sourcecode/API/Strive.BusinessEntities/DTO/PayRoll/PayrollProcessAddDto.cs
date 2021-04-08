using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PayRoll
{
   public class PayrollProcessAddDto
    {
        public PayrollProcess PayrollProcess { get; set; }
        public List<PayrollEmployee> PayrollEmployee { get; set; }
    }
}

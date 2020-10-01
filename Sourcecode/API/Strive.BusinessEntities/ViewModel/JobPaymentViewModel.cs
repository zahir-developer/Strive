using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class JobPaymentViewModel
    {
        public int JobId { get; set; }
        public int JobPaymentId { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsRollBack { get; set; }
    }
}

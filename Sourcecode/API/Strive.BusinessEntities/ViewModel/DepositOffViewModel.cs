using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
  public  class DepositOffViewModel
    {
       
        public string Manager { get; set; }
        public DateTime? JobDate { get; set; }
        public decimal Difference { get; set; }
    }
}

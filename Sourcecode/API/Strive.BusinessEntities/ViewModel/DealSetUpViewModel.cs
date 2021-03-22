using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DealSetUpViewModel
    {
        public int DealId { get; set; }

      
        public string DealName { get; set; }

     
        public int TimePeriod { get; set; }

      
        public bool Deals { get; set; }

       
        public DateTime StartDate { get; set; }
      
        public DateTime EndDate { get; set; }

    }
}

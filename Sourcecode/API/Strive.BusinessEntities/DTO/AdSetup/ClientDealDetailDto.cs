using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.AdSetup
{
    public class ClientDealDetailDto
    {
        public int ClientDealId { get; set; }
        
        public int? DealId { get; set; }

        public string DealName { get; set; }

        public int? ClientId { get; set; }

        public string DealCount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Client
{
    public class ClientAmountUpdateDto
    {
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}

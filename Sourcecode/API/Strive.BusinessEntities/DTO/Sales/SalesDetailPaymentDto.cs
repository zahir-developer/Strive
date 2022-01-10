using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesPaymentDetailDto
    {
        public SalesPaymentDto SalesPaymentDto { get; set; }
        public SalesProductItemDto SalesProductItemDto { get; set; }
        public int LocationId { get; set; }
        public string TicketNumber { get; set; }
    }
}
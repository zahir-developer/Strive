using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class DeleteItemDto
    {
        public int ItemId { get; set; }

        public bool IsJobItem { get; set; } 
    }
}

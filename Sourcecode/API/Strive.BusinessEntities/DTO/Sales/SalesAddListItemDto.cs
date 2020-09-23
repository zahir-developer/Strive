using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesAddListItemDto
    {
        public Job Job { get; set; }
        public JobItem JobItem { get; set; }
        public JobProductItem JobProductItem { get; set; }
    }
}

using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesUpdateItemDto
    {
        public List<JobItem> JobItem { get; set; }
        public JobProductItem JobProductItem { get; set; }
    }
}

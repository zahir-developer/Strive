using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesProductItemDto
    {
        public List<JobProductViewModel> JobProductItem { get; set; }
    }
}

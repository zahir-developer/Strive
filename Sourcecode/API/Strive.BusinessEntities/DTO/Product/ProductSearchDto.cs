using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Product
{
    public class ProductSearchDto
    {
        public string ProductSearch { get; set; }

        public string ProductTypeNames { get; set; }

        public bool? Status { get; set; }

        public bool? LoadThumbnailImage { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Product
{
    public class ProductDescriptionViewModel
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.GiftCard
{
    public class GiftCardDto
    {
        public Model.GiftCard GiftCard { get; set; }
        public List<GiftCardHistoryDto> GiftCardHistory { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleItemSummaryViewModel
    {
        public decimal? Cash { get; set; }
        public decimal? Credit { get; set; }
        public decimal? CashBack { get; set; }
        public decimal? GiftCard { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? TotalPaid { get; set; }
        public bool? IsProcessed { get; set; }
    }
}

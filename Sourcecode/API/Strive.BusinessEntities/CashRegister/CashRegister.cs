using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegister
    {
        public int CashRegisterId { get; set; }
        public int? CashRegisterType { get; set; }
        public int? LocationId { get; set; }
        public int? DrawerId { get; set; }
        public int? UserId { get; set; }

        public int? CashRegCoinId { get; set; }
        public int? CashRegRollId { get; set; }
        public int? CashRegBillId { get; set; }
        public int? CashRegOtherId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? EnteredDateTime { get; set; }
    }
}


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
        public DateTime? EnteredDateTime { get; set; }
        public int? CashRegisterCoinId { get; set; }
        public int? CashRegisterBillId { get; set; }
        public int? CashRegisterRollId { get; set; }
        public int? CashRegisterOtherId { get; set; }

    }
}


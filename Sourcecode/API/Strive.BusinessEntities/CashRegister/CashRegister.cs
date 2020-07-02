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
        [Column(TypeName = "datetime")]
        public DateTime? EnteredDateTime { get; set; }
        public List<CashRegisterCoin> Coins { get; set; }
        public List<CashRegisterBill> Bills { get; set; }
        public List<CashRegisterRoll> Rolls { get; set; }
        public List<CashRegisterOther> Others { get; set; }

    }
}

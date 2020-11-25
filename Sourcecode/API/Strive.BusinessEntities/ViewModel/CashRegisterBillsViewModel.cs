using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class CashRegisterBillsViewModel
    {
        public int CashRegBillId { get; set; }
        
        public int CashRegisterId { get; set; }
        
        public decimal s1 { get; set; }                
        public decimal s5 { get; set; }                
        public decimal s10 { get; set; }                
        public decimal s20 { get; set; }                
        public decimal s50 { get; set; }                
        public decimal s100 { get; set; }
    }
}

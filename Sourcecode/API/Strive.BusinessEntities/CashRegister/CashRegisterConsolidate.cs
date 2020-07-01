using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterConsolidate
    {
        public long CashRegisterId { get; set; }
        public long CashRegisterType { get; set; }
        public List<Location> TblLocation { get; set; }
        //public List<tblDrawer> TblDrawer { get; set; }
    }
}

using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class JobProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int Quantity { get; set; }
    }
}

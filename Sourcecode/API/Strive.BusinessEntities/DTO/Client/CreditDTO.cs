using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Client
{
    public class CreditDTO
    {
        public Model.CreditAccount CreditAccount { get; set; }
        public Model.CreditAccountHistory CreditAccountHistory { get; set; }
    }
}

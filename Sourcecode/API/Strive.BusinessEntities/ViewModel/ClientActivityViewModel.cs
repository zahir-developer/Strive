using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientActivityViewModel
    {
        public int? CreditAccountId { get; set; }
        public int? ClientId { get; set; }
        public decimal? Amount { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}

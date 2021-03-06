using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class GiftCardDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       public int GiftCardId { get; set; }
        public int LocationId { get; set; }
        public string GiftCardCode { get; set; }
        public string GiftCardName { get; set; }
        public DateTimeOffset ActivationDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

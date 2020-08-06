using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.GiftCard
{
    public class GiftCard
    {
        public int GiftCardId { get; set; }
        public int LocationId { get; set; }
        public string GiftCardCode { get; set; }
        public string GiftCardName { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

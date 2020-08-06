using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.GiftCard
{
    public class GiftCardView : GiftCard
    {
        public List<GiftCardHistory> GiftCardHistory { get; set; }
    }
}

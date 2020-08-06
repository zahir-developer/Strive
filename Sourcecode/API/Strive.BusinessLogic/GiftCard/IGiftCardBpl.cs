using Strive.BusinessEntities.DTO.GiftCard;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.GiftCard
{
    public interface IGiftCardBpl
    {
        Result GetAllGiftCard(int locationId);
        Result GiftCardDetailByGiftCardId(int giftCardId);
        Result ActivateorDeactivateGiftCard(GiftCardStatus giftCard);
        Result SaveGiftCard(GiftCardView giftCard);
    }
}

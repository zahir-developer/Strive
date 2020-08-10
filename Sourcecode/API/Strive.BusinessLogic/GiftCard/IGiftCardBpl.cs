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
        Result GetAllGiftCardHistory(int giftCardId);
        Result GetGiftCardByGiftCardId(int giftCardId);
        Result ActivateorDeactivateGiftCard(GiftCardStatus giftCard);
        Result AddGiftCard(GiftCardDto addGiftCard);
        Result UpdateGiftCard(GiftCardDto updateGiftCard);
        Result AddGiftCardHistory(GiftCardHistoryDto addGiftCardHistory);
        Result UpdateGiftCardHistory(GiftCardHistoryDto updateGiftCardHistory);
    }
}

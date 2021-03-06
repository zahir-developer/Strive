using Strive.BusinessEntities.DTO;
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
        Result GetAllGiftCardByLocation(int locationId);
        //Result GetAllGiftCardHistory(int giftCardId);
        Result GetAllGiftCardHistory(string giftCardNumber);
        Result GetGiftCardByGiftCardId(string giftCardNumber);
        Result GetGiftCardHistoryByNumber(string giftCardNumber);
        Result ActivateorDeactivateGiftCard(GiftCardStatus giftCard);
        Result AddGiftCard(GiftCardDto addGiftCard);
        Result UpdateGiftCard(GiftCardDto updateGiftCard);
        Result AddGiftCardHistory(GiftCardHistoryDto addGiftCardHistory);
        Result UpdateGiftCardHistory(GiftCardHistoryDto updateGiftCardHistory);
        Result GetGiftCardBalance(string giftCardNumber);

        Result GetAllGiftCard(SearchDto searchDto);
        Result DeleteGiftCard(int id);
        Result IsGiftCardExist(string giftCardCode);

        Result GetGiftCardBalanceHistory(string giftCardNumber);
    }
}

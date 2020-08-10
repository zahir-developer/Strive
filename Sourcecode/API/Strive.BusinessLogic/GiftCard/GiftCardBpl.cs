using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.GiftCard;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.GiftCard
{
    public class GiftCardBpl : Strivebase, IGiftCardBpl
    {
        public GiftCardBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        public Result GetAllGiftCard(int locationId)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetAllGiftCard,locationId, "GiftCard");
        }

        public Result GetGiftCardByGiftCardId(int giftCardId)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetGiftCardByGiftCardId, giftCardId, "GiftCardDetail");
        }
        public Result GetAllGiftCardHistory(int giftCardId)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetAllGiftCardHistory, giftCardId, "GiftCardHistory");
        }
        public Result ActivateorDeactivateGiftCard(GiftCardStatus giftCard)
        {
            return ResultWrap(new GiftCardRal(_tenant).ActivateorDeactivateGiftCard, giftCard, "ChangeStatus");
        }
        public Result AddGiftCard(GiftCardDto giftCardDto)
        {
            return ResultWrap(new GiftCardRal(_tenant).AddGiftCard, giftCardDto, "Status");
        }

        public Result UpdateGiftCard(GiftCardDto giftCardDto)
        {
            return ResultWrap(new GiftCardRal(_tenant).UpdateGiftCard, giftCardDto, "Status");
        }
        public Result AddGiftCardHistory(GiftCardHistoryDto giftCardHistoryDto)
        {
            return ResultWrap(new GiftCardRal(_tenant).AddGiftCardHistory, giftCardHistoryDto, "Status");
        }

        public Result UpdateGiftCardHistory(GiftCardHistoryDto giftCardHistoryDto)
        {
            return ResultWrap(new GiftCardRal(_tenant).UpdateGiftCardHistory, giftCardHistoryDto, "Status");
        }
    }
}

﻿using Microsoft.Extensions.Caching.Distributed;
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
        public Result GetAllGiftCardByLocation(int locationId)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetAllGiftCardByLocation, locationId, "GiftCard");
        }

        public Result GetGiftCardByGiftCardId(string giftCardNumber)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetGiftCardByGiftCardId, giftCardNumber, "GiftCardDetail");
        }
        public Result GetGiftCardHistoryByNumber(string giftCardNumber)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetGiftCardHistoryByNumber, giftCardNumber, "GiftCardDetail");
        }
        
        public Result GetAllGiftCardHistory(string giftCardNumber)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetAllGiftCardHistory, giftCardNumber, "GiftCardHistory");
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
        public Result GetGiftCardBalance(string giftCardNumber)
        {
            return ResultWrap(new GiftCardRal(_tenant).GetGiftCardBalance, giftCardNumber, "GiftCardDetail");
        }

        public Result GetAllGiftCard()
        {
            return ResultWrap(new GiftCardRal(_tenant).GetAllGiftCard, "GiftCard");
        }

        public Result DeleteGiftCard(int id)
        {
            return ResultWrap(new GiftCardRal(_tenant).DeleteGiftCard,id, "GiftCard");
        }
    }
}

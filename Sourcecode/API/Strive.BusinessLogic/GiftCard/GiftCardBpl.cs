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
            try
            {
                var lstGiftCardById = new GiftCardRal(_tenant).GetAllGiftCard(locationId);
                _resultContent.Add(lstGiftCardById.WithName("GiftCard"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GiftCardDetailByGiftCardId(int giftCardId)
        {
            try
            {
                var lstGiftCardById = new GiftCardRal(_tenant).GiftCardDetailByGiftCardId(giftCardId);
                _resultContent.Add(lstGiftCardById.WithName("GiftCardDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result ActivateorDeactivateGiftCard(GiftCardStatus giftCard)
        {
            try
            {
                var blnStatus = new GiftCardRal(_tenant).ActivateorDeactivateGiftCard(giftCard);

                _resultContent.Add(blnStatus.WithName("StatusChange"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveGiftCard(GiftCardView giftCard)
        {
            try
            {
                var blnStatus = new GiftCardRal(_tenant).SaveGiftCard(giftCard);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
    }
}

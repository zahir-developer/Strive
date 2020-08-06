using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.GiftCard;
using Strive.BusinessLogic.GiftCard;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class GiftCardController : ControllerBase
    {
        IGiftCardBpl _giftCardBpl = null;

        public GiftCardController(IGiftCardBpl giftCardBpl)
        {
            _giftCardBpl = giftCardBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllGiftCard(int locationId)
        {
            return _giftCardBpl.GetAllGiftCard(locationId);
        }

        [HttpGet]
        [Route("GiftCardDetail")]
        public Result GiftCardDetailByGiftCardId(int giftCardId)
        {
            return _giftCardBpl.GiftCardDetailByGiftCardId(giftCardId);
        }
        [HttpPost]
        [Route("ChangeStatus")]
        public Result ActivateorDeactivateGiftCard([FromBody] GiftCardStatus giftCard)
        {
            return _giftCardBpl.ActivateorDeactivateGiftCard(giftCard);
        }
        [HttpPost]
        [Route("AddGiftCard")]
        public Result SaveGiftCard([FromBody] GiftCardView giftCard)
        {
            return _giftCardBpl.SaveGiftCard(giftCard);
        }
    }
}

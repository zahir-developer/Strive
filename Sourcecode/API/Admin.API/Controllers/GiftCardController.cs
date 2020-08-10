using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Collision;
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
    public class GiftCardController : StriveControllerBase<IGiftCardBpl>
    {
        public GiftCardController(IGiftCardBpl giftBpl) : base(giftBpl) { }
        #region GET
        [HttpGet]
        [Route("GetAllGiftCard/{locationId}")]
        public Result GetAllGiftCard(int locationId) => _bplManager.GetAllGiftCard(locationId);
        #endregion
        #region GET
        [HttpGet]
        [Route("GetAllGiftCard/{giftCardId}")]
        public Result GetGiftCardByGiftCardId(int giftCardId) => _bplManager.GetGiftCardByGiftCardId(giftCardId);
        #endregion
        #region GET
        [HttpGet]
        [Route("GetAllGiftCardHistory/{locationId}")]
        public Result GetAllGiftCardHistory(int giftCardId) => _bplManager.GetAllGiftCardHistory(giftCardId);
        #endregion
        #region
        [HttpPost]
        [Route("ChangeStatus")]
        public Result ActivateorDeactivateGiftCard([FromBody] GiftCardStatus giftCard) => _bplManager.ActivateorDeactivateGiftCard(giftCard);
        #endregion
        #region
        [HttpPost]
        [Route("AddGiftCard")]
        public Result AddGiftCard([FromBody] GiftCardDto addGiftCard) => _bplManager.AddGiftCard(addGiftCard);
        #endregion
        #region
        [HttpPost]
        [Route("UpdateGiftCard")]
        public Result UpdateGiftCard([FromBody] GiftCardDto updateGiftCard) => _bplManager.UpdateGiftCard(updateGiftCard);
        #endregion
        #region
        [HttpPost]
        [Route("AddGiftCardHistory")]
        public Result AddGiftCardHistory([FromBody] GiftCardHistoryDto addGiftCardHistory) => _bplManager.AddGiftCardHistory(addGiftCardHistory);
        #endregion
        #region
        [HttpPost]
        [Route("UpdateGiftCardHistory")]
        public Result UpdateGiftCardHistory([FromBody] GiftCardHistoryDto updateGiftCardHistory) => _bplManager.UpdateGiftCardHistory(updateGiftCardHistory);
        #endregion
    }
}

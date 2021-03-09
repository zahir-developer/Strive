using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
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
        public Result GetAllGiftCardByLocation(int locationId) => _bplManager.GetAllGiftCardByLocation(locationId);
        #endregion
        #region GET
        [HttpGet]
        [Route("GetGiftCard/{giftCardNumber}")]
        public Result GetGiftCardByGiftCardId(string giftCardNumber) => _bplManager.GetGiftCardByGiftCardId(giftCardNumber);

        [HttpGet]
        [Route("GetGiftCardHistoryByNumber/{giftCardNumber}")]
        public Result GetGiftCardHistoryByNumber(string giftCardNumber) => _bplManager.GetGiftCardHistoryByNumber(giftCardNumber);
        
        #endregion
        #region GET
        [HttpGet]
        [Route("GetAllGiftCardHistory/{giftCardNumber}")]
        public Result GetAllGiftCardHistory(string giftCardNumber) => _bplManager.GetAllGiftCardHistory(giftCardNumber);
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

        [HttpGet]
        [Route("GetGiftCardBalance/{giftCardNumber}")]
        public Result GetGiftCardBalance(string giftCardNumber) => _bplManager.GetGiftCardBalance(giftCardNumber);

        #endregion

        [HttpPost]
        [Route("GetAllGiftCard")]
        public Result GetAllGiftCard([FromBody] SearchDto searchDto) => _bplManager.GetAllGiftCard(searchDto);


        [HttpDelete]
        [Route("Delete")]
        public Result DeleteGiftCard(int id) => _bplManager.DeleteGiftCard(id);

    }
}

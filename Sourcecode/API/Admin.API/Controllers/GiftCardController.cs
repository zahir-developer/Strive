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

        [HttpGet]
        [Route("GetGiftCard/{giftCardNumber}")]
        public Result GetGiftCardByGiftCardId(string giftCardNumber) => _bplManager.GetGiftCardByGiftCardId(giftCardNumber);

        [HttpGet]
        [Route("GetGiftCardHistoryByNumber/{giftCardNumber}")]
        public Result GetGiftCardHistoryByNumber(string giftCardNumber) => _bplManager.GetGiftCardHistoryByNumber(giftCardNumber);
        
        [HttpGet]
        [Route("GetAllGiftCardHistory/{giftCardNumber}")]
        public Result GetAllGiftCardHistory(string giftCardNumber) => _bplManager.GetAllGiftCardHistory(giftCardNumber);

        [HttpGet]
        [Route("GetGiftCardBalance/{giftCardNumber}")]
        public Result GetGiftCardBalance(string giftCardNumber) => _bplManager.GetGiftCardBalance(giftCardNumber);

        [HttpGet]
        [Route("IsGiftCardExist/{giftCardCode}")]
        public Result IsGiftCardExist(string giftCardCode) => _bplManager.IsGiftCardExist(giftCardCode);

        [HttpGet]
        [Route("GetGiftCardBalanceHistory/{giftCardNumber}")]
        public Result GetGiftCardBalanceHistory(string giftCardNumber) => _bplManager.GetGiftCardBalanceHistory(giftCardNumber);
        
        #endregion

        #region POST
        [HttpPost]
        [Route("ChangeStatus")]
        public Result ActivateorDeactivateGiftCard([FromBody] GiftCardStatus giftCard) => _bplManager.ActivateorDeactivateGiftCard(giftCard);

        [HttpPost]
        [Route("AddGiftCard")]
        public Result AddGiftCard([FromBody] GiftCardDto addGiftCard) => _bplManager.AddGiftCard(addGiftCard);

        [HttpPost]
        [Route("UpdateGiftCard")]
        public Result UpdateGiftCard([FromBody] GiftCardDto updateGiftCard) => _bplManager.UpdateGiftCard(updateGiftCard);

        [HttpPost]
        [Route("AddGiftCardHistory")]
        public Result AddGiftCardHistory([FromBody] GiftCardHistoryDto addGiftCardHistory) => _bplManager.AddGiftCardHistory(addGiftCardHistory);

        [HttpPost]
        [Route("UpdateGiftCardHistory")]
        public Result UpdateGiftCardHistory([FromBody] GiftCardHistoryDto updateGiftCardHistory) => _bplManager.UpdateGiftCardHistory(updateGiftCardHistory);

        [HttpPost]
        [Route("GetAllGiftCard")]
        public Result GetAllGiftCard([FromBody] SearchDto searchDto) => _bplManager.GetAllGiftCard(searchDto);
        
        #endregion

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteGiftCard(int id) => _bplManager.DeleteGiftCard(id);

    }
}

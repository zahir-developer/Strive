using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.GiftCard;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class GiftCardRal : RalBase
    {
        public GiftCardRal(ITenantHelper tenant) : base(tenant) { }
        public List<GiftCardDetailsViewModel> GetAllGiftCard(int locationId)
        {
            _prm.Add("@LocationId", locationId);
            return db.Fetch<GiftCardDetailsViewModel>(SPEnum.uspGetGiftCardByLocation.ToString(), _prm);
        }
        //
        public List<GiftCardViewModel> GetGiftCardByGiftCardId(string giftCardNumber)
        {
            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardViewModel>(SPEnum.USPGETALLGIFTCARD.ToString(), _prm);
            return result;
        }
        public List<GiftCardViewModel> GetGiftCardHistoryByNumber(string giftCardNumber)
        {
            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardViewModel>(SPEnum.uspGetGiftCardHistoryByNumber.ToString(), _prm);
            return result;
        }
        //
        public List<GiftCardHistoryViewModel> GetAllGiftCardHistory(string giftCardNumber)
        {

            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardHistoryViewModel>(SPEnum.USPGETGIFTCARDHISTORY.ToString(), _prm);
            return result;
        }
        public bool ActivateorDeactivateGiftCard(GiftCardStatus giftCard)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@GiftCardId", giftCard.GiftCardId);
            dynParams.Add("@IsActive", giftCard.IsActive);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPGIFTCARDCHANGESTATUS.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool AddGiftCard(GiftCardDto giftCardDto)
        {
            return dbRepo.SavePc(giftCardDto, "GiftCardId");
        }
        public bool UpdateGiftCard(GiftCardDto giftCardDto)
        {
            return dbRepo.SavePc(giftCardDto, "GiftCardId");
        }
        public bool AddGiftCardHistory(GiftCardHistoryDto giftCardHistoryDto)
        {
            return dbRepo.SavePc(giftCardHistoryDto, "GiftCardHistoryId");
        }
        public bool UpdateGiftCardHistory(GiftCardHistoryDto giftCardHistoryDto)
        {
            return dbRepo.SavePc(giftCardHistoryDto, "GiftCardHistoryId");
        }
    }
}
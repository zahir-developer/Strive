using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.GiftCard;
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
        public List<GiftCardViewModel> GetAllGiftCard(int locationId)
        {
            return db.Fetch<GiftCardViewModel>(SPEnum.USPGETALLGIFTCARD.ToString(), null);
        }

        public GiftCardViewModel GetGiftCardByGiftCardId(int giftCardId)
        {
            _prm.Add("@GiftCardId", giftCardId);
            var result = db.FetchSingle<GiftCardViewModel>(SPEnum.USPGETALLGIFTCARD.ToString(), _prm);
            return result;
        }
        public GiftCardHistoryViewModel GetAllGiftCardHistory(int giftCardId)
        {

            _prm.Add("@GiftCardId", giftCardId);
            var result = db.GetSingleByFkId<GiftCardHistoryViewModel>(giftCardId, "GiftCardId");
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
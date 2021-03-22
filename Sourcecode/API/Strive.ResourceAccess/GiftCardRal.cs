using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
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
        public List<GiftCardDetailsViewModel> GetAllGiftCardByLocation(int locationId)
        {
            _prm.Add("@LocationId", locationId);
            return db.Fetch<GiftCardDetailsViewModel>(EnumSP.GiftCard.uspGetGiftCardByLocation.ToString(), _prm);
        }
        public List<GiftCardBalanceViewModel> GetGiftCardBalance(string giftCardNumber)
        {
            _prm.Add("@GiftCardNumber", giftCardNumber);
            return db.Fetch<GiftCardBalanceViewModel>(EnumSP.GiftCard.uspGetGiftCardBalance.ToString(), _prm);
        }

        public List<GiftCardViewModel> GetGiftCardByGiftCardId(string giftCardNumber)
        {
            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardViewModel>(EnumSP.GiftCard.USPGETALLGIFTCARD.ToString(), _prm);
            return result;
        }
        public List<GiftCardViewModel> GetGiftCardHistoryByNumber(string giftCardNumber)
        {
            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardViewModel>(EnumSP.GiftCard.uspGetGiftCardHistoryByNumber.ToString(), _prm);
            return result;
        }

        public List<GiftCardHistoryViewModel> GetAllGiftCardHistory(string giftCardNumber)
        {

            _prm.Add("@GiftCardCode", giftCardNumber);
            var result = db.Fetch<GiftCardHistoryViewModel>(EnumSP.GiftCard.USPGETGIFTCARDHISTORY.ToString(), _prm);
            return result;
        }
        public bool ActivateorDeactivateGiftCard(GiftCardStatus giftCard)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@GiftCardId", giftCard.GiftCardId);
            dynParams.Add("@IsActive", giftCard.IsActive);
            CommandDefinition cmd = new CommandDefinition(EnumSP.GiftCard.USPGIFTCARDCHANGESTATUS.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool AddGiftCard(GiftCardDto giftCardDto)
        {
            return dbRepo.InsertPc(giftCardDto, "GiftCardId");
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
        public GiftCardCountViewModel GetAllGiftCard(SearchDto searchDto)
        {

            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@StartDate", searchDto.StartDate);

            _prm.Add("@EndDate", searchDto.EndDate);
            var result = db.FetchMultiResult<GiftCardCountViewModel>(EnumSP.GiftCard.USPGETALLGIFTCARDS.ToString(), _prm);
            return result;

        }

        public bool DeleteGiftCard(int id)
        {
            _prm.Add("GiftCardId", id.toInt());
            db.Save(EnumSP.GiftCard.USPDELETEGIFTCARD.ToString(), _prm);
            return true;
        }
        public bool IsGiftCardExist(string giftCardCode)
        {
            _prm.Add("@GiftCardCode", giftCardCode);
            var result = db.Fetch<GiftCardViewModel>(EnumSP.GiftCard.USPISGIFTCARDEXIST.ToString(), _prm);
            if(result.Count > 0)
            {
                return true;
            }else
            {
                return false;
            }

        }
    }
}
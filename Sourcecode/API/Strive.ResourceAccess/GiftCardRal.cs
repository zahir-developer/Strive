using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.GiftCard;
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
        IDbConnection _dbconnection;
        public Db db;
        public GiftCardRal(ITenantHelper tenant) : base(tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<GiftCardView> GetAllGiftCard(int locationId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@LocationId", locationId);
            var lstGiftCard = db.FetchRelation1<GiftCardView, GiftCardHistory>(SPEnum.USPGETALLGIFTCARD.ToString(), dynParams);
            return lstGiftCard;
        }
        public List<GiftCardView> GiftCardDetailByGiftCardId(int giftCardId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@GiftCardId", giftCardId);
            var lstGiftCardDetail = db.FetchRelation1<GiftCardView, GiftCardHistory>(SPEnum.USPGETGIFTCARDBYID.ToString(), dynParams);
            return lstGiftCardDetail;
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
        public bool SaveGiftCard(GiftCardView giftCard)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<GiftCard> lstGiftCard = new List<GiftCard>();
            lstGiftCard.Add(giftCard);

            dynParams.Add("@tvpGiftCard", lstGiftCard.ToDataTable().AsTableValuedParameter("tvpGiftCard"));
            dynParams.Add("@tvpGiftCardHistory", giftCard.GiftCardHistory.ToDataTable().AsTableValuedParameter("tvpGiftCardHistory"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEGIFTCARD.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}

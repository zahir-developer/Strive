using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblClientCardDetails")]
    public class ClientCardDetails
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int Id { get; set; }

        [Column]
        public string CardType { get; set; }

        [Column]
        public string CardNumber { get; set; }    
       
        [Column]
        public DateTime ExpiryDate { get; set; }

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientId { get; set; }

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int VehicleId { get; set; }

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int MembershipId { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}

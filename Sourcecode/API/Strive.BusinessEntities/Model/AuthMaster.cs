using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblAuthMaster")]
    public class AuthMaster
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int AuthId { get; set; }

        [Column, PrimaryKey]
        public string UserGuid { get; set; }

        [Column, PrimaryKey]
        public string EmailId { get; set; }

        [Column, PrimaryKey]
        public string MobileNumber { get; set; }

        [Column]
        public int? UserType { get; set; }

        [Column]
        public int? TenantId { get; set; }

        [Column]
        public short EmailVerified { get; set; }

        [Column]
        public short LockoutEnabled { get; set; }

        [Column]
        public string PasswordHash { get; set; }

        [Column]
        public string SecurityStamp { get; set; }

        [Column]
        public DateTime? CreatedDate { get; set; }
    }
}
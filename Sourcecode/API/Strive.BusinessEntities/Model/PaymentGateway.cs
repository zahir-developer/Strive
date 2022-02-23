using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cocoon.ORM;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblPaymentGateway")]
    public class PaymentGateway
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int? PaymentGatewayId { get; set; }

        [Column]
        public string PaymentGatewayName { get; set; }

        [Column]
        public string BaseURL { get; set; }

        [Column]
        public string APIKey { get; set; }

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

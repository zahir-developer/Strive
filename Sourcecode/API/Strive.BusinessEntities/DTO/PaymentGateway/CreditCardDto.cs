using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class CreditCardDto
    {
        /// <summary>
        /// 16 - Digit credit/debit card number
        /// </summary>
        [StringLength(16)]
        public string CardNumber { get; set; }

        /// <summary>
        /// NNN
        /// </summary>
        [StringLength(3)]
        public string CCV { get; set; }

        /// <summary>
        /// MM/YYYY
        /// </summary>
        [StringLength(4)]
        public string Expiry { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class CardConnectDetail
    {
        public string Profile { get { return "Y"; } }

        public string EcomInd { get { return "E"; } }

        public string Track { get { return null; } }

        public string Capture { get { return "Y"; } }

        public string Bin { get { return "Y"; } }
    }
}

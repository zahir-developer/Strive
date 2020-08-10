using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.User
{
    public class UserSignupDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }

        public int UserType { get; set; }

        public string InvitationCode { get; set; }
        
        [JsonIgnore]
        public int TenantId { get; set; }

        [JsonIgnore]
        public int ClientId { get; set; }

    }
}

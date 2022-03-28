using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TokenExpireViewModel
    {
        public int? TokenExpireMinutes { get; set; }
        public float? RefreshTokenExpiryMinutes { get; set; }
        public float? SessionExpiryWarning { get; set; }
        
    }
}

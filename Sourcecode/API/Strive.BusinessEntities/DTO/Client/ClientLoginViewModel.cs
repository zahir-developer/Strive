using Strive.BusinessEntities.ViewModel;
using System.Collections.Generic;

namespace Strive.BusinessLogic.DTO.Client
{
    public class ClientLoginViewModel
    {

        public ClientLoginDetailViewModel ClientDetail { get; set; }

        public List<RolePermissionViewModel> RolePermissionViewModel { get; set; }
        public TokenExpireViewModel TokenExpireMinutes { get; set; }

    }
}
using Strive.BusinessEntities.DTO.Product;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ServiceAndProductViewModel
    {
        public  List<ServiceViewModel> Service {get;set;}
        public  List<ProductDescriptionViewModel> Product {get;set;}
    }
}

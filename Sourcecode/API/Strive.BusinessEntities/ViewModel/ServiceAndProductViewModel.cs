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
        public  List<Service> Service {get;set;}
        public  List<Model.Product> Product {get;set;}
    }
}

using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ForcastedCarEmployeehoursViewModel
    {
        public decimal? ForcastedCars { get; set; }

        public decimal? ForcastedEmployeeHours { get; set; } 

        public decimal? RainPrecipitation { get; set; }
        public DateTime Date { get; set; }
        public int? TotalEmployees { get; set; }
        public decimal? Totalhours { get; set; }

    }

  

}


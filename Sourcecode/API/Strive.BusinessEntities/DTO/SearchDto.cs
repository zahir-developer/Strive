using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class SearchDto
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ClientId { get; set; }

        public int? VehicleId { get; set; }

        public int? LocationId { get; set; }

        public int? PageNo { get; set; }

        public int? PageSize { get; set; }

        public string Query { get; set; }

        public string SortOrder { get; set; }

        public string SortBy { get; set; }

        public bool? Status { get; set; }
       
        
    }
}

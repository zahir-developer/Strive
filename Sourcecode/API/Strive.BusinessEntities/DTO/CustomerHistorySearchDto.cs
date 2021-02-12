using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class CustomerHistorySearchDto
    {
        public int LocationId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNo { get; set; }

        public int? PageSize { get; set; }

        public string Query { get; set; }

        public string SortOrder { get; set; }

        public string SortBy { get; set; }
    }
}

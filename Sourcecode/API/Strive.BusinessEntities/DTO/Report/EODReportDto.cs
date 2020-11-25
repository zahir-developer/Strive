using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Report
{
    public class EODReportDto
    {
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public string CashRegisterType { get; set; }
    }
}

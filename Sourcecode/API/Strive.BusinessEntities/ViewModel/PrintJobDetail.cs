using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PrintJobDetail
    {
        public PrintJob Job { get; set; }

        public List<PrintJobItem> JobItem { get; set; }

        public PrintJobClient ClientDetail { get; set; }
    }
}

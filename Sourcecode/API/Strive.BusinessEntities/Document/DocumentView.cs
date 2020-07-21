using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Document
{
    public class DocumentView
    {
        public long DocumentId { get; set; }
        public long EmployeeId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string Base64Url { get; set; }

    }
}

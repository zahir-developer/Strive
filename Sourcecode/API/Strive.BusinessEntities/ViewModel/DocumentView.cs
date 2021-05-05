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
        public int DocumentType { get; set; }
        public string DocumentSubtype { get; set; }
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FilePath { get; set; }
        public string Base64 { get; set; }
        public string DocumentName { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}

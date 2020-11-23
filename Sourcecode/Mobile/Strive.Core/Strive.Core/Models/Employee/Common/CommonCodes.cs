using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Common
{
    public class CommonCodes
    {
        public List<Codes> Codes;
    }
    public class Codes
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CodeId { get; set; }
        public string CodeValue { get; set; }
        public string CodeShortValue { get; set; }
        public string Sortorder { get; set; }
    }
}

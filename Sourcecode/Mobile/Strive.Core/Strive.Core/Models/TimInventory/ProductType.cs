using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class Code
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CodeId { get; set; }
        public string CodeValue { get; set; }
        public string CodeShortValue { get; set; }
        public int Sortorder { get; set; }
    }

    public class ProductType
    {
        public List<Code> Codes { get; set; }
    }       
}

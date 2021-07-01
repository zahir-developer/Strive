using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class VehicleCodes
    {
        public List<VehicleDetails> VehicleDetails { get; set; }
    }
    public class VehicleDetails
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CodeId { get; set; }
        public string CodeValue { get; set; }
    }
    public class ModelDetails
    {
        public int ModelId { get; set; }
        public string ModelValue { get; set; }
        public string MakeValue { get; set; }
    }
    public class ModelList
    {
        public List<ModelDetails> Model { get; set; }
    }
    public class MakeDetails
    {
        public int MakeId { get; set; }
        public string MakeValue { get; set; }
    }
    public class MakeList
    {
        public List<MakeDetails> Make { get; set; }
    }
}

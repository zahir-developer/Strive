using System.Collections.Generic;

namespace Strive.BusinessEntities
{

    public class Temp
    {
        public Min Min { get; set; }
        public Max Max { get; set; }
    }
   
    public class Precipitation
    {
        public Max Max { get; set; }
    }
    public class Min
    {

        public decimal? Value { get; set; }
        public string Units { get; set; }
    }
    public class Max
    {
        public decimal? Value { get; set; }
        public string Units { get; set; }
    }
    public class PrecipitationProbability
    {
        public int Value { get; set; }
        public string Units { get; set; }
    }
}
